using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console menu render - Methods are static due to this being used in many places without init</summary>
    class MenuScreen
    {
        static MenuItem[] currentMenuItems;

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

        /// <summary> Initialize the menu and print what is needed</summary>
        public static void Setup()
        {
            Console.Clear();
        }

        /// <summary> Fucntion used to update the menu a menu array and selected</summary>
        public static void Update(MenuItem[] desiredMenuItems, MenuItem selectedMenuItem)
        {
            // Itteration of print
            int x = 0;

            // Wipe only if whole menu list is changed
            if (currentMenuItems != desiredMenuItems)
            {
               Console.Clear();
            }

            // Update the stored menu items
            currentMenuItems = desiredMenuItems;

            // Print each menu item out
            foreach (MenuItem item in currentMenuItems)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, x);

                if (item == selectedMenuItem)
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
