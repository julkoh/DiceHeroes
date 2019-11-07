using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private int gold;
    private int diceAmount; //Number of dices
    private List<Dice> dices;
    private List<Item> items;

    public List<Dice> getDices(){
        return dices;
    }
}
