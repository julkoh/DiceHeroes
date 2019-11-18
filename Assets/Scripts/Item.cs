using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string itemName;
    private Effect effect;

    public string getItemName(){
        return itemName;
    }

    public void setItemName(string name){
        itemName = name;
    }

    public Effect getEffect(){
        return effect;
    }

    public void setEffect(Effect e){
        effect = e;
    }
}
