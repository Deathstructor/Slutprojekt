using Raylib_cs;

enum boxProperties
{
    size = 50
}

public class Square
{
    public bool mineExist = false;
    Rectangle rec;

    public Square(int x, int y)
    {
        rec = new Rectangle(x, y, (float)boxProperties.size, (float)boxProperties.size);
    }

    public void Draw(bool isMine)
    {
        Raylib.DrawRectangleLinesEx(rec, 1, Color.BLACK);

        if (isMine)
        {
            Raylib.DrawCircle((int)(rec.x + (int)boxProperties.size / 2), (int)(rec.y + (int)boxProperties.size / 2), 10, Color.ORANGE);
        }

    }

    public void Click()
    {

    }

    public void CountNeighbours()
    {
        if (isMine)
        {
            nc = -1;
            return;
        }

        var total = 0;

        for (var xo = -1; xo <= 1; xo++)
            {
                for (var yo = -1; yo <= 1; yo++)
                {
                    var i = this.i + xo;
                    var j = this.j + yo;
                    if (i > -1 && i < cols && j > -1 && j < rows)
                    {
                        var neighbor = grid[i][j];
                        if (neighbor.mine)
                        {
                            total++;
                        }
                    }
                }
            }
        this.nc = total;
    }
}