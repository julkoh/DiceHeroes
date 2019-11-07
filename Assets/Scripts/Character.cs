using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public int maxHP;
    private int currentHP;
    private int armor;
    private List<Buff> buffs; //Buffs and debuffs active on the player

    public int getCurrentHP(){
        return currentHP;
    }

    public void setCurrentHP(int hp){
        currentHP = hp;
    }
}
