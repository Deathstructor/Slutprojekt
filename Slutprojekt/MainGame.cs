using Raylib_cs;

public class MainGame
{
    (Square square, bool isMine)[,] squares = new (Square, bool isMine)[20, 20];    // Själva arrayen för rutorna som har dimensionerna 20x20. Säger även om det finns en mina.
    Mines mines = new Mines();
    Logic logic = new Logic();

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

        logic.CountNeighbours();



        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.ColorFromHSV(0, 0, 0.75f));

            // Ritar ut allt som ska ritas ut
            foreach ((Square square, bool isMine) s in squares)
            {
                s.square.Update(s.isMine);
            }

            // Kallar på Click om man vänsterklickar i en ruta
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                logic.Click();
            }
            // Ändra på boolen 'flagged' om man högerklickar i en ruta
            else if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                var pos = logic.ClickCords();

                squares[pos.x, pos.y].square.flagged = !squares[pos.x, pos.y].square.flagged;
            }

            // Ritar ut grannar
            for (int i = 0; i < squares.GetLength(0); i++)
            {
                for (int j = 0; j < squares.GetLength(1); j++)
                {
                    if (squares[i, j].square.clicked && squares[i, j].square.nc == 0 && !squares[i, j].isMine)
                    {
                        logic.ShowNeighbours((i, j));
                    }
                }
            }

            Raylib.EndDrawing();
        }
    }
}