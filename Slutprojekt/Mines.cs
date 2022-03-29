using System;
public class Mines
{
    public (int x, int y)[] MinePos()
    {
        (int x, int y)[] minePosArr = new (int x, int y)[Vars.mineAmount];
        Random rdm = new Random();

        for (int i = 0; i < Vars.mineAmount; i++)
        {
            minePosArr[i] = (rdm.Next(20), rdm.Next(20));
        }
        return minePosArr;
    }
}