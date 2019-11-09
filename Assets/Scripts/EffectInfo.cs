using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInfo : MonoBehaviour
{
    private Effect effect;
    private int value;
    private string description;

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

    public void applyEffect(Character target){
        effect.apply(target, value);
    }
}
