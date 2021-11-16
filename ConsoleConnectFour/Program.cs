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

            while (true)
            {
                Menu.Loop();
                GlobalFunctions.WriteAt("run", 1, 8);

                if (Game.Active)
                {
                    Game.Loop();
                }
            }
        }
    }
}
