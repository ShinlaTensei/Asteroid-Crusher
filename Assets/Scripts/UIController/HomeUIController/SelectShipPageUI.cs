﻿using System.Collections.Generic;
using System.Linq;
using Pattern.Interface;
using UnityEngine;
using UnityEngine.UI;
using Pattern.Implement;

public class SelectShipPageUI : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private GameObject shipInfoPage;
    [SerializeField] private GameObject contentNode;
    [SerializeField] private List<Ship> listShip = new List<Ship>();
    [SerializeField] private GameObject popUpNode;
    [SerializeField] private GameObject buttonCancel;
    [SerializeField] private GameObject buttonConfirm;

    private int currentPage = 0;

    private void Awake()
    {
        for (int i = 0; i < listShip.Count; ++i)
        {
            List<ShipInfo> info;
            Base.SaveLoad.LoadShipInfo(out info, Constant.Path.saveFileName);
            listShip[i].shipInfo = info[i];
            GameObject shipInfo = Instantiate(shipInfoPage, Vector3.zero, Quaternion.identity,
                contentNode.transform);
            shipInfo.GetComponent<ShipInfoPage>().InitPageData(listShip[i]);
        }
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
        if (listShip.Count > 0)
        {
            PlayerManager.Instance.choosingShip = listShip[0].gameObject;
            GameManager.Instance.gameStateMachine.Initialize(new GameBeginState());
        }
        else
        {
            GameManager.Instance.ShowMessage("Không có ship");
        }
    }

    private void GetInfoFromStorage(List<Ship> storage)
    {
        if (storage.Count > 0)
        {
            foreach (var item in storage)
            {
                var result = listShip.Select(ship =>
                {
                    if (ship.name == item.name)
                    {
                        return ship;
                    }

                    return null;
                } );
            }
        }
    }
}