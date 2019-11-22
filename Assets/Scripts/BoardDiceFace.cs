using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardDiceFace : MonoBehaviour , IDragHandler, IEndDragHandler
{
    private DiceFace diceFace;
    private Vector3 basePosition;
    private bool reposition;
    private bool action;
    private int slot;

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
