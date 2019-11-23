using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class NewDiceFace : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private DiceFace diceFace;
    private Vector3 basePosition;
    private bool reposition;
    private bool action;

    public static GameObject Create(DiceFace df, GameObject newDiceFacePrefab, Vector3 pos){
        GameObject go = Instantiate(newDiceFacePrefab, pos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
        go.GetComponent<NewDiceFace>().setDiceFace(df);
        go.GetComponent<NewDiceFace>().setBasePosition(go.transform.position);
        go.GetComponentInChildren<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Effects/effect_"+df.getFaceColor().ToString().ToLower()+".png");
        return go;
    }

    void Awake(){
        reposition = true;
        action = true;
    }

    public DiceFace getDiceFace(){
        return diceFace;
    }

    public void setDiceFace(DiceFace df){
        diceFace = df;
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

    public void OnDrag(PointerEventData pointerEventData){
        action = false;
        Vector3 pos = Input.mousePosition;
        pos.z = 100.0f;
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }

    public void OnEndDrag(PointerEventData pointerEventData){
        action = true;
        if(reposition)
            gameObject.transform.position = basePosition;
    }

    void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag("CustomizationDiceFace"))
        {
            reposition = false;
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("CustomizationDiceFace"))
        {
            reposition = true;
        }
    }
}
