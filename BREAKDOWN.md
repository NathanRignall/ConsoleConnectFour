<b>Any class which has a UI also has a screen class, this is used to render in the console</b>

Program.cs - main program entry point uses a state loop

[Game.cs](ConsoleConnectFour/game.cs] - main game loop interfaces with other classes
[GameAI.cs](ConsoleConnectFour/GameAI.cs] - functions for min max AI to decide best move
[GameBoard.cs](ConsoleConnectFour/GameBoard.cs] - class to contain a board state, is very optimised using bit boards (just used in AI for now)
[GameLogic.cs](ConsoleConnectFour/GameLogic.cs] - this class is redundant and can be replaced by GameBoard.cs just is not implamented
[GameScreen.cs](ConsoleConnectFour/GameScreen.cs] - Render functions for game

[Leaderboard.cs](ConsoleConnectFour/Leaderboard.cs] - class for getting the leaderboard from API
[LeaderboardScreen.cs](ConsoleConnectFour/LeaderboardScreen.cs] - used to render the leaderboard in console

[Menu.cs](ConsoleConnectFour/Menu.cs] - whole menu system class
[MenuScreen.cs](ConsoleConnectFour/MenuScreen.cs] - render functions for menu

[Message.cs](ConsoleConnectFour/Message.cs] - whole message system class
[MessageScreen.cs](ConsoleConnectFour/MessageScreen.cs] - render functions for messages (very basic)

[Client.cs](ConsoleConnectFour/Client.cs] - this used for communicating with the API as is a prototype