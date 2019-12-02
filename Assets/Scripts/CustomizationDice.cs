using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CustomizationDice : MonoBehaviour
{
    private Dice dice;
    private CustomizationController customizationController;

    public static GameObject Create(Dice d, GameObject dicePrefab, Vector3 pos, CustomizationController cc){
        GameObject go = Instantiate(dicePrefab, pos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("CanvasCustomization").GetComponent<RectTransform>().transform, false);
        go.GetComponent<CustomizationDice>().setDice(d);
        go.GetComponent<CustomizationDice>().setCustomizationController(cc);
        go.GetComponentInChildren<Button>().onClick.AddListener(()=>{
            cc.AddDiceFace(go);
        });
        return go;
    }

    void Awake(){
    }

    public Dice getDice(){
        return dice;
    }

    public void setDice(Dice d){
        dice = d;
    }

    public CustomizationController getCustomizationController(){
        return customizationController;
    }

    public void setCustomizationController(CustomizationController cc){
        customizationController = cc;
    }
}
