using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console leaderboard render - Methods are static due to this being used in many places without init </summary>
    class LeaderboardScreen
    {
        /// <summary> Initialize the leaderboard and print what is needed</summary>
        public static void Setup()
        {
            Console.Clear();
        }

        /// <summary> Function used to update the leaderboard given the leaderboard class</summary>
        public static void Update(LeaderboardObj[] scores)
        {
            Console.Clear();
            Console.WriteLine("test");
        }
    }
}
