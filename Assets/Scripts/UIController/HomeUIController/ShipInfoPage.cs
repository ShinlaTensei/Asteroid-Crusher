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
    public Sprite[] spritesAttributeLevel = new Sprite[6];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitPageData(Ship ship)
    {
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
        }
        else
        {
            priceText.text = crrInfo.price.ToString();
        }

        shipModel.sprite = crrInfo.shipImage[crrInfo.numberOfCannon - 1];
    }
    
    public void ClickBuyShip()
    {
        Ship thisShip = GetComponent<Ship>();
        ICommand clickBuy = new ClickBuyShip(thisShip);
        int shipMoney = int.Parse(priceText.text);
        if (thisShip.shipInfo.isOwn == false)
        {
            clickBuy.Execute(shipMoney);
        }
    }
    
    public void ClickChoseShip()
    {
        Ship thisShip = GetComponent<Ship>();
        ICommand clickChoseShip = new ClickChoseShip(thisShip);
        if (clickChoseShip.CanExecute()) clickChoseShip.Execute();
    }
}
