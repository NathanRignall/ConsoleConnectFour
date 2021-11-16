using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    enum MenuItem
    {
        play,
        leaderboard,
        settings,
        single,
        dual,
        tripple,
        quad,

    }

    class MenuSystem
    {

        public static int Row = 0;

        public static MenuItem[] items = { MenuItem.play, MenuItem.leaderboard, MenuItem.settings };

        public static MenuItem selected = MenuItem.play;

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
                case MenuItem.play:
                    items = new MenuItem[] { MenuItem.single, MenuItem.dual, MenuItem.tripple, MenuItem.quad };
                    selected = MenuItem.single;
                    return false;
                case MenuItem.dual:
                case MenuItem.tripple:
                case MenuItem.quad:
                    return true;
                default:
                    return false;
            }
        }

        public static void Return()
        {
            items = new MenuItem[] { MenuItem.play, MenuItem.leaderboard, MenuItem.settings };
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
                        Game.Active = true;
                        Game.Mode = MenuSystem.selected;
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
