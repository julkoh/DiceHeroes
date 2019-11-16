using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect
{
    public abstract void apply(Character source, Character target, int value);
}

public class Damage : Effect
{
    public override void apply(Character source, Character target, int value){
        if(target.getArmor() > 0){
            int armorLeft = target.getArmor() > value ? target.getArmor() - value : 0;
            value -= target.getArmor();
            target.setArmor(armorLeft);
        }
        if(value > 0)
            target.setCurrentHP(target.getCurrentHP() - value);
    }
}

public class Shield : Effect
{
    public override void apply(Character source, Character target, int value){
        source.setArmor(source.getArmor() + value);
    }
}
