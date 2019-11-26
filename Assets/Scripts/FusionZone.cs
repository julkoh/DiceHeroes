using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionZone : MonoBehaviour
{
    public CombatController cb;
    private GameObject diceFace;
    private bool active;

    void Awake()
    {
        active = true;
    }

    public bool getActive(){
        return active;
    }

    public void setActive(bool b){
        active = b;
    }

    void OnTriggerStay2D(Collider2D other){
        if (active && other.gameObject.CompareTag("DiceFace")){
            GameObject df = other.gameObject;
            if(df.GetComponent<BoardDiceFace>().getAction()){
                active = false;
                if(df.transform.position != gameObject.transform.position){
                    diceFace = df;
                    cb.AddDiceFaceToFusion(diceFace);
                    diceFace.transform.position = gameObject.transform.position;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject == diceFace)
        {
            cb.RemoveDiceFaceToFusion(other.gameObject);
            active = true;
        }
    }
}
