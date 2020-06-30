using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int score;
    public bool isMusic = false;
    public PlayerData(int money, int score)
    {
        this.money = money;
        this.score = score;
    }
}
[System.Serializable]
public class ShipProgress
{
    
}
