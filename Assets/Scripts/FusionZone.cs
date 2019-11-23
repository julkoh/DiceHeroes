using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionZone : MonoBehaviour
{
    public CombatController cb;
    private bool active;

    void Awake()
    {
        active = true;
    }

    public bool getActive(){
        return active;
    }

    void OnTriggerStay2D(Collider2D other){
        if (active && other.gameObject.CompareTag("DiceFace")){
            GameObject df = other.gameObject;
            if(df.GetComponent<BoardDiceFace>().getAction()){
                active = false;
                    if(df.transform.position != gameObject.transform.position){
                    cb.AddDiceFaceToFusion(df);
                    df.transform.position = gameObject.transform.position;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("DiceFace"))
        {
            cb.RemoveDiceFaceToFusion(other.gameObject);
            active = true;
        }
    }
}
