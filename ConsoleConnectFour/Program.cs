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
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Console Connect Four";
            Console.SetWindowSize(79, 30);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            gameExit gameExitInfo;

            while (true)
            {
                Menu.Loop();

                Game gameInstance = new Game(MenuSystem.selected);
                gameExitInfo = gameInstance.Loop();

                if (gameExitInfo.won)
                {
                    Message messageInstance = new Message(gameExitInfo);
                    messageInstance.Loop();
                }

            }
        }
    }
}
