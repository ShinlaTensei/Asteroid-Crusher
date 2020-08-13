using System;
using Constant;
using UnityEngine;
using UnityEngine.UI;
using Pattern.Interface;
using Pattern.Implement;
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
    [SerializeField] private GameObject chosen;
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
        if (crrInfo.isOwn && !crrInfo.isChosen)
        {
            priceText.text = "Owned";
            priceText.color = Color.green;
            buyButton.gameObject.SetActive(false);
            chosen.SetActive(false);
            chooseButton.gameObject.SetActive(true);
            AllowUpgradeShip();
        }
        else if (crrInfo.isChosen)
        {
            chooseButton.gameObject.SetActive(false);
            chosen.SetActive(true);
        }
        else
        {
            chosen.SetActive(false);
            priceText.text = crrInfo.price.ToString();
        }

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
        thisShip.shipInfo.isChosen = true;
        chosen.SetActive(true);
        buyButton.gameObject.SetActive(false);
        chooseButton.gameObject.SetActive(false);
    }

    private void AllowUpgradeShip()
    {
        
    }
}
