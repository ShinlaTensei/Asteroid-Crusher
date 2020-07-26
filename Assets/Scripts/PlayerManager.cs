using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private static PlayerManager instance;
    public event Action<int> OnBuyItem;

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
    public GameObject choosingShip;

    public PlayerSetting setting = new PlayerSetting();
    

    private PlayerManager()
    {
        instance = this;
    }

    ~PlayerManager()
    {
        instance = null;
        if (OnBuyItem?.GetInvocationList() is Action<int>[] invocationList)
        {
            foreach (var function in invocationList)
            {
                OnBuyItem -= function;
            }
        }
    }

    public void InitData(PlayerData data)
    {
        userData = data;
    }

    public void BuyItem(int money)
    {
        userData.money -= money;
        OnBuyItem?.Invoke(userData.money);
    }
}
