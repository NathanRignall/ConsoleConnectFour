using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleConnectFour
{
    /// <summary> Structure used for the end of a game </summary>
    struct GameExitStatus
    {
        public string message;
        public bool won;
    }

    /// <summary> Class for main gameplay </summary>
    class Game
    {
        /// <summary> Paired board instance </summary>
        GameBoard gameBoardInstance;

         /// <summary> Paired drop selector instance </summary>
        GameDropSelector gameDropSelectorInstance;

        /// <summary> Stored game mode</summary>
        MenuItem gameMode;

        /// <summary> Init class by setting vars and starting screen</summary>
        public Game(MenuItem Mode)
        {
            gameMode = Mode;

            GameScreen.Setup();

            gameBoardInstance = new GameBoard(2);
            gameDropSelectorInstance = new GameDropSelector(7);
        }


        /// <summary> Loop for each interaction with console </summary>
        public GameExitStatus Loop()
        {
            // Main game loop
            while (true)
            {
                // Re-render the screen on each action
                GameScreen.Update(gameBoardInstance, gameDropSelectorInstance);

                // The key that was last pressed by the user
                ConsoleKey key = Console.ReadKey(true).Key;

                // Check which key was pressed for approriate action
                if (key == ConsoleKey.RightArrow)
                {
                    gameDropSelectorInstance.Move(true);
                }
                else if (key == ConsoleKey.LeftArrow)
                {
                    gameDropSelectorInstance.Move(false);
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    // Only need to check if the game has eneded/switch active player if a piece was dropped
                    if (gameBoardInstance.PlacePiece(gameDropSelectorInstance.column))
                    {
                        if (gameBoardInstance.IsWin(0))
                        {
                            Console.Clear();
                            Console.WriteLine("0 win");
                        }

                        if(gameBoardInstance.IsWin(1))
                        {
                            Console.Clear();
                            Console.WriteLine("1 Win");
                        }

                        // Use the game logic to check if a piece was dropped in the board
                        if (gameBoardInstance.IsWinCurrent())
                        {
                            // Contains the outcome of the game insatnce
                            GameExitStatus returnMessage;
                            returnMessage.won = true;
                            returnMessage.message = "Game Over! Well done " + gameBoardInstance.GetPlayerCode();

                            // Only add to leaderboard if player 1 won
                            if(gameBoardInstance.GetPlayerCode() == 0 && Client.LoggedIn) {
                                Client.AddPoints(10);
                            }

                            return returnMessage;
                        }
                    }

                }
                else if (key == ConsoleKey.Escape)
                {
                    // Contains the outcome of the game insatnce
                    GameExitStatus returnMessage;
                    returnMessage.won = false;
                    returnMessage.message = "Quit";

                    return returnMessage;
                }
            }
        }
    }
}
