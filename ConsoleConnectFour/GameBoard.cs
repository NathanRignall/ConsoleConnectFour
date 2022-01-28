using System.Collections.Generic;

namespace ConsoleConnectFour
{
    /// <summary> Class used for manging the game board state </summary>
    public class GameBoard
    {
        /// <summary> Number of pieces placed </summary>
        public int counter;

        /// <summary> Height of columns in board </summary>
        public readonly int[] height;

        /// <summary> Array of all moves </summary>
        public readonly long[] moves;

        /// <summary> Bit array of boards for players </summary>
        public readonly long[] board;

        /// <summary> Bits used for masking top </summary>
        private long topMarker;

        /// <summary> Init class by setting </summary>
        public GameBoard()
        {
            height = new int[] { 0, 7, 14, 21, 28, 35, 42 };
            counter = 0;
            moves = new long[42];
            board = new long[2];

            topMarker = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;
        }

        /// <summary> Drop a peice on the board give the column </summary>
        public bool PlacePiece(int col)
        {
            if ((topMarker & (1L << height[col])) == 0)
            {
                long move = 1L << height[col]++;
                board[counter & 1] ^= move;
                moves[counter++] = col;

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary> Undo moving the last dropped piece </summary>
        public void UndoPlacePiece()
        {
            long col = moves[--counter];
            long move = 1L << --height[col];
            board[counter & 1] ^= move;
        }

        /// <summary> Check if the board is full </summary>
        public bool IsFull()
        {
            for (int col = 0; col <= 6; col++)
            {
                if ((topMarker & (1L << height[col])) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary> Produce a list of possible moves </summary>
        public List<int> PossibleMoves()
        {
            List<int> possibleMoves = new List<int>();

            for (int col = 0; col <= 6; col++)
            {
                if ((topMarker & (1L << height[col])) == 0) possibleMoves.Add(col);
            }

            return possibleMoves;
        }

        /// <summary> Check if a player has won </summary>
        public bool IsWin(int player)
        {
            long currentBitBoard = board[player];

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
    }
}