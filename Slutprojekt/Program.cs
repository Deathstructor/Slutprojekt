using Raylib_cs;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(1000, 1000, "Minesweeper");

var callMainGame = new MainGame();
callMainGame.Game();