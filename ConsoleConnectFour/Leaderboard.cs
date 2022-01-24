using System;
using System.Threading.Tasks;

namespace ConsoleConnectFour
{
    /// <summary> Class for whole leaderboard </summary>
    class Leaderboard
    {
        public LeaderboardPayloadObj leaderboard;

        /// <summary> Init class by starting screen</summary>
        public Leaderboard() {
            Task<LeaderboardPayloadObj> task = Client.Leaderboard();
            leaderboard = task.Result;

            LeaderboardScreen.Setup();
        }

        /// <summary> Loop for each interaction with console </summary>
        public void Loop()
        {
            // Main leaderboard loop
            while (true)
            {
                // Re-render the screen on each action
                //LeaderboardScreen.Update(leaderboard.scores);

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
