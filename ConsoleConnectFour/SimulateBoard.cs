using System;
using System.Collections.Generic;

namespace ConsoleConnectFour
{
	public class SimulateBoard
	{
		int[] columnHeights;
		int moveCounter;
		long[] moves;
		long[] boards;
		long topMarker;

		public SimulateBoard(int[] setHeights, int setCounter, long[] setMoves, long[] setBoards)
		{
			columnHeights = new int[7];
			moveCounter = setCounter;
			moves = new long[42];
			boards = new long[2];

			Array.Copy(setHeights, columnHeights, 7);
			Array.Copy(setMoves, moves, 42);
			Array.Copy(setBoards, boards, 2);

			topMarker = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;
		}

		public void PlacePiece(int col)
		{
			long move = 1L << columnHeights[col]++;
			boards[moveCounter & 1] ^= move;
			moves[moveCounter++] = col;
		}

		public void UndoPlacePiece()
		{
			long col = moves[--moveCounter];
			long move = 1L << --columnHeights[col];
			boards[moveCounter & 1] ^= move; 
		}

		public bool IsWin(int player)
		{
			long currentBitBoard = boards[player];

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
				if ((topMarker & (1L << columnHeights[col])) == 0)
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
				if ((topMarker & (1L << columnHeights[col])) == 0) possibleMoves.Add(col);
			}

			return possibleMoves;
		}
	}

	struct BoardState
    {
		public int[] columnHeights;
		public int moveCounter;
		public long[] moves;
		public long[] boards;
	}

	class BoardSystem
    {
		static public long topMarker = 0b1000000_1000000_1000000_1000000_1000000_1000000_1000000L;

		static public int Score(BoardState state)
        {
			int[] directions = { 1, 7, 6, 8 };
			long bb;

			for (int i = 0; i < directions.Length; i++)
			{
				int direction = directions[i];

				bb = state.boards[1] & (state.boards[1] >> direction);
				if ((bb & (bb >> (2 * direction))) != 0)
				{
					return 44 - state.moveCounter;
				}
			}

			for (int i = 0; i < directions.Length; i++)
			{
				int direction = directions[i];

				bb = state.boards[0] & (state.boards[0] >> direction);
				if ((bb & (bb >> (2 * direction))) != 0)
				{
					return -44 + state.moveCounter;
				}
			}

			return 0;
		}

		static public void Move(BoardState state, int col)
        {
			long move = 1L << state.columnHeights[col]++;
			state.boards[state.moveCounter & 1] ^= move;
			state.moves[state.moveCounter++] = col;
		}

		static public bool Full(BoardState state)
		{
			for (int col = 0; col <= 6; col++)
			{
				if ((topMarker & (1L << state.columnHeights[col])) == 0)
				{
					return false;
				}
			}

			return true;
		}

		static public List<int> PossibleMoves(BoardState state)
		{
			List<int> possibleMoves = new List<int>();

			for (int col = 0; col <= 6; col++)
			{
				if ((topMarker & (1L << state.columnHeights[col])) == 0) possibleMoves.Add(col);
			}

			return possibleMoves;
		}
	}
}

