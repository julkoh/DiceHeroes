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
    private int diceFacesAmount;

    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = GameController.getPlayer().getGold();
        nextScene = "MapScene";
        diceSide = new DiceFace();
        GameObject.Find("DiceSide").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Effects/effect_"+diceSide.getFaceColor().ToString().ToLower());
        diceFacesAmount = Random.Range(4,9);
        GameObject.Find("DiceFacesAmount").GetComponent<Text>().text = ""+diceFacesAmount;
    }

    // Update is called once per frame
    void Update()
    {
        moneyAmountText.text = moneyAmount.ToString() + " gold";

        isDiceSold = PlayerPrefs.GetInt("IsDiceSold");
        if (moneyAmount >= 10 && isDiceSold == 0)
            buyDiceButton.interactable = true;
        else
            buyDiceButton.interactable = false;

        isDiceSideSold = PlayerPrefs.GetInt("IsDiceSideSold");
        if (moneyAmount >= 5 && isDiceSideSold == 0)
            buyDiceSideButton.interactable = true;
        else
            buyDiceSideButton.interactable = false;

        isBonusItemSold = PlayerPrefs.GetInt("IsBonusItemSold");
        if (moneyAmount >= 2 && isBonusItemSold == 0)
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
        moneyAmount -= 5;
        PlayerPrefs.SetInt("isDiceSideSold", 1);
        diceSidePrice.text = "Sold !";
        buyDiceSideButton.gameObject.SetActive(false);
        nextScene = "CustomizationScene";
        GameController.setNextDiceFaceCustomization(diceSide);
    }

    public void buyBonusItem()
    {
        GameObject.Find("ShopkeeperSprite").GetComponent<Animator>().SetTrigger("selling");
        moneyAmount -= 2;
        PlayerPrefs.SetInt("isBonusItemSold", 1);
        bonusItemPrice.text = "Sold !";
        buyBonusItemButton.gameObject.SetActive(false);
    }

    public void exitShop()
    {
        GameController.getPlayer().setGold(moneyAmount);
        StartCoroutine(CombatController.PlayAndWait(GameObject.Find("ShopkeeperSprite").GetComponent<Animator>(),"greeting",() => {
            SceneManager.LoadScene(nextScene);
        }));
    }
}
