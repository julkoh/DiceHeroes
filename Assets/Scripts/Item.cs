using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int itemCost;
    private string itemName;
    private ItemEffect itemEffect;
    private Sprite icon;

    public Item(){
        setItemCost(Random.Range(2,6));
        setItemName("");
        int rand = Random.Range(0,2);
        switch(rand){
            case 0:
                setItemEffect(new MoreArmor(1,""));
                setIcon(Resources.Load<Sprite>("Images/armor"));
                break;
            case 1:
                setItemEffect(new MoreDamage(1,""));
                setIcon(Resources.Load<Sprite>("Images/hook"));
                break;
        }
    }

    public Item(string spriteName){
        setItemCost(Random.Range(2,6));
        setItemName("");
        int rand = Random.Range(0,2);
        switch(rand){
            case 0:
                setItemEffect(new MoreArmor(1,""));
                break;
            case 1:
                setItemEffect(new MoreDamage(1,""));
                break;
        }
        setIcon(Resources.Load<Sprite>("Images/"+spriteName));
    }

    public Item(string spriteName, string name){
        setItemCost(Random.Range(2,6));
        setItemName(name);
        int rand = Random.Range(0,2);
        switch(rand){
            case 0:
                setItemEffect(new MoreArmor(1,""));
                break;
            case 1:
                setItemEffect(new MoreDamage(1,""));
                break;
        }
        setIcon(Resources.Load<Sprite>("Images/"+spriteName));
    }

    public Item(string spriteName, int cost, string name){
        setItemCost(cost);
        setItemName(name);
        int rand = Random.Range(0,2);
        switch(rand){
            case 0:
                setItemEffect(new MoreArmor(1,""));
                break;
            case 1:
                setItemEffect(new MoreDamage(1,""));
                break;
        }
        setIcon(Resources.Load<Sprite>("Images/"+spriteName));
    }

    public Item(string spriteName, int cost, string name, ItemEffect e){
        setItemCost(cost);
        setItemName(name);
        setItemEffect(e);
        setIcon(Resources.Load<Sprite>("Images/"+spriteName));
    }

    public int getItemCost(){
        return itemCost;
    }

    public void setItemCost(int c){
        itemCost = c;
    }

    public string getItemName(){
        return itemName;
    }

    public void setItemName(string name){
        itemName = name;
    }

    public ItemEffect getItemEffect(){
        return itemEffect;
    }

    public void setItemEffect(ItemEffect e){
        itemEffect = e;
    }

    public Sprite getIcon(){
        return icon;
    }

    public void setIcon(Sprite i){
        icon = i;
    }
}
