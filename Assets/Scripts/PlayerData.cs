using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int score;
    public bool isMusic = false;
    public List<Ship> ownShip = new List<Ship>();
    public PlayerData(int money, int score)
    {
        this.money = money;
        this.score = score;
    }
}
[System.Serializable]
public class PlayerSetting
{
    public bool isSound = false;
    public bool isVibrate = false;
    public float volume = 0f;
}
