using System;
using System.Linq;

namespace ConsoleConnectFour
{
	public class GameAI
	{
        static Random random = new Random();

        private static int MinMax(int depth, GameBoard board, bool maximizingPlayer)
        {
            if (depth <= 0)
                return 0;

            if (board.IsWin(1))
                return depth;

            if (board.IsWin(0))
                return -depth;

            if (board.IsFull())
                return 0;


            int bestValue = maximizingPlayer ? -1 : 1;

            foreach (int i in board.PossibleMoves())
            {
                if (!board.PlacePiece(i))
                    continue;

                int v = MinMax(depth - 1, board, !maximizingPlayer);

                bestValue = maximizingPlayer ? Math.Max(bestValue, v) : Math.Min(bestValue, v);

                board.UndoPlacePiece();
            }

            return bestValue;
        }

        public static int GenerateMove(GameBoard board)
        {
            var moves = new System.Collections.Generic.List<Tuple<int, int>>();

            foreach (int i in board.PossibleMoves())
            {
                if (!board.PlacePiece(i))
                    continue;

                moves.Add(Tuple.Create(i, MinMax(6, board, false)));

                board.UndoPlacePiece();
            }

            int maxMoveScore = moves.Max(t => t.Item2);
            var bestMoves = moves.Where(t => t.Item2 == maxMoveScore).ToList();

            return bestMoves[random.Next(0, bestMoves.Count)].Item1;
        }
    }
}

