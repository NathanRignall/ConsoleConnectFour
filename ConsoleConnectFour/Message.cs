using System;


namespace ConsoleConnectFour
{
    class MessageSystem
    {


    }

    class Message
    {
        string message;
        bool won;

        public Message(gameExit gameExitInfo)
        {
            message = gameExitInfo.message;
            won = gameExitInfo.won;
        }

        public void Loop()
        {
            // setup the screen
            MessageScreen.Setup();

            // menu loop
            while (true)
            {
                MessageScreen.Update(message);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
