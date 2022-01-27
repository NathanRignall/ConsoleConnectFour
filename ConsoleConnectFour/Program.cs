using System;
using System.Threading.Tasks;

namespace ConsoleConnectFour
{
    /// <summary> Main whole program class </summary>
    class Program
    {
        /// <summary> Task for game to allow async methods </summary>
        static async Task Main(string[] args)
        {
            // Generally setup the console for use (only works in windows)
            Console.CursorVisible = false;
            Console.Title = "Console Connect Four";
            //Console.SetWindowSize(53, 25);
            //Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            // Setup the http client for use with API
            Client.Setup();

            // Main game loop - contains sub loops but always returns here.
            while (true)
            {
                Menu menuInstance = new Menu(Client.LoggedIn, Client.User);
                MenuItem systemMode = menuInstance.Loop();

                switch (systemMode)
                {
                    case MenuItem.local_dual:
                    case MenuItem.local_tripple:
                    case MenuItem.local_quad:
                        Game gameInstance = new Game(systemMode);
                        GameExitStatus gameExitInfo = gameInstance.Loop();

                        // Only if exit due to win should show celebration
                        if (gameExitInfo.won)
                        {
                            Message messageInstance = new Message(gameExitInfo);
                            messageInstance.Loop();
                        }

                        break;
                    case MenuItem.leaderboard:
                        Leaderboard leaderboardInstance = new Leaderboard();
                        leaderboardInstance.Loop();

                        break;
                    case MenuItem.login:
                        await Client.Login();

                        break;
                    case MenuItem.register:
                        await Client.Register();

                        break;
                    case MenuItem.info:
                        await Client.Info();
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
