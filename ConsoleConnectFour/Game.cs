using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleConnectFour
{
    /// <summary> Structure used for the end of a game </summary>
    struct GameExitStatus
    {
        public string message;
        public bool won;
    }

    /// <summary> Class used to manage drops of pieces in the game </summary>
    class DropSystem
    {
        /// <summary> The current column of the board selected </summary>
        public int Column = 3;

        /// <summary> Move the selected column to the left or right </summary>
        public void Move(bool direction, int playerCode, ref int[,] PiecePlacementGrid)
        {
            // Width of the board
            int gridWidth = PiecePlacementGrid.GetLength(0);

            // Move to the left or right based on the bool direction
            if (direction)
            {
                // Check if reached column bound 
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
                // Check if reached column bound  
                if (Column == 0)
                {
                    Column = (gridWidth - 1);
                }
                else
                {
                    Column--;
                }
            }

            // Mark the correct spot in grid with player code or 0
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

        /// <summary> Set a column specifically </summary>
        public void SpecifyMove(int setColumn)
        {
            Column = setColumn;
        }

        /// <summary> Drop a piece in the selected column </summary>
        public bool Release(ref int[,] PiecePlacementGrid, int playerCode)
        {
            // Hieght of the board
            int gridHeight = PiecePlacementGrid.GetLength(1);

            // Sate if a peice was relased as not always possible
            bool placedPiece = false;

            // Move peice down until impact
            for (int y = 1; y < gridHeight; y++)
            {

                if (PiecePlacementGrid[y, Column] == 0)
                {
                    // Add peice to array with current player code
                    PiecePlacementGrid[(y), Column] = playerCode;

                    // Remove piece on previous location (above)
                    PiecePlacementGrid[(y - 1), Column] = 0;

                    // Has been possible to drop a peice so set flag to true
                    placedPiece = true;

                    // Re-render the screen on each move down
                    GameScreen.Update(PiecePlacementGrid);

                    // Some time delay to give animation effect
                    Thread.Sleep(20);
                }
            }

            // Return if the peice was able to be dropped in the column
            return placedPiece;
        }

        /// <summary> Init class by setting variables to default </summary>
        public DropSystem(ref int[,] PiecePlacementGrid, int playerCode)
        {
            Column = 3;
            PiecePlacementGrid[0, Column] = playerCode;
        }
    }

    /// <summary> Structure used to hold the location of peices in a baord and whos go it is </summary>
    struct PiecePlacement
    {
        /// <summary> Array where the current peices on board are stored </summary>
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

        /// <summary> Stores code of which player is currently playing </summary>
        public int PlayerCode = 1;

    }

    /// <summary> Class for main gameplay </summary>
    class Game
    {
         /// <summary> Paired piece system instance </summary>
        PiecePlacement piecePlacementInstance;

         /// <summary> Paired menu system instance </summary>
        DropSystem dropSystemInstance;

        /// <summary> Stored game mode</summary>
        MenuItem gameMode;

        /// <summary> Paired menu game board instance </summary>
        GameBoard gameBoardInstance;

        /// <summary> Init class by setting vars and starting screen</summary>
        public Game(MenuItem Mode)
        {
            gameMode = Mode;

            piecePlacementInstance = new PiecePlacement();
            dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

            // only need to instanciate bit board if in single player
            if (gameMode == MenuItem.local_single)
            {
                gameBoardInstance = new GameBoard();
            }

            GameScreen.Setup();
        }


        /// <summary> Loop for each interaction with console </summary>
        public GameExitStatus Loop()
        {
            // Main game loop
            while (true)
            {
                // Re-render the screen on each action
                GameScreen.Update(piecePlacementInstance.Grid);

                // The key that was last pressed by the user
                ConsoleKey key = Console.ReadKey(true).Key;

                // Check which key was pressed for approriate action
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
                    // Only need to check if the game has eneded/switch active player if a piece was dropped
                    if (dropSystemInstance.Release(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode))
                    {
                        // also place piece in bit board if in single player for AI
                        if (gameMode == MenuItem.local_single)
                        {
                            gameBoardInstance.PlacePiece(dropSystemInstance.Column);
                        }
                        
                        // Use the game logic to check if a piece was dropped in the board
                        if (GameLogic.GameOver(piecePlacementInstance.Grid, dropSystemInstance.Column))
                        {
                            // Contains the outcome of the game insatnce
                            GameExitStatus returnMessage;
                            returnMessage.won = true;
                            returnMessage.message = "Game Over! Well done " + piecePlacementInstance.PlayerCode.ToString();

                            // Only add to leaderboard if player 1 won
                            if(piecePlacementInstance.PlayerCode == 1 && Client.LoggedIn) {
                                Client.AddPoints(10);
                            }

                            return returnMessage;
                        }

                        // Sinle player selection sequence
                        if (gameMode == MenuItem.local_single)
                        {
                                // set the player code as normal for AI
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

                                // generate a best move using AI
                                int bestMove = GameAI.GenerateMove(gameBoardInstance);

                                // place the best
                                dropSystemInstance.SpecifyMove(bestMove);
                                dropSystemInstance.Release(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                                gameBoardInstance.PlacePiece(dropSystemInstance.Column);
                                    
                                // Use the game logic to check if a piece was dropped in the board (repeated code)
                                if (GameLogic.GameOver(piecePlacementInstance.Grid, dropSystemInstance.Column))
                                {
                                    // Contains the outcome of the game insatnce
                                    GameExitStatus returnMessage;
                                    returnMessage.won = true;
                                    returnMessage.message = "Game Over! Well done AI :)";

                                    return returnMessage;
                                }

                                // set the player code back to normal 
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                        }

                        // Dual player selection sequence
                        if (gameMode == MenuItem.local_dual)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }

                        // Tripple player selection sequence
                        if (gameMode == MenuItem.local_tripple)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 2)
                            {
                                piecePlacementInstance.PlayerCode = 3;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }

                        // Quad player selection sequence
                        if (gameMode == MenuItem.local_quad)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 2)
                            {
                                piecePlacementInstance.PlayerCode = 3;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else if (piecePlacementInstance.PlayerCode == 3)
                            {
                                piecePlacementInstance.PlayerCode = 4;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                            else
                            {
                                piecePlacementInstance.PlayerCode = 1;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);
                            }
                        }
                    }

                }
                else if (key == ConsoleKey.Escape)
                {
                    // Contains the outcome of the game insatnce
                    GameExitStatus returnMessage;
                    returnMessage.won = false;
                    returnMessage.message = "Quit";

                    return returnMessage;
                }

                // check if the board is full
                if (GameLogic.IsFull(piecePlacementInstance.Grid))
                {
                    // Contains the outcome of the game insatnce
                    GameExitStatus returnMessage;
                    returnMessage.won = true;
                    returnMessage.message = "Draw! The board is full.";

                    return returnMessage;
                }
            }
        }
    }
}
