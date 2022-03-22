using Raylib_cs;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(1000, 1000, "Minesweeper");

game();

void game()
{
    Square[,] squares = new Square[20, 20];

    for (int i = 0; i < squares.GetLength(0); i++)
    {
        for (var j = 0; j < squares.GetLength(1); j++)
        {
            squares[i, j] = new Square(i * Raylib.GetScreenWidth() / 20, j * Raylib.GetScreenHeight() / 20);
        }
    }


    while (!Raylib.WindowShouldClose())
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.GRAY);
        foreach (Square s in squares)
        {
            s.Draw();
        }
        Raylib.EndDrawing();
    }
}