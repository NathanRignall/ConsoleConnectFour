using System;
using System.Collections.Generic;
using System.Threading;

namespace ConsoleConnectFour
{
    struct gameExit
    {
        public string message;
        public bool won;
    }

    class DropSystem
    {
        public int Column = 3;

        public void Move(bool direction, int playerCode, ref int[,] PiecePlacementGrid)
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

        public bool Release(ref int[,] PiecePlacementGrid, int playerCode)
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

                    // update the screen
                    GameScreen.Update(PiecePlacementGrid);

                    // wait
                    Thread.Sleep(20);
                }
            }

            // return if the peice was able to be dropped at the space
            return placedPiece;
        }

        public void Reset(ref int[,] PiecePlacementGrid, int playerCode)
        {
            Column = 3;
            PiecePlacementGrid[0, Column] = playerCode;
        }
    }

    class PiecePlacement
    {
        /// <summary> Visual Board Outline for Console </summary>
        public int[,] Grid =
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
        public int PlayerCode = 1;

    }

    class Game
    {
        MenuItem mode;
        PiecePlacement piecePlacementInstance;
        DropSystem dropSystemInstance;

        public Game(MenuItem Mode)
        {
            mode = Mode;

            piecePlacementInstance = new PiecePlacement();
            dropSystemInstance = new DropSystem();

            GameScreen.Setup();
            dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

        }

        public gameExit Loop()
        {
            // main game loop
            while (true)
            {
                GameScreen.Update(piecePlacementInstance.Grid);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.RightArrow)
                {
                    dropSystemInstance.Move(true, piecePlacementInstance.PlayerCode, ref piecePlacementInstance.Grid);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    dropSystemInstance.Move(false, piecePlacementInstance.PlayerCode, ref piecePlacementInstance.Grid);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    if (dropSystemInstance.Release(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode))
                    {

                        if (Logic.GameOver(piecePlacementInstance.Grid, dropSystemInstance.Column))
                        {
                            gameExit returnMessage;
                            returnMessage.won = true;
                            returnMessage.message = "Game Over! Well done " + piecePlacementInstance.PlayerCode.ToString();

                            return returnMessage;
                        }

                        // dual player
                        if (mode == MenuItem.dual)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }

                        // tripple player
                        if (mode == MenuItem.tripple)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 2)
                            {
                                piecePlacementInstance.PlayerCode = 3;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }

                        // quad player
                        if (mode == MenuItem.quad)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 2)
                            {
                                piecePlacementInstance.PlayerCode = 3;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 3)
                            {
                                piecePlacementInstance.PlayerCode = 4;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance.Reset(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }
                    }

                }
                else if (key == ConsoleKey.Escape)
                {
                    gameExit returnMessage;
                    returnMessage.won = false;
                    returnMessage.message = "Quit";

                    return returnMessage;
                }
            }
        }
    }
}
