using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console message render - Methods are static due to this being used in many places without init </summary>
    class MessageScreen
    {
        /// <summary> Initialize the message and print what is needed</summary>
        public static void Setup()
        {
            Console.Clear();
        }

        /// <summary> Fucntion used to update the message given a new message</summary>
        public static void Update(string message)
        {
            Console.Clear();
            Console.WriteLine($"{message}");
        }
    }
}
