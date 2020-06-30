using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class SelectShipPageUI : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private Text shipName;
    [SerializeField] private Text numOfCannon;
    public List<Ship> listShip = new List<Ship>();

    private void OnEnable()
    {
        money.text = PlayerManager.Instance.UserData.money.ToString();
        InitPageData(0);
    }


    private void InitPageData(int index)
    {
        shipName.text = listShip[index].shipInfo.name;
        int numCannon = listShip[index].shipInfo.numberOfCannon;
        numOfCannon.text = numCannon.ToString();
    }
}