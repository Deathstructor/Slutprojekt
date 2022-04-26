using Raylib_cs;

public class Logic
{
    (Square square, bool isMine)[,] squares = new (Square, bool isMine)[20, 20];    // Själva arrayen för rutorna som har dimensionerna 20x20. Säger även om det finns en mina.

    public Logic((Square square, bool isMine)[,] squares_)
    {
        squares = squares_;
    }

    // En tuple som kollar var man klickar och sparar x och y koordinaterna
    public (int x, int y) ClickCords()
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

    // Metod för när man klickar (vänsterklickar)
    public void Click()
    {
        var pos = ClickCords();

        // Kollar om det finns en flagga i rutan och kör koden om det inte finns någon flagga
        if (!squares[pos.x, pos.y].square.flagged)
        {
            squares[pos.x, pos.y].square.clicked = true; // Säger att man har klickat i den rutan

            // Visar hela brädet om man klickar på en mina
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

    // Kod som visar / ritar ut intilliggande rutor om man har klickat i en ruta utan minor runt sig
    public void ShowNeighbours((int x, int y) offset)
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

    public void CountNeighbours()
    {
        // Allt från rad 81 till 129 är till för att kolla rutans "grannar" och se
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