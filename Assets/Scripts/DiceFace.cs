using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    private DiceFaceColor faceColor;
    private Color color;
    private EffectInfo effectInfo;
    private int value;

    public EffectInfo getEffectInfo(){
        return effectInfo;
    }

    public void applyEffect(Character target){
        effectInfo.applyEffect(target);
    }
}
