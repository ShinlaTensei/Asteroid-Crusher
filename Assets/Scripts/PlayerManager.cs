using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if ( instance == null )
            {
                instance = new PlayerManager();
            }

            return instance;
        }
    }

    private PlayerData userData;
    public PlayerData UserData => userData;

    private PlayerManager()
    {
        instance = this;
    }

    public void InitData(PlayerData data)
    {
        userData = data;
    }
}
