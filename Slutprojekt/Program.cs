using System.Threading;
using Raylib_cs;
using System.Numerics;

Raylib.SetTargetFPS(60);
Raylib.InitWindow(1000, 1000, "Minesweeper");

game();

void game()
{
    Mines mines = new Mines();
    mines.MinePos();


    (Square square, bool isMine)[,] squares = new (Square, bool isMine)[20, 20];

    for (int i = 0; i < squares.GetLength(0); i++)
    {
        for (var j = 0; j < squares.GetLength(1); j++)
        {
            squares[i, j] = (new Square(i * Raylib.GetScreenWidth() / 20, j * Raylib.GetScreenHeight() / 20), false);
        }
    }

    foreach (var item in mines.positions)
    {
        squares[item.x, item.y].isMine = true;
    }

    while (!Raylib.WindowShouldClose())
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.GRAY);

        foreach ((Square square, bool isMine) s in squares)
        {
            s.square.Draw(s.isMine);
        }

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            var pos = clickCords();
            squares[pos.x, pos.y].square.Click();
        }

        mines.ShowMines();

        Raylib.EndDrawing();
    }
}

(int x, int y) clickCords()
{
    (int x, int y) pos = (0, 0);
    var mouseCords = Raylib.GetMousePosition();
    int boxSize = 50;

    for (int i = 50; i < Raylib.GetScreenWidth(); i += 50)
    {
        if (mouseCords.X < i)
        {
            for (int j = 50; j < Raylib.GetScreenHeight(); j += 50)
            {
                if (mouseCords.Y < j)
                {
                    pos = (i / boxSize, j / boxSize);
                    System.Console.WriteLine(pos.x + "      " + pos.y);
                    return pos;
                }
            }
        }
    }
    return pos;
}