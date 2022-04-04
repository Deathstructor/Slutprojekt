using System;

// Randomizar positionen för alla minor och sparar positionen i en tuple.
public class Mines
{
    public int mineAmount = 75;
    public (int x, int y)[] positions;

    public void MinePos()
    {
        (int x, int y)[] minePosArr = new (int x, int y)[mineAmount];
        Random rdm = new Random();

        for (int i = 0; i < mineAmount; i++)
        {
            minePosArr[i] = (rdm.Next(20), rdm.Next(20));
        }

        positions = minePosArr;

        foreach (var item in positions)
        {
            Console.WriteLine($"X:{item.x}, Y:{item.y}");
        }
    }
}