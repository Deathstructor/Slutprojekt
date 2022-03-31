using Raylib_cs;

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

    for (int sx = 0; sx < squares.GetLength(0); sx++)
    {
        for (int sy = 0; sy < squares.GetLength(1); sy++)
        {
            int total = 0;

            if (sx != 0)
            {
                if (sy != 0)
                {
                    total = squares[sx - 1, sy - 1].isMine ? total += 1: total; // 1
                }

                total = squares[sx - 1, sy - 0].isMine ? total += 1: total; // 4
                
                if (sy != 19)
                {
                    total = squares[sx - 1, sy + 1].isMine ? total += 1: total; // 7
                }
            }

            if (sx != 19)
            {
                if (sy != 0)
                {
                    total = squares[sx + 1, sy - 1].isMine ? total += 1: total; // 3
                }
                
                total = squares[sx + 1, sy - 0].isMine ? total += 1: total; // 6
                
                if (sy != 19)
                {
                    total = squares[sx + 1, sy + 1].isMine ? total += 1: total; // 9
                }
            }

            if (sy != 0)
            {
                total = squares[sx - 0, sy - 1].isMine ? total += 1: total; // 2
            }

            if (sy != 19)
            {
                total = squares[sx - 0, sy + 1].isMine ? total += 1: total; // 8
            }
            
            squares[sx, sy].square.nc = total;
        }
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
                    pos = ((i / boxSize) - 1, (j / boxSize) - 1);
                    System.Console.WriteLine(pos.x + "      " + pos.y);
                    return pos;
                }
            }
        }
    }
    return pos;
}