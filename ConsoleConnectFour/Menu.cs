using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
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

    class MenuSystem
    {

        public static int Row = 0;

        public static MenuItem[] items = { MenuItem.local_play, MenuItem.leaderboard, MenuItem.settings, MenuItem.quit };

        public static MenuItem selected = MenuItem.local_play;

        public static void Move(bool direction)
        {
            // get the width of the baord
            int total = items.GetLength(0);

            // control the pacement of the selected column
            if (direction)
            {
                if (Row == (total - 1))
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
                if (Row == 0)
                {
                    Row = (total - 1);
                }
                else
                {
                    Row--;
                }
            }

            selected = items[Row];
        }

        public static bool Select()
        {
            switch (selected)
            {
                case MenuItem.local_play:
                    items = new MenuItem[] { MenuItem.local_single, MenuItem.local_dual, MenuItem.local_tripple, MenuItem.local_quad };
                    selected = MenuItem.local_single;
                    Row = 0;
                    return false;
                case MenuItem.local_dual:
                case MenuItem.local_tripple:
                case MenuItem.local_quad:
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
                default:
                    return false;
            }
        }

        public static void Return()
        {
            items = new MenuItem[] { MenuItem.local_play, MenuItem.leaderboard, MenuItem.settings, MenuItem.quit };
            selected = MenuItem.local_play;
            Row = 0;
        }
    }

    class Menu
    {
        static public void Loop()
        {
            // setup the screen
            MenuScreen.Setup();

            // menu loop
            while (true)
            {
                MenuScreen.Update(MenuSystem.items, MenuSystem.selected);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    MenuSystem.Move(false);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    MenuSystem.Move(true);
                }
                else if (key == ConsoleKey.Enter)
                {
                    if (MenuSystem.Select())
                    {
                        break;
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    MenuSystem.Return();
                }
            }
        }
    }
}
