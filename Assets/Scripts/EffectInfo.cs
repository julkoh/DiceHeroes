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

    public int getValue(){
        return value;
    }

    public void applyEffect(Character target){
        effect.apply(target, value);
    }
}
