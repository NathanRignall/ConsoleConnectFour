using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console message render - Methods are static due to this being used in many places without init </summary>
    class MessageScreen
    {
        static string[] messageOutline =
            {
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "               * Game Over *               ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
        };

        /// <summary> Initialize the message and print what is needed</summary>
        public static void Setup()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            // Height of the message 
            int messageHeight = messageOutline.GetLength(0);

            // Print each line of gave over out on board
            for (int y = 0; y < messageHeight; y++)
            {
                Console.SetCursorPosition(6, y);

                Console.WriteLine(messageOutline[y]);
            }
        }

        /// <summary> Fucntion used to update the message given a new message</summary>
        public static void Update(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;

{           Console.SetCursorPosition(14, 8);
            
            Console.WriteLine(message.PadSides(26));}
        }
    }
}
