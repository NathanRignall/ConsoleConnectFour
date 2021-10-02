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
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 1, 0, 0, 0, 0 },
        };

        /// <summary> Stores the current selected column for drop </summary>
        private int selectedColumn = 3;

        /// <summary> Simple write console fucnction used with debuging </summary>
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary> Used to move the selected space before drop </summary>
        private void move(bool direction, int player)
        {
            // get the width of the baord
            int boardWidth = boardDesiredPlacement.GetLength(0);

            // control the pacement of the selected column
            if (direction == true)
            {
                if (selectedColumn == (boardWidth - 1))
                {
                    selectedColumn = 0;
                }
                else
                {
                    selectedColumn++;
                }
            }
            else
            {
                if (selectedColumn == 0)
                {
                    selectedColumn = (boardWidth - 1);
                }
                else
                {
                    selectedColumn--;
                }
            }

            // mark the correct spot in grid with mark
            for (int i = 0; i < boardWidth; i++)
            {
                if (i == selectedColumn)
                {
                    boardDesiredPlacement[0, i] = player;
                }
                else
                {
                    boardDesiredPlacement[0, i] = 0;
                }
            }

        }

        /// <summary> Drop the piece </summary>
        private bool drop(int player)
        {
            // get the width of the baord
            int boardHeight = boardDesiredPlacement.GetLength(1);

            // used to say if teh piece has been dropped
            bool placedPiece = false;

            // move down until impact
            for (int i = 1; i < boardHeight; i++)
            {

                if (boardDesiredPlacement[i, selectedColumn] == 0)
                {
                    // mark piece on array
                    boardDesiredPlacement[(i), selectedColumn] = player;

                    // clear previous piece on array
                    boardDesiredPlacement[(i - 1), selectedColumn] = 0;

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

            // stores which player is currently playing
            int player = 1;

            // main game loop
            while (true)
            {
                DefaultScreen.updateBoard(boardDesiredPlacement);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    move(true, player);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    move(false, player);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (drop(player))
                    {
                        if (player == 1)
                        {
                            player = 2;
                            selectedColumn = 3;

                        }
                        else
                        {
                            player = 1;
                            selectedColumn = 3;
                        }

                        boardDesiredPlacement[0, 3] = player;
                    }

                }
                else if (key == ConsoleKey.Escape)
                {

                }
            }
        }


    }
}
