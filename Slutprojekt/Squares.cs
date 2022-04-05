
using Raylib_cs;

enum boxProperties
{
    size = 50,
}

// Varenda rutas egenskaper finns här
public class Square
{
    public int nc = 0;
    public bool clicked = false;
    public bool flagged = false;

    Rectangle rec;

    // Skapar rektanglarna som skapar griden / rutnätet
    public Square(int x, int y)
    {
        rec = new Rectangle(x, y, (float)boxProperties.size, (float)boxProperties.size);
    }

    private void DrawGrid()
    {
        Raylib.DrawRectangleLinesEx(rec, 1, Color.BLACK); // Ritar ut outlines på rektanglar istället för hela rektanglar för att lätt skapa rutnätet
    }

    public void Update(bool isMine)
    {
        Draw(isMine);
        Flag();
        DrawGrid();
    }

    private void Draw(bool isMine)
    {
        if (clicked)
        {
            Raylib.DrawRectangleRec(rec, Raylib.ColorFromHSV(0, 0, 0.50f));

            if (isMine)
            {
                Raylib.DrawCircle((int)(rec.x + (int)boxProperties.size / 2), (int)(rec.y + (int)boxProperties.size / 2), 10, Color.RED); // Ritar ut minorna
            }
            else if (nc > 0) // Ritar ut siffran som anger hur många minor som finns runt den rutan
            {
                if (nc == 1) { Raylib.DrawText(nc.ToString(), (int)rec.x + 23, (int)rec.y + 15, 28, Raylib.ColorFromHSV(240f, 1f, 0.99f)); }
                if (nc == 2) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(120f, 0.99f, 0.49f)); }
                if (nc == 3) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(0, 1f, 0.99f)); }
                if (nc == 4) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(240f, 1f, 0.5f)); }
                if (nc == 5) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(359f, 0.99f, 0.5f)); }
                if (nc == 6) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(180f, 1f, 0.5f)); }
                if (nc == 7) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(0, 0, 0)); }
                if (nc == 8) { Raylib.DrawText(nc.ToString(), (int)rec.x + 18, (int)rec.y + 15, 28, Raylib.ColorFromHSV(0, 0, 0.5f)); }
            }
        }
    }

    private void Flag()
    {
        if (flagged)
        {
            Raylib.DrawTexture(LoadImage.flagImg, (int)rec.x, (int)rec.y, Color.WHITE);
            // Raylib.DrawRectangleRec(rec, Raylib.ColorFromHSV(100f, 1f, 0.5f));
        }
    }
}