using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private int gold;
    private int diceAmount; //Number of dices
    private List<Dice> dices = new List<Dice>();
    private List<Item> items = new List<Item>();
    private int maxDicesOnBoard; //The maximum number of Dices on the board AKA the number of Dice slots on the board AKA the board's size

    public Player(){}

    public Player(int hp, int gold, int diceAmount, int maxDicesOnBoard)
    {
        setMaxHP(hp);
        setCurrentHP(getMaxHP());
        setGold(gold);
        setDiceAmount(diceAmount);
        setMaxDicesOnBoard(maxDicesOnBoard);
        for (int i = 0; i < diceAmount; i++){
            addDice(new Dice(4));
        }
    }

    void Awake(){
        Player p = GameController.getPlayer();
        setMaxHP(p.getMaxHP());
        setCurrentHP(p.getCurrentHP());
        setGold(p.getGold());
        setDiceAmount(p.getDiceAmount());
        setMaxDicesOnBoard(p.getMaxDicesOnBoard());
        setDices(p.getDices());
        setItems(p.getItems());
    }

    public int getGold(){
        return gold;
    }

    public void setGold(int val){
        gold = val;
    }

    public int getDiceAmount(){
        return diceAmount;
    }

    public void setDiceAmount(int val){
        diceAmount = val;
    }

    public List<Dice> getDices(){
        return dices;
    }

    public void setDices(List<Dice> ds){
        dices = ds;
    }

    public void addDice(Dice d){
        dices.Add(d);
    }

    public List<Item> getItems(){
        return items;
    }

    public void setItems(List<Item> i){
        items = i;
    }

    public void addItem(Item i){
        items.Add(i);
    }

    public int getMaxDicesOnBoard(){
        return maxDicesOnBoard;
    }

    public void setMaxDicesOnBoard(int val){
        maxDicesOnBoard = val;
    }

    public void showItems(){
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Item");
        Dictionary<System.Type,int> displayItems = new Dictionary<System.Type, int>();
        foreach(Item item in items){
            if(displayItems.ContainsKey(item.getItemEffect().GetType())){
                displayItems[item.getItemEffect().GetType()] += item.getItemEffect().getValue();
            }else{
                displayItems.Add(item.getItemEffect().GetType(), item.getItemEffect().getValue());
            }
        }
        List<System.Type> itemTypes = new List<System.Type>(displayItems.Keys);
        for(int i = 0; i < itemTypes.Count; i++){
            GameObject go = Instantiate(prefab, new Vector3(prefab.transform.position.x + 50*i, prefab.transform.position.y),Quaternion.identity);
            go.transform.SetParent(gameObject.transform.Find("Items"),false);
            go.GetComponentInChildren<Image>().sprite = items.Find(item => item.getItemEffect().GetType() == itemTypes[i]).getIcon();
            go.GetComponentInChildren<Text>().text = ""+displayItems[itemTypes[i]];
        }
        
    }
}
