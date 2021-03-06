﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public abstract class Effect : MonoBehaviour
{
    private int value;
    private string description;

    public Effect(int val, string desc){
        setValue(val);
        setDescription(desc);
    }

    public int getValue(){
        return value;
    }

    public void setValue(int val){
        value = val;
    }

    public string getDescription(){
        return description;
    }

    public void setDescription(string desc){
        description = desc;
    }

    
    public abstract void apply(Character source, Character target);
}

public class Damage : Effect
{
    public Damage(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        int attackValue = getValue();
        if(source is Player){
            Player player = (Player)source;
            foreach(Item item in player.getItems().FindAll(i => i.getItemEffect().getEffectTypeToAffect() == this.GetType())){
                attackValue = item.getItemEffect().changeEffectValue(attackValue);
            }
        }
        if(target.getArmor() > 0){
            int armorLeft = target.getArmor() > attackValue ? target.getArmor() - attackValue : 0;
            attackValue -= target.getArmor();
            target.setArmor(armorLeft);
        }
        if(attackValue > 0){
            int HPLeft = target.getCurrentHP() > attackValue ? target.getCurrentHP() - attackValue : 0;
            target.setCurrentHP(HPLeft);
        }
        target.refreshHUD();
        target.gameObject.GetComponentInChildren<Animator>().SetTrigger("hurt");
    }
}

public class Shield : Effect
{
    public Shield(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        int val = getValue();
        if(source is Player){
            Player player = (Player)source;
            foreach(Item item in player.getItems().FindAll(i => i.getItemEffect().getEffectTypeToAffect() == this.GetType())){
                val = item.getItemEffect().changeEffectValue(val);
            }
        }
        source.setArmor(source.getArmor() + val);
        source.refreshHUD();
    }
}

public class ShieldDamage : Effect
{
    public ShieldDamage(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        new Damage((int)(source.getArmor()*getValue()), "").apply(source, target);
        target.refreshHUD();
    }
}

public class TrueDamage : Effect
{
    public TrueDamage(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        int HPLeft = target.getCurrentHP() > getValue() ? target.getCurrentHP() - getValue() : 0;
        target.setCurrentHP(HPLeft);
        target.refreshHUD();
    }
}

public class Heal : Effect
{
    public Heal(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        if(source.getCurrentHP()+getValue() <= source.getMaxHP()){
            source.setCurrentHP(source.getCurrentHP()+getValue());
            source.refreshHUD();
        }
    }
}

public class Confuse : Effect
{
    public Confuse(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        if(target is Enemy){
            Instantiate(Resources.Load<GameObject>("Prefabs/RadiationParticle"),target.transform);
            Enemy e = (Enemy)target;
            e.chooseAbility();
            target.refreshHUD();
        }else if(target is Player){
            new AddBuff(new AntiFusion(), getValue(), "").apply(source, target);
        }
    }
}

public class AddBuff : Effect
{

    private Buff buff;

    public AddBuff(Buff b ,int val, string desc): base(val, desc){
        buff = b;
    }

    public override void apply(Character source, Character target){
        buff.setStacks(getValue());
        target.addBuff(buff);
        target.refreshHUD();
    }
}

public class Lava : Effect
{
    public Lava(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        Buff buff = target.getMatchingBuff("Fire");
        if(buff != null){
            int stacks = buff.getStacks();
            buff.applyBuff(target);
            buff.setStacks(stacks*getValue());
            target.refreshHUD();
        }
    }
}