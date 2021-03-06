﻿using System;
using System.Collections.Generic;
using System.Linq;
using Base.Module;
using Base.Pattern.Interface;
using UnityEngine;
using UnityEngine.UI;
using Base.Pattern.Implement;

public class SelectShipPageUI : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private GameObject shipInfoPage;
    [SerializeField] private GameObject contentNode;
    [SerializeField] private List<Ship> listShip = new List<Ship>();
    [SerializeField] private GameObject popUpNode;
    [SerializeField] private GameObject buttonCancel;
    [SerializeField] private GameObject buttonConfirm;

    private void Awake()
    {
        
    }

    void Start()
    {
        SaveLoad.LoadFromBinary(out List<ShipInfo> info, Constant.Path.shipData);
        for (int i = 0; i < listShip.Count; ++i)
        {
            if (info != null) listShip[i].shipInfo = info[i];
            GameObject shipInfo = Instantiate(shipInfoPage, Vector3.zero, Quaternion.identity,
                contentNode.transform);
            shipInfo.GetComponent<ShipInfoPage>().InitPageData(listShip[i]);
        }
        contentNode.GetComponent<SwipeControll>().InitSwipe();
    }

    private void OnEnable()
    {
        money.text = PlayerManager.Instance.UserData.money.ToString();
        GameManager.Instance.TweenFrom(popUpNode, new Vector3(
            (GetComponent<RectTransform>().rect.width / 2.0f +
             popUpNode.GetComponent<RectTransform>().rect.width / 2.0f) * -1f,
            popUpNode.GetComponent<RectTransform>().anchoredPosition.y));
        GameManager.Instance.TweenFrom(buttonCancel, new Vector3(
            buttonCancel.GetComponent<RectTransform>().anchoredPosition.x,
            (GetComponent<RectTransform>().rect.height / 2.0f +
             buttonCancel.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));
        GameManager.Instance.TweenFrom(buttonConfirm, new Vector3(
            buttonConfirm.GetComponent<RectTransform>().anchoredPosition.x,
            (GetComponent<RectTransform>().rect.height / 2.0f +
             buttonConfirm.GetComponent<RectTransform>().rect.height / 2.0f) * -1f));

        PlayerManager.Instance.OnBuyItem += OnBuyShip;
    }

    void Update()
    {
        
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnBuyItem -= OnBuyShip;
    }

    private void OnDestroy()
    {
        List<ShipInfo> dataToSave = new List<ShipInfo>();
        foreach (var ship in listShip)
        {
            dataToSave.Add(ship.shipInfo);
        }
        SaveLoad.SaveToBinary(dataToSave, Constant.Path.shipData);
    }

    private void OnApplicationQuit()
    {
        List<ShipInfo> dataToSave = new List<ShipInfo>();
        foreach (var ship in listShip)
        {
            dataToSave.Add(ship.shipInfo);
        }
        SaveLoad.SaveToBinary(dataToSave, Constant.Path.shipData);
    }

    public void ClickGoto(GameObject to)
    {
        ICommand clickGoto = new ClickGoTo(gameObject);
        clickGoto.Execute(to);
        List<ShipInfo> data = new List<ShipInfo>();
        for (int i = 0; i < listShip.Count; i++)
        {
            data.Add(listShip[i].shipInfo);
        }
    }

    public void ClickAccept()
    {
        if (PlayerManager.Instance.choosingShip != null)
        {
            GameManager.Instance.gameStateMachine.Initialize(new GameBeginState());
        }
        else
        {
            GameManager.Instance.ShowMessage("Please choose a ship to continue!");
        }
    }

    private void OnBuyShip(int crrMoney)
    {
        money.text = crrMoney.ToString();
        
    }
}