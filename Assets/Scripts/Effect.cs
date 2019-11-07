using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    public abstract void apply(Character target, int value);
}

public class Damage : Effect
{
    public override void apply(Character target, int value){
        target.setCurrentHP(target.getCurrentHP() - value);
    }
}
