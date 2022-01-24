using System;

namespace ConsoleConnectFour
{
    public static class Extension
    {
        public static string PadSides(this string str, int totalWidth, char paddingChar = ' ')
        {
            int padding = totalWidth - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(totalWidth, paddingChar);
        }
    }

    /// <summary> Class for console menu render - Methods are static due to this being used in many places without init</summary>
    class MenuScreen
    {
        /// <summary> Visual menu outline for console </summary>
        static string[] menuOutline =
            {
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "               * Game Menu *               ",
                "       +--------------------------+        ",
                "       |                          |        ",
                "       |                          |        ",
                "       |                          |        ",
                "       |                          |        ",
                "       |                          |        ",
                "       |                          |        ",
                "       |                          |        ",
                "       +--------------------------+        ",
                "                                           ",
                "           Console Connect Four            ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
                "                                           ",
        };

        static MenuItem[] currentMenuItems;

        static string getMenuItemString(MenuItem item)
        {
            switch (item)
            {
                case MenuItem.local_play:
                    return "Local Play";
                case MenuItem.local_single:
                    return "Single Player";
                case MenuItem.local_dual:
                    return "Two Player";
                case MenuItem.local_tripple:
                    return "Three Player";
                case MenuItem.local_quad:
                    return "Four Player";
                case MenuItem.leaderboard:
                    return "Online Leaderbaord";
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
        public static void Setup(bool loggedIn, UserObj user)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            // Height of the menu
            int menuHeight = menuOutline.GetLength(0);

            // Print each line of grid out on board
            for (int y = 0; y < menuHeight; y++)
            {
                Console.SetCursorPosition(6, y);

                if (y == 22 && loggedIn) {
                    Console.WriteLine(user.username);
                } else {
                    Console.WriteLine(menuOutline[y]);
                }
            }
        }

        /// <summary> Fucntion used to update the menu a menu array and selected</summary>
        public static void Update(MenuItem[] desiredMenuItems, MenuItem selectedMenuItem)
        {
            // Itteration of print
            int x = 8;

            // Wipe only if whole menu list is changed
            if (currentMenuItems != desiredMenuItems)
            {
               //Console.Clear();
            };

            // Update the stored menu items
            currentMenuItems = desiredMenuItems;

            // Print each menu item out
            foreach (MenuItem item in currentMenuItems)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(14, x);

                string output = getMenuItemString(item);

                 if (item == selectedMenuItem)
                {
                    output = $"[{output}]";
                }
                
                Console.WriteLine(output.PadSides(26));

                x+=1;
            }
        }
    }
}
