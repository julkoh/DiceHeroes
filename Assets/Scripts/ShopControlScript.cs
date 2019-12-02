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
    // Start is called before the first frame update
    void Start()
    {
        moneyAmount = PlayerPrefs.GetInt("moneyAmount");
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
        moneyAmount -= 10;
        PlayerPrefs.SetInt("isDiceSold", 1);
        dicePrice.text = "Sold !";
        buyDiceButton.gameObject.SetActive(false);
    }

    public void buyDiceSide()
    {
        moneyAmount -= 5;
        PlayerPrefs.SetInt("isDiceSideSold", 1);
        diceSidePrice.text = "Sold !";
        buyDiceSideButton.gameObject.SetActive(false);
    }

    public void buyBonusItem()
    {
        moneyAmount -= 2;
        PlayerPrefs.SetInt("isBonusItemSold", 1);
        bonusItemPrice.text = "Sold !";
        buyBonusItemButton.gameObject.SetActive(false);
    }

    public void exitShop()
    {
        PlayerPrefs.SetInt("MoneyAmount", moneyAmount);
        SceneManager.LoadScene("MapScene");
    }
}
