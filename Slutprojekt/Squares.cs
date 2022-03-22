using Raylib_cs;

public class Square
{
    int size = 50;
    Rectangle rec;

    public Square(int x, int y)
    {
        rec = new Rectangle(x, y, size, size);
    }

    public void Draw()
    {
        Raylib.DrawRectangleLinesEx(rec, 1, Color.BLACK);
    }
}