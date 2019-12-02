using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class CustomizationDiceFace : MonoBehaviour
{
    private DiceFace diceFace;
    private CustomizationController customizationController;

    public static GameObject Create(DiceFace df, GameObject diceFacePrefab, Vector3 pos, Transform parent, CustomizationController cc){
        GameObject go = Instantiate(diceFacePrefab, pos, Quaternion.identity);
        go.transform.SetParent(parent, false);
        go.GetComponent<CustomizationDiceFace>().setDiceFace(df);
        go.GetComponent<CustomizationDiceFace>().setCustomizationController(cc);
        go.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>("Images/Effects/effect_"+df.getFaceColor().ToString().ToLower());
        go.GetComponentInChildren<Button>().onClick.AddListener(()=>{
            cc.RemoveDiceFace(go);
        });
        return go;
    }

    void Awake(){
    }

    public DiceFace getDiceFace(){
        return diceFace;
    }

    public void setDiceFace(DiceFace df){
        diceFace = df;
    }

    public CustomizationController getCustomizationController(){
        return customizationController;
    }

    public void setCustomizationController(CustomizationController cc){
        customizationController = cc;
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("NewDiceFace"))
        {
            if(other.gameObject.GetComponent<NewDiceFace>().getAction()){
                customizationController.SwapDiceFaces(other.gameObject, gameObject);
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
    }
}
