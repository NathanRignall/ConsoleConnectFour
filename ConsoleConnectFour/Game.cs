using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    class Game
    {
        /// <summary> Visual Board Outline for Console </summary>
        private int[,] boardDesiredPlacement =
        {
            { 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 2, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 2 },
            { 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 2, 0, 0, 0, 2 },
        };

        public Game()
        {
            Board DefaultScreen = new Board();

            while (true)
            {
                DefaultScreen.updateBoard(boardDesiredPlacement);
                Thread.Sleep(1000);
            }
        }


    }
}
