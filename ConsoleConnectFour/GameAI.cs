using System;
using System.Linq;

namespace ConsoleConnectFour
{
    /// <summary> Class for game AI player - Methods are static due to this being used without init</summary>
	public class GameAI
	{
        /// <summary> Random number generator for multiple moves </summary>
        static Random random = new Random();

        /// <summary> MinMax function for tree node </summary>
        private static int MinMax(int depth, GameBoard board, bool maximizingPlayer)
        {
            // Check if at the leaf node
            if (depth <= 0)
                return 0;

            // Check if AI has won
            if (board.IsWin(1))
                return depth;

            // Check if user has won
            if (board.IsWin(0))
                return -depth;

            // Check if the board is full
            if (board.IsFull())
                return 0;

            // Select best value based on minimisng or maximising
            int bestValue = maximizingPlayer ? -1 : 1;

            // Make move on each column
            foreach (int i in board.PossibleMoves())
            {
                // Place the piece and check it placed
                if (!board.PlacePiece(i))
                    continue;

                // Recusivly run to generate child node
                int eval = MinMax(depth - 1, board, !maximizingPlayer);

                // Evaluate if child is better than others
                bestValue = maximizingPlayer ? Math.Max(bestValue, eval) : Math.Min(bestValue, eval);

                // Undo the dropped peice on board
                board.UndoPlacePiece();
            }

            // Return the optimum value
            return bestValue;
        }

        /// <summary> Generate a position to drop piece </summary>
        public static int GenerateMove(GameBoard board)
        {
            // List to store all the moves and evaluation
            var moves = new System.Collections.Generic.List<Tuple<int, int>>();

            // Evaluate each possible drop
            foreach (int i in board.PossibleMoves())
            {
                // Place the piece and check it placed
                if (!board.PlacePiece(i))
                    continue;

                // Recusivly run to generate child node
                int eval =  MinMax(6, board, false);

                // Add the evaluation to possible moves
                moves.Add(Tuple.Create(i, eval));

                // Undo the dropped peice on board
                board.UndoPlacePiece();
            }

            // Pick the max score
            int maxMoveScore = moves.Max(t => t.Item2);
            var bestMoves = moves.Where(t => t.Item2 == maxMoveScore).ToList();

            // Return the column of the best move
            return bestMoves[random.Next(0, bestMoves.Count)].Item1;
        }
    }
}