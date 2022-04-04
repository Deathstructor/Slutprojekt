using System;
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

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.GRAY);

            // foreach ((Square square, bool isMine) s in squares)
            // {
            //     s.square.Draw(s.isMine);
            // }

            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Click();
            }
            Raylib.EndDrawing();
        }
    }

    // En tuple som kollar var man klickar och sparar x och y koordinaterna
    (int x, int y) clickCords()
    {
        (int x, int y) pos = (0, 0);
        var mouseCords = Raylib.GetMousePosition();

        for (int i = (int)boxProperties.size; i < Raylib.GetScreenWidth(); i += (int)boxProperties.size)
        {
            if (mouseCords.X < i)
            {
                for (int j = (int)boxProperties.size; j < Raylib.GetScreenHeight(); j += (int)boxProperties.size)
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

        squares[pos.x, pos.y].square.clicked = true;

        if (squares[pos.x, pos.y].square.nc == 0)
        {
            CountNeighbours();
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
                        total = (!squares[sx - 1, sy - 1].isMine && !squares[sx - 1, sy - 1].square.clicked) ? total += 1 : total; // 1
                    }

                    total = (!squares[sx - 1, sy - 0].isMine && !squares[sx - 1, sy - 0].square.clicked) ? total += 1 : total; // 4

                    if (sy != 19)
                    {
                        total = (!squares[sx - 1, sy + 1].isMine && !squares[sx - 1, sy + 1].square.clicked) ? total += 1 : total; // 7
                    }
                }

                if (sx != 19)
                {
                    if (sy != 0)
                    {
                        total = (!squares[sx + 1, sy - 1].isMine && !squares[sx + 1, sy - 1].square.clicked) ? total += 1 : total; // 3
                    }

                    total = (!squares[sx + 1, sy - 0].isMine && !squares[sx + 1, sy - 0].square.clicked) ? total += 1 : total; // 6

                    if (sy != 19)
                    {
                        total = (!squares[sx + 1, sy + 1].isMine && !squares[sx + 1, sy + 1].square.clicked) ? total += 1 : total; // 9
                    }
                }

                if (sy != 0)
                {
                    total = (!squares[sx - 0, sy - 1].isMine && !squares[sx - 0, sy - 1].square.clicked) ? total += 1 : total; // 2
                }

                if (sy != 19)
                {
                    total = (!squares[sx - 0, sy + 1].isMine && !squares[sx - 0, sy + 1].square.clicked) ? total += 1 : total; // 8
                }

                squares[sx, sy].square.nc = total;
            }
        }
    }

    void CountNeighbours()
    {
        // Allt från rad 34 till 82 är till för att kolla rutans "grannar" och se
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