using System;


namespace ConsoleConnectFour
{
    /// <summary> Class for whole message </summary>
    class Message
    {
        /// <summary> Current message to be output </summary>
        string message;

        /// <summary> Init class by setting vars and starting screen </summary>
        public Message(GameExitStatus gameExitInfo)
        {
            message = gameExitInfo.message;

            MessageScreen.Setup();
        }

        /// <summary> Loop for each interaction with console </summary>
        public void Loop()
        {
            // Main menu loop
            while (true)
            {
                // Re-render the screen on each action
                MessageScreen.Update(message);

                // The key that was last pressed by the user
                ConsoleKey key = Console.ReadKey(true).Key;

                // Check which key was pressed for approriate action
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
