using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console game board render - Methods are static due to this being used in many places without init</summary>
    class GameScreen
    {
        /// <summary> Visual board outline for console </summary>
        static string[] boardOutline =
            {
                "                                           ",
                "                                           ",
                "                                           ",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "|     |     |     |     |     |     |     |",
                "|     |     |     |     |     |     |     |",
                "+-----+-----+-----+-----+-----+-----+-----+",
                "                                           ",
                "           Console Connect Four            ",
        };

        /// <summary> Visual character for an empty board piece </summary>
        static string[] emptyPiece = {
                "     ",
                "     ",
            };


        /// <summary> Visual character for a board piece </summary>
        static string[] filledPiece = {
                "  x  ",
                " x x ",
            };


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
                {{7,0},{13,0},{19,0},{25,0},{31,0},{37,0},{43,0}},
                {{7,4},{13,4},{19,4},{25,4},{31,4},{37,4},{43,4}},
                {{7,7},{13,7},{19,7},{25,7},{31,7},{37,7},{43,7}},
                {{7,10},{13,10},{19,10},{25,10},{31,10},{37,10},{43,10}},
                {{7,13},{13,13},{19,13},{25,13},{31,13},{37,13},{43,13}},
                {{7,16},{13,16},{19,16},{25,16},{31,16},{37,16},{43,16}},
                {{7,19},{13,19},{19,19},{25,19},{31,19},{37,19},{43,19}},
            };

        /// <summary> Places a board piece on the grid from given co-ordinates</summary>
        protected static void PlacePiece(int piece, int y, int x)
        {
            try
            {
                
                // Write the piece to grid
                switch (piece)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case 1:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case 2:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                }

                // Move cursor position to correct place on grid
                Console.SetCursorPosition(boardConolePlacement[y, x, 0], boardConolePlacement[y, x, 1]);

                if(piece == 0){
                    Console.WriteLine(emptyPiece[0]);
                } else {
                    Console.WriteLine(filledPiece[0]);
                }
                
                
                // Move cursor position to correct place on grid below
                Console.SetCursorPosition(boardConolePlacement[y, x, 0], boardConolePlacement[y, x, 1] + 1);
                if(piece == 0){
                    Console.WriteLine(emptyPiece[1]);
                } else {
                    Console.WriteLine(filledPiece[1]);
                }

                // Set colour back
                Console.ForegroundColor = ConsoleColor.White;
            }
            // If the requested value is out of range of console/array (should not happen)
            catch (ArgumentOutOfRangeException error)
            {
                Console.Clear();
                Console.WriteLine(error.Message);
                Console.ReadKey();
            }
        }

        /// <summary> Initialize the board and print the board</summary>
        public static void Setup()
        {
            // Clears the board of any exiting verbose
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;

            // Height of the board
            int boardHeight = boardOutline.GetLength(0);

            // Print each line of grid out on board
            for (int y = 0; y < boardHeight; y++)
            {
                Console.SetCursorPosition(6, y);
                Console.WriteLine(boardOutline[y]);
            }
        }

        /// <summary> Function used to update the game board given piece array</summary>
        public static void Update(int[,] desiredPiecePlacementGrid)
        {
            // Make sure the baord has actually updated
            if (currentPiecePlacementGrid != desiredPiecePlacementGrid)
            {
                // Foreach row in grid
                for (int x = 0; x < desiredPiecePlacementGrid.GetLength(0); x++)
                {
                    // Foreach collumn
                    for (int y = 0; y < desiredPiecePlacementGrid.GetLength(1); y++)
                    {
                        // Get the cell value
                        int cell = desiredPiecePlacementGrid[y, x];

                        // place the piece on the board
                        PlacePiece(cell, y, x);
                    }
                }
            }
        }
    }
}
