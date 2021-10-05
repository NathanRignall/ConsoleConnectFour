using System;

namespace ConsoleConnectFour
{
    /// <summary> Class for game logic </summary>
    class Logic
    {
        /// <summary> Function used to check if a peice is on a connect 4 line </summary>
        static bool checkPiece(int[,] pieceGrid, int currentPieceY, int currentPieceX, int currentPiecePlayer)
        {
            // counters to see if a connect has been made
            int horizontalCounter = 0;
            int verticalCounter = 0;
            int angled1Counter = 0;
            int angled2Counter = 0;

            // bounds for grid
            int gridWidth = pieceGrid.GetLength(0);
            int gridHeight = pieceGrid.GetLength(1);

            // check in the horizontal to the right
            int horizontalRightCheck = currentPieceX;
            while (true)
            {
                horizontalRightCheck++;

                if (horizontalRightCheck >= gridWidth)
                {
                    break;
                }

                if (pieceGrid[currentPieceY, horizontalRightCheck] == currentPiecePlayer)
                {
                    horizontalCounter++;
                }
                else
                {
                    break;
                }
            }


            // check in the horizontal to the left
            int horizontalLeftCheck = currentPieceX;
            while (true)
            {
                horizontalLeftCheck--;

                if (horizontalLeftCheck < 0)
                {
                    break;
                }

                if (pieceGrid[currentPieceY, horizontalLeftCheck] == currentPiecePlayer)
                {
                    horizontalCounter++;
                }
                else
                {
                    break;
                }
            }

            // check in the vertical down
            int verticalDownCheck = currentPieceY;
            while (true)
            {
                verticalDownCheck--;

                if (verticalDownCheck < 0)
                {
                    break;
                }

                if (pieceGrid[verticalDownCheck, currentPieceX] == currentPiecePlayer)
                {
                    verticalCounter++;
                }
                else
                {
                    break;
                }
            }

            // check in the angle 1 Up
            int angled1UpCheckX = currentPieceX;
            int angled1UpCheckY = currentPieceY;
            while (true)
            {
                angled1UpCheckX++;
                angled1UpCheckY++;

                if (angled1UpCheckX >= gridHeight || angled1UpCheckY >= gridWidth)
                {
                    break;
                }

                if (pieceGrid[angled1UpCheckY, angled1UpCheckX] == currentPiecePlayer)
                {
                    angled1Counter++;
                }
                else
                {
                    break;
                }
            }

            // check in the angle 1 Down
            int angled1DownCheckX = currentPieceX;
            int angled1DownCheckY = currentPieceY;
            while (true)
            {
                angled1DownCheckX--;
                angled1DownCheckY--;

                if (angled1DownCheckX < 0 || angled1DownCheckY < 0)
                {
                    break;
                }

                if (pieceGrid[angled1DownCheckY, angled1DownCheckX] == currentPiecePlayer)
                {
                    angled1Counter++;
                }
                else
                {
                    break;
                }
            }

            // check in the angle 2 Up
            int angled2UpCheckX = currentPieceX;
            int angled2UpCheckY = currentPieceY;
            while (true)
            {
                angled2UpCheckX--;
                angled2UpCheckY++;

                if (angled2UpCheckX < 0 || angled2UpCheckY >= gridWidth)
                {
                    break;
                }

                if (pieceGrid[angled2UpCheckY, angled2UpCheckX] == currentPiecePlayer)
                {
                    angled2Counter++;
                }
                else
                {
                    break;
                }
            }

            // check in the angle 2 Down
            int angled2DownCheckX = currentPieceX;
            int angled2DownCheckY = currentPieceY;
            while (true)
            {
                angled2DownCheckX++;
                angled2DownCheckY--;

                if (angled2DownCheckX >= gridWidth || angled2DownCheckY < 0)
                {
                    break;
                }

                if (pieceGrid[angled2DownCheckY, angled2DownCheckX] == currentPiecePlayer)
                {
                    angled2Counter++;
                }
                else
                {
                    break;
                }
            }


            // finally check if the amount is greater than if won
            if (horizontalCounter >= 3 || verticalCounter >= 3 || angled1Counter >= 3 || angled2Counter >= 3)
            {
                return true;
            }

            return false;
        }

        /// <summary> Function to hceck if the game is actually over </summary>
        public bool gameOver(int[,] pieceGrid, int checkColumn)
        {
            // for each row as column known
            for (int y = 0; y < pieceGrid.GetLength(1); y++)
            {
                // get the cell value
                int player = pieceGrid[y, checkColumn];

                // only check piece if not empty
                if (player != 0)
                {
                    // check a few pieces if a connect is on them
                    if (checkPiece(pieceGrid, y, checkColumn, player))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
