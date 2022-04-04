using Raylib_cs;

enum boxProperties
{
    size = 50
}

// Varenda rutas egenskaper finns här
public class Square
{
    public int nc = 0;
    public bool clicked = false;

    Rectangle rec;

    // Skapar rektanglarna som skapar griden / rutnätet
    public Square(int x, int y)
    {
        rec = new Rectangle(x, y, (float)boxProperties.size, (float)boxProperties.size);
    }

    public void Draw(bool isMine)
    {
        Raylib.DrawRectangleLinesEx(rec, 1, Color.BLACK); // Ritar ut outlines på rektanglar istället för hela rektanglar för att lätt skapa rutnätet

        if (isMine)
        {
            Raylib.DrawCircle((int)(rec.x + (int)boxProperties.size / 2), (int)(rec.y + (int)boxProperties.size / 2), 10, Color.RED); // Ritar ut minorna
        }
        else if (nc > 0) // Ritar ut siffran som anger hur många minor som finns runt den rutan
        {
            if (nc == 1) { Raylib.DrawText(nc.ToString(), (int)rec.x + 23, (int)rec.y + 15, 28, Color.DARKBLUE); }
            if (nc == 2) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.GREEN); }
            if (nc == 3) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.RED); }
            if (nc == 4) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.DARKPURPLE); }
            if (nc == 5) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.ORANGE); }
            if (nc == 6) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.SKYBLUE); }
            if (nc == 7) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.BLACK); }
            if (nc == 8) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Color.GRAY); }
        }
    }
}