using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int gold;
    private int diceAmount; //Number of dices
    private List<Dice> dices;
    private List<Item> items;
    private int maxDicesOnBoard; //The maximum number of Dices on the board AKA the number of Dice slots on the board AKA the board's size

    
    public int getDiceAmount(){
        return diceAmount;
    }

    public void setDiceAmount(int val){
        diceAmount = val;
    }

    public List<Dice> getDices(){
        return dices;
    }

    public void addDice(Dice d){
        dices.Add(d);
    }

    public List<Item> getItems(){
        return items;
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

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        gold = 0;
        diceAmount = 6;
        dices = new List<Dice>();
        items = new List<Item>();
        maxDicesOnBoard = 4;
        for (int i = 0; i < diceAmount; i++){
            addDice(new Dice());
        }
    }
}
