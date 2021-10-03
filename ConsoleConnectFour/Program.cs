using System;


namespace ConsoleConnectFour
{

    public static class GlobalFunctions
    {
        public static void WriteAt(string s, int x, int y)
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
    }

    class Program
    {
        protected static void setupConsole()
        {
            Console.CursorVisible = false;
            Console.Title = "Console Connect Four";
            Console.SetWindowSize(79, 42);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }


        static void Main(string[] args)
        {
            setupConsole();

            Game DefaultGame = new Game();
        }
    }
}
