using System;
using Constant;
using UnityEngine;
using UnityEngine.UI;
using Base.Pattern.Interface;
using Base.Pattern.Implement;
using UnityEngine.SceneManagement;

public class ShipInfoPage : MonoBehaviour
{
    [SerializeField] private Text shipName;
    [SerializeField] private Text numOfCannon;
    [SerializeField] private Image speedLevel;
    [SerializeField] private Image fuelConsumptionLevel;
    [SerializeField] private Image enduranceLevel;
    [SerializeField] private Text priceText;
    [SerializeField] private Image shipModel;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button chooseButton;
    [SerializeField] private Button upNumCannonButton;
    [SerializeField] private Button upSpeedButton;
    [SerializeField] private Button upFuelConsumptionButton;
    [SerializeField] private Button upEnduranceButton;
    public Sprite[] spritesAttributeLevel = new Sprite[6];
    private Ship thisShip;

    public void InitPageData(Ship ship)
    {
        thisShip = ship;
        var crrInfo = ship.shipInfo;
        shipName.text = crrInfo.name;
        numOfCannon.text = crrInfo.numberOfCannon.ToString();
        int levelSpeed = (int) ((crrInfo.speed % crrInfo.originSpeed) /
                                ShipUpgrade.OffsetAttributeLevel);
        speedLevel.sprite = spritesAttributeLevel[levelSpeed];
        int levelFuelConsump = (int) ((crrInfo.fuelConsumption % crrInfo.originFuelConsumption) /
                                      ShipUpgrade.OffsetAttributeLevel);
        fuelConsumptionLevel.sprite = spritesAttributeLevel[levelFuelConsump];
        int levelEndurance = (int) ((crrInfo.endurance % crrInfo.originEndurance) /
                                    ShipUpgrade.OffsetAttributeLevel);
        enduranceLevel.sprite = spritesAttributeLevel[levelEndurance];
        if (crrInfo.isOwn)
        {
            priceText.text = "Owned";
            priceText.color = Color.green;
            buyButton.gameObject.SetActive(false);
            chooseButton.gameObject.SetActive(true);
        }
        else
        {
            priceText.text = crrInfo.price.ToString();
        }
        AllowUpgradeShip();
        shipModel.sprite = ship.shipImage[crrInfo.numberOfCannon - 1];
    }
    
    public void ClickBuyShip()
    {
        int shipMoney = int.Parse(priceText.text);
        if (thisShip.shipInfo.isOwn == false)
        {
            ICommand clickBuy = new ClickBuyShip(thisShip, OnResultBuyShip);
            clickBuy.Execute(shipMoney);
        }
    }

    private void OnResultBuyShip(bool isBuySuccess)
    {
        if (isBuySuccess)
        {
            buyButton.gameObject.SetActive(false);
            chooseButton.gameObject.SetActive(true);
            if (thisShip.shipInfo.isOwn)
            {
                priceText.text = "Owned";
                priceText.color = Color.green;
                AllowUpgradeShip();
            }
        }
    }
    
    public void ClickChoseShip()
    {
        ICommand clickChoseShip = new ClickChoseShip(thisShip, OnClickChooseShip);
        if (clickChoseShip.CanExecute()) clickChoseShip.Execute();
    }

    private void OnClickChooseShip()
    {
        
    }

    private void AllowUpgradeShip()
    {
        if (PlayerManager.Instance.UserData.money >= Const.PRICE_UPGRADE && thisShip.shipInfo.isOwn)
        {
            upNumCannonButton.gameObject.SetActive(true);
            upSpeedButton.gameObject.SetActive(true);
            upFuelConsumptionButton.gameObject.SetActive(true);
            upEnduranceButton.gameObject.SetActive(true);
        }
        else
        {
            upNumCannonButton.gameObject.SetActive(false);
            upSpeedButton.gameObject.SetActive(false);
            upFuelConsumptionButton.gameObject.SetActive(false);
            upEnduranceButton.gameObject.SetActive(false);
        }
    }

    public void ClickUpgradeShip(int type)
    {
        GameManager.Instance.ShowMessage($"Do you want to spent {Const.PRICE_UPGRADE} to upgrade this ship?", () =>
        {
            var command = new ClickUpgradeShip((TypeUpgrade) type, ref thisShip);
            if (command.CanExecute()) command.Execute();
            InitPageData(thisShip);
        });

    }
}
