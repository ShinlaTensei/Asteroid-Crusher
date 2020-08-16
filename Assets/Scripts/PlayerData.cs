using System;

[Serializable]
public class PlayerData
{
    public int money;
    public int score;

    public UserFacebookData facebookData = new UserFacebookData();
    public UserPlayFabData PlayFabData = new UserPlayFabData();
    public bool isLoginToPlayFab = false;
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

[Serializable]
public class UserPlayFabData
{
    public string Id = String.Empty;
}
