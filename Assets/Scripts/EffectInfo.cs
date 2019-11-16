using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo
{
    private Effect effect;
    private int value;
    private string description;

    public EffectInfo(Effect e, int val, string desc){
        setEffect(e);
        setValue(val);
        setDescription(desc);
    }

    public Effect getEffect(){
        return effect;
    }

    public void setEffect(Effect e){
        effect = e;
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

    public void applyEffect(Character source, Character target){
        effect.apply(source, target, value);
    }
}
