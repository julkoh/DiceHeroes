using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceFace : MonoBehaviour
{
    private DiceFaceColor faceColor;
    private Color color;
    private EffectInfo effectInfo;
    //private int value; //The number on the face, giving it its value

public DiceFace()
    {
        faceColor = (DiceFaceColor)Random.Range(0, System.Enum.GetNames(typeof(DiceFaceColor)).Length-1);
        switch(faceColor){
            case DiceFaceColor.NEUTRAL :
                color = new Color(255,255,255,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.FIRE :
                color = new Color(255,0,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.EARTH :
                color = new Color(255,127,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.WATER :
                color = new Color(88, 189, 255,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.LAVA :
                color = new Color(127,0,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ROCK :
                color = new Color(127,64,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ICE :
                color = new Color(0, 105, 176,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.PHYSICAL :
                color = new Color(255,200,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.POISON :
                color = new Color(127,0,255,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.RADIATION :
                color = new Color(0,127,0,1);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
        }
    }
    
    public EffectInfo getEffectInfo(){
        return effectInfo;
    }

    public void applyEffect(Character target){
        effectInfo.applyEffect(target);
    }
}
