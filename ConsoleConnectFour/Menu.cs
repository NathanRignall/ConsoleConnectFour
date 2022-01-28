using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    /// <summary> Enum for the various menu possible items </summary>
    enum MenuItem
    {
        local_play,
        local_single,
        local_dual,
        local_tripple,
        local_quad,
        leaderboard,
        settings,
        login,
        register,
        info,
        quit,

    }

    /// <summary> Class used to manage the selected column in the menu </summary>
    class MenuSystem
    {
        /// <summary> The current row of the menu selected </summary>
        int Row = 0;

        /// <summary> Array of current items in menu selection </summary>
        public MenuItem[] items;

        /// <summary> The current selected item in the menu list (not done by index)</summary>
        public MenuItem selected = MenuItem.local_play;

        /// <summary> Move the selected row up or down </summary>
        public void Move(bool direction)
        {
            // Height of the menu
            int menuHeight = items.GetLength(0);

            // Move up or down based on the bool direction
            if (direction)
            {
                // Check if reached row bound 
                if (Row == (menuHeight - 1))
                {
                    Row = 0;
                }
                else
                {
                    Row++;
                }
            }
            else
            {
                // Check if reached row bound 
                if (Row == 0)
                {
                    Row = (menuHeight - 1);
                }
                else
                {
                    Row--;
                }
            }

            // Set the selected item
            selected = items[Row];
        }

         /// <summary> Select the selected row in the menu and output if action is required</summary>
        public bool Select()
        {
            switch (selected)
            {
                case MenuItem.local_play:
                    items = new MenuItem[] { MenuItem.local_single, MenuItem.local_dual, MenuItem.local_tripple, MenuItem.local_quad };
                    selected = MenuItem.local_single;
                    Row = 0;
                    return false;
                case MenuItem.local_single:
                case MenuItem.local_dual:
                case MenuItem.local_tripple:
                case MenuItem.local_quad:
                    return true;
                case MenuItem.leaderboard:
                    return true;
                case MenuItem.settings:
                    items = new MenuItem[] { MenuItem.login, MenuItem.register, MenuItem.info };
                    selected = MenuItem.login;
                    Row = 0;
                    return false;
                case MenuItem.login:
                    return true;
                case MenuItem.register:
                    return true;
                case MenuItem.info:
                    return true;
                case MenuItem.quit:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary> Init class by setting variables to default </summary>
        public MenuSystem()
        {
            items = new MenuItem[] { MenuItem.local_play, MenuItem.leaderboard, MenuItem.settings, MenuItem.quit };
            selected = MenuItem.local_play;
            Row = 0;
        }
    }

    /// <summary> Class for whole menu </summary>
    class Menu
    {
        /// <summary> Paired menu system instance </summary>
        MenuSystem menuSystemInstance;

        /// <summary> Init class by setting vars and starting screen </summary>
        public Menu(bool loggedIn, UserObj user)
        {
            menuSystemInstance = new MenuSystem();

            MenuScreen.Setup(loggedIn, user);
        }

        public MenuItem Loop()
        {
            // Main menu loop
            while (true)
            {
                // Re-render the screen on each action
                MenuScreen.Update(menuSystemInstance.items, menuSystemInstance.selected);

                 // The key that was last pressed by the user
                ConsoleKey key = Console.ReadKey(true).Key;

                // Check which key was pressed for approriate action
                if (key == ConsoleKey.UpArrow)
                {
                    menuSystemInstance.Move(false);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    menuSystemInstance.Move(true);
                }
                else if (key == ConsoleKey.Enter)
                {
                    // Only if the menu should exit
                    if (menuSystemInstance.Select())
                    {
                        return menuSystemInstance.selected;
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    // Reset the class back to home
                    menuSystemInstance = new MenuSystem();
                }
            }
        }
    }
}
