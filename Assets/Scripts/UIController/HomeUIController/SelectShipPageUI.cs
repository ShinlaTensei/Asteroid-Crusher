using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Pattern.Interface;
using UnityEngine;
using UnityEngine.UI;
using Constant;
using Pattern.Implement;

public class SelectShipPageUI : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private GameObject shipInfoPage;
    [SerializeField] private GameObject contentNode;
    [SerializeField] private List<Ship> listShip = new List<Ship>();

    private int currentPage = 0;

    private void Awake()
    {
        for (int i = 0; i < listShip.Count; ++i)
        {
            GameObject shipInfo = Instantiate(shipInfoPage, Vector3.zero, Quaternion.identity,
                contentNode.transform);
            shipInfo.GetComponent<ShipInfoPage>().InitPageData(listShip[i]);
        }
    }

    private void OnEnable()
    {
        money.text = PlayerManager.Instance.UserData.money.ToString();
    }
    
}