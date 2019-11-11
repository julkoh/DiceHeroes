using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using UnityEngine.EventSystems;
public class DiceFace : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private DiceFaceColor faceColor;
    private Color color;
    private EffectInfo effectInfo;
    //private int value; //The number on the face, giving it its value
    private Vector3 basePosition;
    private bool reposition = true;
    private bool action = true;
    private int slot;

    public DiceFace()
    {
        //DiceFaceColor dfc = (DiceFaceColor) new System.Random().Next(Enum.GetNames(typeof(DiceFaceColor)).Length);
        DiceFaceColor dfc = (DiceFaceColor) new System.Random().Next(4);
        //Debug.Log("Color : "+dfc.ToString());
        setFaceColor(dfc);
        Thread.Sleep(20);
    }

    public DiceFace(DiceFaceColor dfc)
    {
        setFaceColor(dfc);
    }

    public DiceFaceColor getFaceColor(){
        return faceColor;
    }

    public void setFaceColor(DiceFaceColor dfc){
        faceColor = dfc;
        switch(faceColor){
            case DiceFaceColor.NEUTRAL :
                color = new Color32(127,127,127,255);
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
                effectInfo.setValue(3);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ROCK :
                color = new Color32(64,0,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(3);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.ICE :
                color = new Color32(0, 105, 176,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(3);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.PHYSICAL :
                color = new Color32(255,200,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(3);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.POISON :
                color = new Color32(127,0,255,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(3);
                effectInfo.setEffect(new Damage());
                break;
            case DiceFaceColor.RADIATION :
                color = new Color32(0,127,0,255);
                effectInfo = new EffectInfo();
                effectInfo.setValue(3);
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

    public Vector3 getBasePosition(){
        return basePosition;
    }

    public void setBasePosition(Vector3 pos){
        basePosition = pos;
    }

    public bool getAction(){
        return action;
    }

    public int getSlot(){
        return slot;
    }

    public void setSlot(int s){
        slot = s;
    }

    public void OnDrag(PointerEventData pointerEventData){
        action = false;
        gameObject.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        action = true;
        if(reposition)
            gameObject.transform.position = basePosition;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            reposition = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            reposition = true;
        }
    }
}
