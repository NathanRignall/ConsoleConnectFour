using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console game board </summary>
    class MenuScreen
    {
        static MenuItem[] currentMenuItems;
        static MenuItem currentSelectedMenuItem;

        static string getMenuItemString(MenuItem item)
        {
            switch (item)
            {
                case MenuItem.local_play:
                    return "Play";
                case MenuItem.local_single:
                    return "Single Player";
                case MenuItem.local_dual:
                    return "2 Player";
                case MenuItem.local_tripple:
                    return "3 Player";
                case MenuItem.local_quad:
                    return "4 Player";
                case MenuItem.leaderboard:
                    return "Leaderbaord";
                case MenuItem.settings:
                    return "Settings";
                case MenuItem.login:
                    return "Login";
                case MenuItem.register:
                    return "Register";
                case MenuItem.info:
                    return "Info";
                default:
                    return "test";
            }
        }

        /// <summary> Initialize the board and print the board</summary>
        public static void Setup()
        {
            Console.Clear();
        }

        /// <summary> fucntion used to update the game board given piece array</summary>
        public static void Update(MenuItem[] desiredMenuItems, MenuItem desiredSelectedMenuItem)
        {
            int x = 0;

            if (currentMenuItems != desiredMenuItems)
            {
                Console.Clear();
            }

            currentMenuItems = desiredMenuItems;
            currentSelectedMenuItem = desiredSelectedMenuItem;

            // print each line of grid out on board
            foreach (MenuItem item in currentMenuItems)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, x);



                if (item == currentSelectedMenuItem)
                {
                    Console.WriteLine($"[{getMenuItemString(item)}]");

                }
                else
                {
                    Console.WriteLine($" {getMenuItemString(item)} ");
                }

                x++;
            }
        }
    }
}
