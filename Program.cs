using System.Diagnostics;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text.Json;


namespace hunt_overlay {
    internal static class Program {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            using (Process p = Process.GetCurrentProcess()) {
                p.PriorityClass = ProcessPriorityClass.BelowNormal;
            }

            SetProcessDPIAware();

            string? playerName = null;
            while (playerName == null) {
                Console.WriteLine("Enter player name:");
                playerName = Console.ReadLine();
            }
            Console.WriteLine("Enter match id or press ENTER to create a new match:");
            string? matchIdStr = Console.ReadLine();
            long? matchId = null;
            if (matchIdStr != null && matchIdStr != "") {
                matchId = long.Parse(matchIdStr);
            }

            var gameEventsDetector = new GameEventsDetector();
            var dataController = new DataController(playerName, matchId);
            gameEventsDetector.PlayerDeath += delegate {
                Console.WriteLine("PlayerDeath");
                dataController.OnPlayerDeath();
            };
            gameEventsDetector.PlayerRespawn += delegate {
                Console.WriteLine("PlayerRespawn");
                dataController.OnPlayerRespawn();
            };

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new OverlayForm(dataController));
        }
    }
}