using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private EffectInfo effectInfo;

    public string getItemName(){
        return itemName;
    }

    public void setItemName(string name){
        itemName = name;
    }

    public EffectInfo getEffect(){
        return effectInfo;
    }

    public void setEffect(EffectInfo ei){
        effectInfo = ei;
    }
}
