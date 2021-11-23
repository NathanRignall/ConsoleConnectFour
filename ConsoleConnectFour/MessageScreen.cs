using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console game board </summary>
    class MessageScreen
    {
        /// <summary> Initialize the board and print the board</summary>
        public static void Setup()
        {
            Console.Clear();
        }

        /// <summary> fucntion used to update the game board given piece array</summary>
        public static void Update(string message)
        {
            Console.Clear();
            Console.WriteLine($"{message}");
        }
    }
}
