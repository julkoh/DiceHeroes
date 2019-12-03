using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopControlScript : MonoBehaviour
{

    int moneyAmount;
    int isDiceSold;
    int isDiceSideSold;
    int isBonusItemSold;

    public Text moneyAmountText;
    public Text dicePrice;
    public Text diceSidePrice;
    public Text bonusItemPrice;

    public Button buyDiceButton;
    public Button buyDiceSideButton;
    public Button buyBonusItemButton;

    private string nextScene;
    private DiceFace diceSide;
    private int diceSideCost;
    private int diceFacesAmount;
    private Item item;

    GameObject canvasShop;

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = GameController.getPlayer().getGold();
        canvasShop = GameObject.Find("CanvasShop");
        nextScene = "MapScene";
        DiceFaceColor dfc = DiceFaceColor.NEUTRAL;
        while(dfc == DiceFaceColor.NEUTRAL){
            dfc = (DiceFaceColor)Random.Range(0,System.Enum.GetNames(typeof(DiceFaceColor)).Length);
        }
        diceSide = new DiceFace(dfc,1);
        GameObject.Find("DiceSide").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Effects/effect_"+diceSide.getFaceColor().ToString().ToLower());
        diceSideCost = (int)diceSide.getFaceColor() > 2 ? ((int)diceSide.getFaceColor() > 3 ? 5 : 1) : 2;
        GameObject.Find("DiceSidePrice").GetComponent<Text>().text = diceSideCost+" gold";
        diceFacesAmount = Random.Range(4,9);
        GameObject.Find("DiceFacesAmount").GetComponent<Text>().text = ""+diceFacesAmount;
        item = new Item();
        GameObject.Find("BonusItem").GetComponent<Image>().sprite = item.getIcon();
        GameObject.Find("BonusItemPrice").GetComponent<Text>().text = item.getItemCost()+" gold";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.shopScene)
        {
            GameController.shopScene=false;    
            canvasShop.SetActive(true);
        }
        moneyAmountText.text = moneyAmount.ToString() + " gold";

        isDiceSold = PlayerPrefs.GetInt("IsDiceSold");
        if (moneyAmount >= 10 && isDiceSold == 0)
            buyDiceButton.interactable = true;
        else
            buyDiceButton.interactable = false;

        isDiceSideSold = PlayerPrefs.GetInt("IsDiceSideSold");
        if (moneyAmount >= diceSideCost && isDiceSideSold == 0)
            buyDiceSideButton.interactable = true;
        else
            buyDiceSideButton.interactable = false;

        isBonusItemSold = PlayerPrefs.GetInt("IsBonusItemSold");
        if (moneyAmount >= item.getItemCost() && isBonusItemSold == 0)
            buyBonusItemButton.interactable = true;
        else
            buyBonusItemButton.interactable = false;
    }

    public void buyDice() 
    {
        GameObject.Find("ShopkeeperSprite").GetComponent<Animator>().SetTrigger("selling");
        moneyAmount -= 10;
        PlayerPrefs.SetInt("isDiceSold", 1);
        dicePrice.text = "Sold !";
        buyDiceButton.gameObject.SetActive(false);
        GameController.getPlayer().addDice(new Dice(diceFacesAmount));
    }

    public void buyDiceSide()
    {
        GameObject.Find("ShopkeeperSprite").GetComponent<Animator>().SetTrigger("selling");
        moneyAmount -= diceSideCost;
        PlayerPrefs.SetInt("isDiceSideSold", 1);
        diceSidePrice.text = "Sold !";
        buyDiceSideButton.gameObject.SetActive(false);
        SceneManager.LoadScene("CustomizationScene",LoadSceneMode.Additive);
        canvasShop.SetActive(false);
        GameController.nextScene = "ShopScene";
        GameController.setNextDiceFaceCustomization(diceSide);
    }

    public void buyBonusItem()
    {
        GameObject.Find("ShopkeeperSprite").GetComponent<Animator>().SetTrigger("selling");
        moneyAmount -= item.getItemCost();
        PlayerPrefs.SetInt("isBonusItemSold", 1);
        bonusItemPrice.text = "Sold !";
        buyBonusItemButton.gameObject.SetActive(false);
        GameController.getPlayer().addItem(item);
    }

    public void exitShop()
    {
        GameController.getPlayer().setGold(moneyAmount);
        StartCoroutine(CombatController.PlayAndWait(GameObject.Find("ShopkeeperSprite").GetComponent<Animator>(),"greeting",() => {
            SceneManager.UnloadSceneAsync("ShopScene");
            GameController.mapScene = true;
        }));
    }
}
