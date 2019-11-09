using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class DiceFace : MonoBehaviour
{
    private DiceFaceColor faceColor;
    private Color color;
    private EffectInfo effectInfo;
    //private int value; //The number on the face, giving it its value

    public DiceFace()
    {
        DiceFaceColor dfc = (DiceFaceColor) new System.Random().Next(Enum.GetNames(typeof(DiceFaceColor)).Length);
        setFaceColor(dfc);
        Thread.Sleep(20);
    }

    public DiceFaceColor getFaceColor(){
        return faceColor;
    }

    public void setFaceColor(DiceFaceColor dfc){
        faceColor = dfc;
        switch(faceColor){
            case DiceFaceColor.NEUTRAL :
                color = new Color32(255,255,255,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.FIRE :
                color = new Color32(255,0,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.EARTH :
                color = new Color32(127,64,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.WATER :
                color = new Color32(88, 189, 255,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.LAVA :
                color = new Color32(127,0,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ROCK :
                color = new Color32(64,0,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ICE :
                color = new Color32(0, 105, 176,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.PHYSICAL :
                color = new Color32(255,200,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.POISON :
                color = new Color32(127,0,255,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.RADIATION :
                color = new Color32(0,127,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(1);
                effectInfo.setEffect(new Damage());
                break;
        }
    }
    
    public Color getColor(){
        return color;
    }

    public void setColor(Color c){
        color = c;
    }

    public EffectInfo getEffectInfo(){
        return effectInfo;
    }

    public void setEffectInfo(EffectInfo ei){
        effectInfo = ei;
    }

    public void applyEffect(Character target){
        effectInfo.applyEffect(target);
    }
}
