using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    class PeiceDrop
    {
        public static int Column = 3;

        public static void Move(bool direction, int playerCode, ref int[,] PiecePlacementGrid)
        {
            // get the width of the baord
            int gridWidth = PiecePlacementGrid.GetLength(0);

            // control the pacement of the selected column
            if (direction)
            {
                if (Column == (gridWidth - 1))
                {
                    Column = 0;
                }
                else
                {
                    Column++;
                }
            }
            else
            {
                if (Column == 0)
                {
                    Column = (gridWidth - 1);
                }
                else
                {
                    Column--;
                }
            }

            // mark the correct spot in grid with mark
            for (int x = 0; x < gridWidth; x++)
            {
                if (x == Column)
                {
                    PiecePlacementGrid[0, x] = playerCode;
                }
                else
                {
                    PiecePlacementGrid[0, x] = 0;
                }
            }
        }

        public static bool Release(ref int[,] PiecePlacementGrid, int playerCode)
        {
            // get the width of the baord
            int gridHeight = PiecePlacementGrid.GetLength(1);

            // used to say if teh piece has been dropped
            bool placedPiece = false;

            // move down until impact
            for (int y = 1; y < gridHeight; y++)
            {

                if (PiecePlacementGrid[y, Column] == 0)
                {
                    // mark piece on array
                    PiecePlacementGrid[(y), Column] = playerCode;

                    // clear previous piece on array
                    PiecePlacementGrid[(y - 1), Column] = 0;

                    // changed the marked piece as true
                    placedPiece = true;
                }
            }

            // return if the peice was able to be dropped at the space
            return placedPiece;
        }

        public static void Reset(ref int[,] PiecePlacementGrid, int playerCode)
        {
            Column = 3;

            PiecePlacementGrid[0, Column] = playerCode;
        }
    }

    class PiecePlacement
    {
        /// <summary> Visual Board Outline for Console </summary>
        public static int[,] Grid =
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        /// <summary> stores which player is currently playing </summary>
        public static int PlayerCode = 1;

    }

    class Game
    {
        public Game()
        {
            // initiaze the classes
            Screen.Setup();

            // main game loop
            while (true)
            {
                Screen.Update(PiecePlacement.Grid);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    PeiceDrop.Move(true, PiecePlacement.PlayerCode, ref PiecePlacement.Grid);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    PeiceDrop.Move(false, PiecePlacement.PlayerCode, ref PiecePlacement.Grid);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (PeiceDrop.Release(ref PiecePlacement.Grid, PiecePlacement.PlayerCode))
                    {

                        if (Logic.GameOver(PiecePlacement.Grid, PeiceDrop.Column))
                        {
                            Screen.Update(PiecePlacement.Grid);

                            string outputText = "Game Over! Well done " + PiecePlacement.PlayerCode.ToString();

                            GlobalFunctions.WriteAt(outputText, 3, 8);
                            break;
                        }

                        if (PiecePlacement.PlayerCode == 1)
                        {
                            PiecePlacement.PlayerCode = 2;
                            PeiceDrop.Reset(ref PiecePlacement.Grid, PiecePlacement.PlayerCode);

                        }
                        else
                        {
                            PiecePlacement.PlayerCode = 1;
                            PeiceDrop.Reset(ref PiecePlacement.Grid, PiecePlacement.PlayerCode);
                        }

                    }

                }
                else if (key == ConsoleKey.Escape)
                {
                    break;
                }
            }

            Console.ReadKey();
        }
    }
}
