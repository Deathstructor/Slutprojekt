using Raylib_cs;
using System.Numerics;

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

    Vector2 mousePos = Raylib.GetMousePosition();


    while (!Raylib.WindowShouldClose()) {
        // for (int i = 0; i < Raylib.GetScreenWidth(); i += 50)
        // {
        //     for (int j = 0; j < Raylib.GetScreenHeight(); j += 50 )
        //     {
        //         if (mousePos.X < i + 50 && mousePos.X > i && mousePos.Y + 50 < j && mousePos.Y > j && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        //         {
        //             System.Console.WriteLine(mousePos);
        //         }
        //     }
        // }
    
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.GRAY);
        foreach (Square s in squares)
        {
            s.Draw();
        }
        Raylib.EndDrawing();
    }
}