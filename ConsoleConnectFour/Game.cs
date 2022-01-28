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

    class Bitboard
    {
        public int[] height;
        public int counter;
        public long[] moves;
        public long[] bitBoard;
        long TOP = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;

        Random rand;

        public Bitboard()
        {
            height = new int[] { 0, 7, 14, 21, 28, 35, 42 };
            counter = 0;
            moves = new long[42];
            bitBoard = new long[2];

            rand = new Random();
        }

        public void MakeMove(int col)
        {
            long move = 1L << height[col]++; // (1)
            bitBoard[counter & 1] ^= move;  // (2)
            moves[counter++] = col;         // (3)
        }

        public void UndoMove()
        {
            long col = moves[counter--];     // reverses (3)
            long move = 1L << height[col]--; // reverses (1)
            bitBoard[counter & 1] ^= move;  // reverses (2)
        }

        public bool IsWin(int player)
        {
            long currentBitBoard = bitBoard[player];

            int[] directions = { 1, 7, 6, 8 };
            long bb;

            for (int i = 0; i < directions.Length; i++)
            {
                int direction = directions[i];

                bb = currentBitBoard & (currentBitBoard >> direction);
                if ((bb & (bb >> (2 * direction))) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsFull()
        {
            for (int col = 0; col <= 6; col++)
            {
                if ((TOP & (1L << height[col])) == 0)
                { 
                    return false;
                } 
            }

            return true;
        }

        public List<int> PossibleMoves()
        {
            List<int> possibleMoves = new List<int>();
            long TOP = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;

            for (int col = 0; col <= 6; col++)
            {
                if ((TOP & (1L << height[col])) == 0) possibleMoves.Add(col);
            }

            return possibleMoves;
        }

        private int minMaxAlphaBeta(SimulateBoard simBoard, int depth, bool maximizingPlayer)
        {
            if (depth == 0)
            {
                return 0;
            }

            if (simBoard.IsWin(1))
            {
                return depth;
            }

            if (simBoard.IsWin(0))
            {
                return -depth;
            }

            if (simBoard.IsFull())
            {
                return 0;
            }

            if (maximizingPlayer)
            {
                int maxEval = int.MinValue;

                foreach (int move in PossibleMoves())
                {

                    simBoard.PlacePiece(move);

                    int eval = minMaxAlphaBeta(simBoard, depth - 1, false);

                    simBoard.UndoPlacePiece();

                    maxEval = Math.Max(maxEval, eval);
                }

                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;

                foreach (int move in PossibleMoves())
                {
                    simBoard.PlacePiece(move);

                    int eval = minMaxAlphaBeta(simBoard, depth - 1, true);

                    simBoard.UndoPlacePiece();

                    minEval = Math.Min(minEval, eval);
                }

                return minEval;
            }
        }

        public void GenerateMove()
        {
            List<int> bestMoves = new List<int>();

            Console.SetCursorPosition(0, 0);

            SimulateBoard simulateBoard = new SimulateBoard(height, counter, moves, bitBoard);

            foreach (int move in PossibleMoves())
            {
                simulateBoard.PlacePiece(move);

                int test = minMaxAlphaBeta(simulateBoard, 6, true);

                simulateBoard.UndoPlacePiece();

                bestMoves.Add(test);

                

                Console.WriteLine(test);
            }
        }
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

        /// <summary> Paired menu birboard instance </summary>
        Bitboard bitboardInstance;

        /// <summary> Init class by setting vars and starting screen</summary>
        public Game(MenuItem Mode)
        {
            gameMode = Mode;

            piecePlacementInstance = new PiecePlacement();
            dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

            GameScreen.Setup();

            bitboardInstance = new Bitboard();
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
                        bitboardInstance.MakeMove(dropSystemInstance.Column);

                        if (bitboardInstance.IsWin(0))
                        {
                            Console.Clear();
                            Console.WriteLine("0 win");
                        }

                        if(bitboardInstance.IsWin(1))
                        {
                            Console.Clear();
                            Console.WriteLine("1 Win");
                        }

                        // Use the game logic to check if a piece was dropped in the board
                        if (Logic.GameOver(piecePlacementInstance.Grid, dropSystemInstance.Column))
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

                        // Dual player selection sequence
                        if (gameMode == MenuItem.local_dual)
                        {
                            if (piecePlacementInstance.PlayerCode == 1)
                            {
                                piecePlacementInstance.PlayerCode = 2;
                                dropSystemInstance = new DropSystem(ref piecePlacementInstance.Grid, piecePlacementInstance.PlayerCode);

                                bitboardInstance.GenerateMove();
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
            }
        }
    }
}
