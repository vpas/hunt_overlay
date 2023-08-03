using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace hunt_overlay {
    [Serializable]
    public struct Match {
        [JsonInclude]
        public long? id;
        [JsonInclude]
        public string playerName1;
        [JsonInclude]
        public string playerName2;
        [JsonInclude]
        public int playerScore1;
        [JsonInclude]
        public int playerScore2;

        [JsonInclude]
        public int timerMillisec;
        [JsonInclude]
        public bool isTimerActive;
        [JsonInclude]
        public string roundInfo;
        [JsonInclude]
        public string message;
    }
}
