using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int score;

    [NonSerialized] public UserFacebookData facebookData;
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
[System.Serializable]
public class UserFacebookData
{
    public bool isLoginFacebook = false;
    public string facebookUserName = String.Empty;
}
