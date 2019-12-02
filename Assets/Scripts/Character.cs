using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Character : MonoBehaviour
{
    private int maxHP;
    private int currentHP;
    private int armor;
    private List<Buff> buffs = new List<Buff>(); //Buffs and debuffs active on the player


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

    public List<Buff> getBuffs(){
        return buffs;
    }

    public void addBuff(Buff b){
        Buff matchingBuff = getMatchingBuff(b);
        if(matchingBuff != null){
            matchingBuff.setStacks(matchingBuff.getStacks()+b.getStacks());
        }else{
            buffs.Add(b);
        }
    }

    public void removeBuff(Buff b){
        buffs.Remove(getMatchingBuff(b));
    }

    public Buff getMatchingBuff(Buff b){
        return buffs.Find(buff => buff.GetType() == b.GetType());
    }

    public Buff getMatchingBuff(string s){
        return buffs.Find(buff => buff.GetType().ToString() == s);
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
        //DEBUFFS
        Transform debuffs = gameObject.transform.Find("Debuffs");
        for(int i = 0; i < debuffs.childCount; i++){
            Buff buff = getMatchingBuff(debuffs.GetChild(i).name);
            if(buff != null && buff.getStacks() > 0){
                Color c = debuffs.GetChild(i).Find("Icon").GetComponent<Image>().color;
                c.a = 1f;
                debuffs.GetChild(i).Find("Icon").GetComponent<Image>().color = c;
                debuffs.GetChild(i).Find("Count").GetComponent<Text>().text = ""+buff.getStacks();
            }else{
                Color c = debuffs.GetChild(i).Find("Icon").GetComponent<Image>().color;
                c.a = 0f;
                debuffs.GetChild(i).Find("Icon").GetComponent<Image>().color = c;
                debuffs.GetChild(i).Find("Count").GetComponent<Text>().text = "";
            }
        }
    }

    public void applyBuffs(){
        int i = 0;
        while(i<buffs.Count){
            buffs[i].applyBuff(this);
            if(buffs[i].getStacks() <= 0){
                removeBuff(buffs[i]);
            }else{
                i++;
            }
        }
        refreshHUD();
    }
}
