using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for console game board </summary>
    class Board
    {
        /// <summary> Visual Board Outline for Console </summary>
        private static string[] boardOutline =
        {
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
        };



        /// <summary> Visual character for an empty board piece </summary>
        private static string boardEmptyPiece = " ";


        /// <summary> Visual character for a board piece </summary>
        private static string boardFilledPiece = "x";


        /// <summary> Visual character for loading board piece</summary>
        private static string boardLoadPiece = "-";


        /// <summary> Contains the current placement of pieces in the board on screen</summary>
        private int[,] boardCurrentPlacement =
        {
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
            };

        /// <summary> Initialize the board and print the board</summary>
        public Board()
        {
            // clears the board of any exiting verbose
            Console.Clear();

            // print each line of grid out on board
            foreach (string i in boardOutline)
            {
                Console.WriteLine(i);
            }
        }

        /// <summary> Places a board piece on the grid from given co-ordinates</summary>
        protected void placePiece(int piece, int x, int y)
        {
            try
            {
                // move cursor position to correct place on grid
                Console.SetCursorPosition(boardConolePlacement[x, y, 0], boardConolePlacement[x, y, 1]);

                // write the piece to grid
                switch (piece)
                {
                    case 0:
                        Console.WriteLine(boardEmptyPiece);
                        break;
                    case 1:
                        Console.Write(boardFilledPiece);
                        break;
                    case 2:
                        Console.Write(boardLoadPiece);
                        break;
                }
            }
            // if the requested value is out of range of console/array
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        /// <summary> fucntion used to update the game board given piece array</summary>
        public void updateBoard(int[,] boardDesiredPlacement)
        {
            // make sure the baord has actually updated
            if (boardCurrentPlacement != boardDesiredPlacement)
            {
                // foreach row in grid
                for (int i = 0; i < boardDesiredPlacement.GetLength(0); i++)
                {
                    // foreach collumn
                    for (int j = 0; j < boardDesiredPlacement.GetLength(1); j++)
                    {
                        // get the cell value
                        int cell = boardDesiredPlacement[i, j];

                        // place the piece on the board
                        placePiece(cell, i, j);
                    }
                }
            }
        }
    }
}
