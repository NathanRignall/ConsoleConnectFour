using System;
using System.Threading.Tasks;

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
        static async Task Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "Console Connect Four";
            Console.SetWindowSize(79, 30);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            Client client = new Client();

            gameExit gameExitInfo;

            while (true)
            {
                Menu.Loop();

                switch (MenuSystem.selected)
                {
                    case MenuItem.local_dual:
                    case MenuItem.local_tripple:
                    case MenuItem.local_quad:
                        Game gameInstance = new Game(MenuSystem.selected);
                        gameExitInfo = gameInstance.Loop();

                        if (gameExitInfo.won)
                        {
                            Message messageInstance = new Message(gameExitInfo);
                            messageInstance.Loop();
                        }
                        break;
                    case MenuItem.login:
                        await client.Login();
                        break;
                    case MenuItem.register:
                        await client.Register();
                        break;
                    case MenuItem.info:
                        await client.Info();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
