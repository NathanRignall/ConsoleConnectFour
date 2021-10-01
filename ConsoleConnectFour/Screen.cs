using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleConnectFour
{
    class Screen
    {
        /// <summary> Visual Board Outline for Console </summary>
        private string[] boardOutline =
        {
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
            "[ ][ ][ ][ ][ ][ ][ ]",
        };


        /// <summary> Visual character for a board piece </summary>
        private string boardPiece = "x";


        /// <summary> Visual character for loading board piece</summary>
        private string boardLoadPiece = "-";


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


        /// <summary> Stores desired placement of pieces in the board on screen</summary>
        private int[,] boardDesiredPlacement =
                {
            { 0, 0, 1, 0, 0, 0, 0 },
            { 0, 0, 0, 2, 0, 1, 0 },
            { 0, 0, 0, 0, 0, 0, 0 },
            { 0, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 1, 0, 0 },
            { 0, 0, 2, 0, 0, 0, 0 },
        };


        /// <summary> Array of where pieces are placed on screen</summary>
        private int[,,] boardConolePlacement =
            {
                {{1,0},{4,0},{7,0},{10,0},{13,0},{16,0},{19,0}},
                {{1,1},{4,1},{7,1},{10,1},{13,1},{16,1},{19,1}},
                {{1,2},{4,2},{7,2},{10,2},{13,2},{16,2},{19,2}},
                {{1,3},{4,3},{7,3},{10,3},{13,3},{16,3},{19,3}},
                {{1,4},{4,4},{7,4},{10,4},{13,4},{16,4},{19,4}},
                {{1,5},{4,5},{7,5},{10,5},{13,5},{16,5},{19,5}},
            };

        public Screen()
        {

        }

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

        public void placeBoard()
        {
            Console.Clear();

            foreach (string i in boardOutline)
            {
                Console.WriteLine(i);
            }



        }

        public void updateBoard()
        {
            if (boardCurrentPlacement != boardDesiredPlacement)
            {
                for (int i = 0; i < boardDesiredPlacement.GetLength(0); i++)
                {
                    for (int j = 0; j < boardDesiredPlacement.GetLength(1); j++)
                    {
                        int cell = boardDesiredPlacement[i, j];

                        if (cell == 1)
                        {
                            WriteAt(boardPiece, boardConolePlacement[i, j, 0], boardConolePlacement[i, j, 1]);
                        }
                        else if (cell == 2)
                        {
                            WriteAt(boardLoadPiece, boardConolePlacement[i, j, 0], boardConolePlacement[i, j, 1]);
                        }
                        else
                        {
                            WriteAt(" ", boardConolePlacement[i, j, 0], boardConolePlacement[i, j, 1]);
                        }

                    }
                }
            }
        }
    }
}
