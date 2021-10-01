using System;
using System.Threading;

namespace ConsoleConnectFour
{
    class Program
    {
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {



            Screen DefaultScreen = new Screen();

            Console.CursorVisible = false;
            Console.Title = "Connect Four";
            Console.SetWindowSize(79, 42);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            DefaultScreen.placeBoard();

            WriteAt("+", 1, 0);

            DefaultScreen.updateBoard();

            Console.ReadKey(true);

            while (true)
            {
                //DefaultScreen.placeBoard();
                Thread.Sleep(100);
            }
        }
    }
}
