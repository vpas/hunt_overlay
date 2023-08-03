using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hunt_overlay {
    [Serializable]
    public struct ServerResponse {
        [JsonInclude]
        public string message;
        [JsonInclude]
        public Match match;
        [JsonInclude]
        public string error;
    }

    public class DataController {
        public event EventHandler<Match> DataUpdated = delegate { };

        private readonly Uri matchUrl = 
            new("https://qo9rj5r757.execute-api.eu-central-1.amazonaws.com/matches");

        private readonly JsonSerializerOptions jsonOptions = new() { WriteIndented = true };
        private readonly HttpClient httpClient;
        private Match match;
        private readonly string playerName;

        public DataController(string playerName, long? matchId) {
            this.match = new() { id = matchId };
            this.playerName = playerName;
            this.httpClient = new();
            var updateThread = new Thread(new ThreadStart(this.UpdateLoop)) {
                IsBackground = true,
                Priority = ThreadPriority.Lowest
            };
            updateThread.Start();
        }

        private void LogErrorIfFound(ServerResponse response) {
            if (response.error != null && response.error != "") {
                Console.WriteLine(response.error);
            }
        }

        public void OnPlayerDeath() {
            Console.WriteLine("DataController.OnPlayerDeath");
            var url = $"{matchUrl}/{match.id}/death?playerName={playerName}";
            var response = httpClient.GetAsync(url).Result;
            var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            Console.WriteLine(responseBody.message);
            LogErrorIfFound(responseBody);
            FetchMatchData();
        }

        public void OnPlayerRespawn() {
            Console.WriteLine("DataController.OnPlayerRespawn");
            //var url = $"{matchUrl}/{match.id}/respawn?playerName={playerName}";
            //var response = httpClient.GetAsync(url).Result;
            //var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            //Console.WriteLine(responseBody.message);
            //LogErrorIfFound(responseBody);
            //FetchMatchData();
        }

        public void OnGameReset() {
            Console.WriteLine("DataController.OnGameReset");
            var url = $"{matchUrl}/{match.id}/reset";
            var response = httpClient.GetAsync(url).Result;
            var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            Console.WriteLine(responseBody.message);
            LogErrorIfFound(responseBody);
            FetchMatchData();
        }

        private void CreateMatch() {
            Console.WriteLine("DataController.CreateMatch");

            match.playerName1 = playerName;
            var response = httpClient.PostAsJsonAsync(matchUrl, match).Result;
            var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            LogErrorIfFound(responseBody);
            match = responseBody.match;
            Console.WriteLine(JsonSerializer.Serialize(responseBody, jsonOptions));
            Console.WriteLine("*****");
        }

        private void JoinMatch() {
            Console.WriteLine("DataController.JoinMatch");
            var url = $"{matchUrl}/{match.id}/join?playerName={playerName}";
            var response = httpClient.GetAsync(url).Result;
            var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            Console.WriteLine(responseBody.message);
            LogErrorIfFound(responseBody);
            FetchMatchData();
        }

        private void FetchMatchData() {
            Console.WriteLine("DataController.FetchMatchData");
            var url = $"{matchUrl}/{match.id}";
            var response = httpClient.GetAsync(url).Result;
            var responseBodyStr = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(responseBodyStr);
            var responseBody = response.Content.ReadFromJsonAsync<ServerResponse>().Result;
            Console.WriteLine(responseBody.message);
            LogErrorIfFound(responseBody);
            match = responseBody.match;

            DataUpdated(this, match);
        }

        private void UpdateLoop() {
            Console.WriteLine("DataController.UpdateLoop");

            if (match.id == null) {
                CreateMatch();
            } else {
                JoinMatch();
            }

            while (true) {
                Thread.Sleep(500);
                FetchMatchData();
            }
        }
    }
}
