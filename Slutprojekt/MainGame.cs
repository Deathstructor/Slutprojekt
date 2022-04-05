using Raylib_cs;

public class MainGame
{
    (Square square, bool isMine)[,] squares = new (Square, bool isMine)[20, 20];    // Själva arrayen för rutorna som har dimensionerna 20x20. Säger även om det finns en mina.
    Mines mines = new Mines();

    public void Game()
    {
        mines.MinePos();

        // Skapar rutnätet / griden
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (var j = 0; j < squares.GetLength(1); j++)
            {
                squares[i, j] = (new Square(i * Raylib.GetScreenWidth() / 20, j * Raylib.GetScreenHeight() / 20), false);
            }
        }

        // Säger mer eller mindre att minorna existerar
        foreach (var item in mines.positions)
        {
            squares[item.x, item.y].isMine = true;
        }

        CountNeighbours();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.ColorFromHSV(0, 0, 0.75f));

            foreach ((Square square, bool isMine) s in squares)
            {
                s.square.Update(s.isMine);
            }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Click();
            }
            else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                var pos = clickCords();

                squares[pos.x, pos.y].square.flagged = !squares[pos.x, pos.y].square.flagged;
            }

            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if (squares[i, j].square.clicked && squares[i, j].square.nc == 0 && !squares[i, j].isMine)
                    {
                        ShowNeighbours((i, j));
                    }
                }
            }

            Raylib.EndDrawing();
        }
    }

    // En tuple som kollar var man klickar och sparar x och y koordinaterna
    (int x, int y) clickCords()
    {
        (int x, int y) pos = (0, 0);
        var mouseCords = Raylib.GetMousePosition();

        for (int i = (int)boxProperties.size; i <= Raylib.GetScreenWidth(); i += (int)boxProperties.size)
        {
            if (mouseCords.X < i)
            {
                for (int j = (int)boxProperties.size; j <= Raylib.GetScreenHeight(); j += (int)boxProperties.size)
                {
                    if (mouseCords.Y < j)
                    {
                        pos = ((i / (int)boxProperties.size) - 1, (j / (int)boxProperties.size) - 1);
                        System.Console.WriteLine(pos.x + "      " + pos.y);
                        return pos;
                    }
                }
            }
        }
        return pos;
    }

    public void Click()
    {
        var pos = clickCords();

        if (!squares[pos.x, pos.y].square.flagged)
        {    
            squares[pos.x, pos.y].square.clicked = true;

            if (squares[pos.x, pos.y].isMine)
            {
                for (int x = 0; x < squares.GetLength(0); x++)
                {
                    for (int y = 0; y < squares.GetLength(1); y++)
                    {
                        squares[x, y].square.clicked = true;
                    }
                }
            }
        }
    }

    void ShowNeighbours((int x, int y) offset)
    {
        for (var xo = -1; xo <= 1; xo++)
        {
            for (var yo = -1; yo <= 1; yo++)
            {
                (int x, int y) c = (offset.x + xo, offset.y + yo);

                if (c.x > -1 && c.x < 20 && c.y > -1 && c.y < 20)
                {
                    if (!squares[c.x, c.y].isMine && !squares[c.x, c.y].square.clicked)
                    {
                        squares[c.x, c.y].square.clicked = true;
                    }
                }
            }
        }
    }

    void CountNeighbours()
    {
        // Allt från rad X till X är till för att kolla rutans "grannar" och se
        // om det finns och var det finns minor och sedan lägga in värdet på antalet
        // minor i variabeln 'nc' (förkortning för "neighbour count"). 'Total' variabeln
        // behövs tekniskt sätt inte, men det blir lättare att läsa och förstå med den.
        for (int sx = 0; sx < squares.GetLength(0); sx++)
        {
            for (int sy = 0; sy < squares.GetLength(1); sy++)
            {
                int total = 0;

                if (sx != 0)
                {
                    if (sy != 0)
                    {
                        total = squares[sx - 1, sy - 1].isMine ? total += 1 : total; // 1
                    }

                    total = squares[sx - 1, sy - 0].isMine ? total += 1 : total; // 4

                    if (sy != 19)
                    {
                        total = squares[sx - 1, sy + 1].isMine ? total += 1 : total; // 7
                    }
                }

                if (sx != 19)
                {
                    if (sy != 0)
                    {
                        total = squares[sx + 1, sy - 1].isMine ? total += 1 : total; // 3
                    }

                    total = squares[sx + 1, sy - 0].isMine ? total += 1 : total; // 6

                    if (sy != 19)
                    {
                        total = squares[sx + 1, sy + 1].isMine ? total += 1 : total; // 9
                    }
                }

                if (sy != 0)
                {
                    total = squares[sx - 0, sy - 1].isMine ? total += 1 : total; // 2
                }

                if (sy != 19)
                {
                    total = squares[sx - 0, sy + 1].isMine ? total += 1 : total; // 8
                }

                squares[sx, sy].square.nc = total;
            }
        }
    }
}