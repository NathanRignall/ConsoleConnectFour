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
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        /// <summary> Stores the current selected column for drop </summary>
        private int dropSelectedColumn = 3;

        /// <summary> Used to move the selected space before drop </summary>
        private void moveSelected(bool direction, int playerCode)
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
            for (int x = 0; x < gridWidth; x++)
            {
                if (x == dropSelectedColumn)
                {
                    piecePlacement[0, x] = playerCode;
                }
                else
                {
                    piecePlacement[0, x] = 0;
                }
            }

        }

        /// <summary> Drop the piece </summary>
        private bool drop(int playerCode)
        {
            // get the width of the baord
            int gridHeight = piecePlacement.GetLength(1);

            // used to say if teh piece has been dropped
            bool placedPiece = false;

            // move down until impact
            for (int y = 1; y < gridHeight; y++)
            {

                if (piecePlacement[y, dropSelectedColumn] == 0)
                {
                    // mark piece on array
                    piecePlacement[(y), dropSelectedColumn] = playerCode;

                    // clear previous piece on array
                    piecePlacement[(y - 1), dropSelectedColumn] = 0;

                    // changed the marked piece as true
                    placedPiece = true;
                }
            }

            // return if the peice was able to be dropped at the space
            return placedPiece;
        }

        public Game()
        {
            // initiaze the classes
            Board DefaultScreen = new Board();
            Logic DefaultLogic = new Logic();

            // stores which player is currently playing
            int playerCode = 1;

            // main game loop
            while (true)
            {
                DefaultScreen.updateBoard(piecePlacement);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    moveSelected(true, playerCode);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    moveSelected(false, playerCode);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (drop(playerCode))
                    {

                        if (DefaultLogic.gameOver(piecePlacement, dropSelectedColumn))
                        {
                            DefaultScreen.updateBoard(piecePlacement);

                            string outputText = "Game Over! Well done " + playerCode.ToString();

                            GlobalFunctions.WriteAt(outputText, 3, 8);
                            break;
                        }

                        if (playerCode == 1)
                        {
                            playerCode = 2;
                            dropSelectedColumn = 3;

                        }
                        else
                        {
                            playerCode = 1;
                            dropSelectedColumn = 3;
                        }

                        piecePlacement[0, 3] = playerCode;

                    }

                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }
    }
}
