using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionZone : MonoBehaviour
{
    public CombatController cb;
    private List<GameObject> diceFacesToFusion = new List<GameObject>();

    public List<GameObject> getDicesFacesToFusion(){
        return diceFacesToFusion;
    }

    public void AddDiceFaceToFusion(GameObject df){
        diceFacesToFusion.Add(df);
    }

    public void RemoveDiceFaceToFusion(GameObject df){
        diceFacesToFusion.Remove(df);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("DiceFace"))
        {
            AddDiceFaceToFusion(other.gameObject);
            if(diceFacesToFusion.Count == 2){
                if(cb.combinableDiceFaces(diceFacesToFusion[0],diceFacesToFusion[1])){
                    cb.combineDiceFaces(diceFacesToFusion[0],diceFacesToFusion[1]);
                    diceFacesToFusion = new List<GameObject>();
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.gameObject.CompareTag("DiceFace"))
        {
            RemoveDiceFaceToFusion(other.gameObject);
        }
    }
}
