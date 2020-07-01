using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Constant;

public class SelectShipPageUI : MonoBehaviour
{
    [SerializeField] private Text money;
    [SerializeField] private Text shipName;
    [SerializeField] private Text numOfCannon;
    [SerializeField] private Image speedLevel;
    [SerializeField] private Image fuelConsumptionLevel;
    [SerializeField] private Image enduranceLevel;
    [SerializeField] private GameObject shipInfoPage;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject contentNode;
    [SerializeField] private Image shipModel;
    public List<Ship> listShip = new List<Ship>();
    public Sprite[] spritesAttributeLevel = new Sprite[6];

    private int currentPage = 0;

    private void OnEnable()
    {
        money.text = PlayerManager.Instance.UserData.money.ToString();
        InitPageData(currentPage);
    }


    private void InitPageData(int index)
    {
        ShipInfo crrShip = listShip[index].shipInfo;
        shipName.text = crrShip.name;
        int numCannon = crrShip.numberOfCannon;
        numOfCannon.text = numCannon.ToString();
        int levelSpeed = (int) ((crrShip.speed % crrShip.originSpeed) /
                                ShipUpgrade.OffsetAttributeLevel);
        speedLevel.sprite = spritesAttributeLevel[levelSpeed];
        int levelFuelConsump = (int) ((crrShip.fuelConsumption % crrShip.originFuelConsumption) /
                                      ShipUpgrade.OffsetAttributeLevel);
        fuelConsumptionLevel.sprite = spritesAttributeLevel[levelFuelConsump];
        int levelEndurance = (int) ((crrShip.endurance % crrShip.originEndurance) /
                                    ShipUpgrade.OffsetAttributeLevel);
        enduranceLevel.sprite = spritesAttributeLevel[levelEndurance];
        if (crrShip.isOwn)
        {
            priceText.text = "Owned";
            priceText.color = Color.green;
        }
        else
        {
            priceText.text = crrShip.price.ToString();
        }

        shipModel.sprite = crrShip.shipImage[numCannon - 1];
    }

    public void ClickBuyShip()
    {
        int userMoney = PlayerManager.Instance.UserData.money;
        int shipMoney = listShip[currentPage].shipInfo.price;
        if (userMoney >= shipMoney)
        {
            int crrMoney = userMoney - shipMoney;
            PlayerManager.Instance.UserData.money = crrMoney;
        }
        else
        {
            Debug.LogWarning("You don't have enough money to buy this ship");
            GameManager.Instance.ShowMessage(Message.NotEnoughtMoney);
        }
    }

    public void ClickChoseShip()
    {
        GameObject chose = listShip[currentPage].gameObject;
        PlayerManager.Instance.choosingShip = chose;
    }
}