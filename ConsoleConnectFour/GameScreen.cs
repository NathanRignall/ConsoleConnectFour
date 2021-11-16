using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console game board </summary>
    class GameScreen
    {
        /// <summary> Visual Board Outline for Console </summary>
        static string[] boardOutline =
        {
            "                     ",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "---------------------",
        };

        /// <summary> Visual character for an empty board piece </summary>
        static string emptyPiece = " ";


        /// <summary> Visual character for a board piece </summary>
        static string filledPiece = "o";


        /// <summary> Contains the current placement of pieces in the board on screen</summary>
        private static int[,] currentPiecePlacementGrid =
        {
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
        };

        /// <summary> Array of where pieces are placed on board</summary>
        private static int[,,] boardConolePlacement =
            {
                {{1,0},{4,0},{7,0},{10,0},{13,0},{16,0},{19,0}},
                {{1,1},{4,1},{7,1},{10,1},{13,1},{16,1},{19,1}},
                {{1,2},{4,2},{7,2},{10,2},{13,2},{16,2},{19,2}},
                {{1,3},{4,3},{7,3},{10,3},{13,3},{16,3},{19,3}},
                {{1,4},{4,4},{7,4},{10,4},{13,4},{16,4},{19,4}},
                {{1,5},{4,5},{7,5},{10,5},{13,5},{16,5},{19,5}},
                {{1,6},{4,6},{7,6},{10,6},{13,6},{16,6},{19,6}},
            };

        /// <summary> Places a board piece on the grid from given co-ordinates</summary>
        protected static void PlacePiece(int piece, int y, int x)
        {
            try
            {
                // move cursor position to correct place on grid
                Console.SetCursorPosition(boardConolePlacement[y, x, 0], boardConolePlacement[y, x, 1]);

                // write the piece to grid
                switch (piece)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(emptyPiece);
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(filledPiece);
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write(filledPiece);
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(filledPiece);
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(filledPiece);
                        break;
                }

                // set colour back
                Console.ForegroundColor = ConsoleColor.White;
            }
            // if the requested value is out of range of console/array
            catch (ArgumentOutOfRangeException error)
            {
                Console.Clear();
                Console.WriteLine(error.Message);
            }
        }

        /// <summary> Initialize the board and print the board</summary>
        public static void Setup()
        {
            // clears the board of any exiting verbose
            Console.CursorVisible = false;
            Console.Title = "Console Connect Four";
            Console.SetWindowSize(79, 42);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.Clear();

            // print each line of grid out on board
            foreach (string boardLine in boardOutline)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(boardLine);
            }
        }

        /// <summary> fucntion used to update the game board given piece array</summary>
        public static void Update(int[,] desiredPiecePlacementGrid)
        {
            // make sure the baord has actually updated
            if (currentPiecePlacementGrid != desiredPiecePlacementGrid)
            {
                // foreach row in grid
                for (int x = 0; x < desiredPiecePlacementGrid.GetLength(0); x++)
                {
                    // foreach collumn
                    for (int y = 0; y < desiredPiecePlacementGrid.GetLength(1); y++)
                    {
                        // get the cell value
                        int cell = desiredPiecePlacementGrid[y, x];

                        // place the piece on the board
                        PlacePiece(cell, y, x);
                    }
                }
            }
        }
    }
}
