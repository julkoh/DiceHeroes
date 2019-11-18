using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
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
        if(target.getArmor() > 0){
            int armorLeft = target.getArmor() > attackValue ? target.getArmor() - attackValue : 0;
            attackValue -= target.getArmor();
            target.setArmor(armorLeft);
        }
        if(attackValue > 0){
            int HPLeft = target.getCurrentHP() > attackValue ? target.getCurrentHP() - attackValue : 0;
            target.setCurrentHP(HPLeft);
        }
    }
}

public class Shield : Effect
{
    public Shield(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        source.setArmor(source.getArmor() + getValue());
    }
}

public class ShieldDamage : Effect
{
    public ShieldDamage(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        new Damage((int)(source.getArmor()*getValue()), "").apply(source, target);
    }
}

public class TrueDamage : Effect
{
    public TrueDamage(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        int HPLeft = target.getCurrentHP() > getValue() ? target.getCurrentHP() - getValue() : 0;
        target.setCurrentHP(HPLeft);
    }
}

public class Heal : Effect
{
    public Heal(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        if(source.getCurrentHP()+getValue() <= source.getMaxHP())
            source.setCurrentHP(source.getCurrentHP()+getValue());
    }
}

public class Confuse : Effect
{
    public Confuse(int val, string desc) : base(val, desc){
    }

    public override void apply(Character source, Character target){
        if(target is Enemy){
            Enemy e = (Enemy)target;
            e.chooseAbilityAndTarget();
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
        }
    }
}