using System;


namespace ConsoleConnectFour
{
    /// <summary> Class for whole leaderboard </summary>
    class Leaderboard
    {
        /// <summary> Init class by starting screen</summary>
        public Leaderboard() {
            LeaderboardScreen.Setup();
        }

        /// <summary> Loop for each interaction with console </summary>
        public void Loop()
        {
            // Main leaderboard loop
            while (true)
            {
                //LeaderboardPayloadObj response = Client.Leaderboard();

                //LeaderboardScreen.Update(response.scores);

                // The key that was last pressed by the user
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
