using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class BoardDiceFace : MonoBehaviour , IDragHandler, IEndDragHandler
{
    private DiceFace diceFace;
    private Vector3 basePosition;
    private bool reposition;
    private bool action;
    private int slot;

    public static GameObject Create(DiceFace df, GameObject diceFacePrefab, int slot, Vector3 pos){
        GameObject go = Instantiate(diceFacePrefab, pos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("CanvasCombat").GetComponent<RectTransform>().transform, false);
        //Apply the rolled DiceFace to the GameObject
        BoardDiceFace dfgo = go.GetComponent<BoardDiceFace>();
        dfgo.setDiceFace(df);
        dfgo.setBasePosition(go.transform.position);
        dfgo.setSlot(slot);
        //Sets the text of the GameObject
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

    public int getSlot(){
        return slot;
    }

    public void setSlot(int s){
        slot = s;
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
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            if(other.gameObject.CompareTag("FusionZone") && !other.gameObject.GetComponent<FusionZone>().getActive()){
                reposition = true;
            }else{
                reposition = false;
            }
        }
        
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("FusionZone") || other.gameObject.CompareTag("Enemy"))
        {
            reposition = true;
        }
    }
}
