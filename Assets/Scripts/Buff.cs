using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    private int stacks;

    public void setStacks(int i){
        stacks = i;
    }

    public int getStacks(){
        return stacks;
    }

    public abstract void applyBuff(Character c);
}

public class Poison : Buff{
    public override void applyBuff(Character c){
        if(getStacks()*2 >= c.getMaxHP()){
            c.gameObject.GetComponentInChildren<Animator>().gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("poison");
            new TrueDamage(c.getMaxHP(),"").apply(c, c);
        }
    }
}

public class Fire : Buff{
    public override void applyBuff(Character c){
        new Damage(getStacks(),"").apply(c, c);
        c.gameObject.GetComponentInChildren<Animator>().gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("fire");
        setStacks(getStacks()-1);
    }
}

public class Ice : Buff{
    public override void applyBuff(Character c){
        c.gameObject.GetComponentInChildren<Animator>().gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("ice");
        setStacks(getStacks()-1);
    }
}

public class AntiFusion : Buff{
    public override void applyBuff(Character c){
        c.gameObject.GetComponentInChildren<Animator>().gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("radiation");
        setStacks(getStacks()-1);
    }
}