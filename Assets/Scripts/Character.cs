using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    private int maxHP;
    private int currentHP;
    private int armor;
    private List<Buff> buffs; //Buffs and debuffs active on the player


    public int getMaxHP(){
        return maxHP;
    }

    public void setMaxHP(int val){
        maxHP = val;
    }

    public int getCurrentHP(){
        return currentHP;
    }

    public void setCurrentHP(int val){
        currentHP = val;
    }

    public int getArmor(){
        return armor;
    }

    public void setArmor(int val){
        armor = val;
    }

    public void refreshHUD(){
        //HP BAR
        Transform health = gameObject.transform.Find("HealthBar");
        GameObject HPBar = health.Find("Bar").gameObject;
        GameObject HPText = health.Find("Text").gameObject;
        HPBar.transform.localScale = new Vector3((float)getCurrentHP()/getMaxHP(),1f);
        HPText.GetComponent<Text>().text = getCurrentHP()+" / "+getMaxHP();
        //ARMOR
        GameObject armor = gameObject.transform.Find("Armor").Find("Count").gameObject;
        armor.GetComponent<Text>().text = ""+getArmor();
    }
}
