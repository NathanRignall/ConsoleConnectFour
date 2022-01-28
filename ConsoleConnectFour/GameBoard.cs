using System.Collections.Generic;

namespace ConsoleConnectFour
{
    public class GameBoard
    {
        public int counter;
        public readonly int[] height;
        public readonly long[] moves;
        public readonly long[] board;

        private long topMarker;

        public GameBoard(int players)
        {
            height = new int[] { 0, 7, 14, 21, 28, 35, 42 };
            counter = 0;
            moves = new long[42];
            board = new long[players];

            topMarker = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;
        }

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

        public void UndoPlacePiece()
        {
            long col = moves[--counter];
            long move = 1L << --height[col];
            board[counter & 1] ^= move;
        }

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

        public List<int> PossibleMoves()
        {


            List<int> possibleMoves = new List<int>();

            for (int col = 0; col <= 6; col++)
            {
                if ((topMarker & (1L << height[col])) == 0) possibleMoves.Add(col);
            }

            return possibleMoves;
        }

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

        public bool IsWinCurrent()
        {
            return IsWin(counter & 1);
        }

        public int GetPlayerCode()
        {
            return counter & 1;
        }
    }
}