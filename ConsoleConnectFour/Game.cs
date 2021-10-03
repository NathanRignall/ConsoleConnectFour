using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    class Game
    {
        /// <summary> Visual Board Outline for Console </summary>
        private int[,] piecePlacement =
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0 },
        };

        /// <summary> Stores the current selected column for drop </summary>
        private int dropSelectedColumn = 3;

        /// <summary> Used to move the selected space before drop </summary>
        private void moveSelected(bool direction, int player)
        {
            // get the width of the baord
            int gridWidth = piecePlacement.GetLength(0);

            // control the pacement of the selected column
            if (direction)
            {
                if (dropSelectedColumn == (gridWidth - 1))
                {
                    dropSelectedColumn = 0;
                }
                else
                {
                    dropSelectedColumn++;
                }
            }
            else
            {
                if (dropSelectedColumn == 0)
                {
                    dropSelectedColumn = (gridWidth - 1);
                }
                else
                {
                    dropSelectedColumn--;
                }
            }

            // mark the correct spot in grid with mark
            for (int i = 0; i < gridWidth; i++)
            {
                if (i == dropSelectedColumn)
                {
                    piecePlacement[0, i] = player;
                }
                else
                {
                    piecePlacement[0, i] = 0;
                }
            }

        }

        /// <summary> Drop the piece </summary>
        private bool drop(int player)
        {
            // get the width of the baord
            int gridHeight = piecePlacement.GetLength(1);

            // used to say if teh piece has been dropped
            bool placedPiece = false;

            // move down until impact
            for (int i = 1; i < gridHeight; i++)
            {

                if (piecePlacement[i, dropSelectedColumn] == 0)
                {
                    // mark piece on array
                    piecePlacement[(i), dropSelectedColumn] = player;

                    // clear previous piece on array
                    piecePlacement[(i - 1), dropSelectedColumn] = 0;

                    // changed the marked piece as true
                    placedPiece = true;
                }
            }

            // return if the peice was able to be dropped at the space
            return placedPiece;
        }

        public Game()
        {
            Board DefaultScreen = new Board();

            Logic DefaultLogic = new Logic();

            // stores which player is currently playing
            int player = 1;

            // main game loop
            while (true)
            {
                DefaultScreen.updateBoard(piecePlacement);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    moveSelected(true, player);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    moveSelected(false, player);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (drop(player))
                    {

                        //if (DefaultLogic.gameOver(piecePlacement))
                        //{
                        //GlobalFunctions.WriteAt("Game Over", 0, 8);
                        //}

                        if (player == 1)
                        {
                            player = 2;
                            dropSelectedColumn = 3;

                        }
                        else
                        {
                            player = 1;
                            dropSelectedColumn = 3;
                        }

                        piecePlacement[0, 3] = player;

                    }

                }
                else if (key == ConsoleKey.Escape)
                {

                }
            }

            GlobalFunctions.WriteAt("Game Over 22", 0, 9);

        }


    }
}
