using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private int maxFaces;
    private List<DiceFace> faces;

    
    public Dice()
    {
        maxFaces = 4;
        faces = new List<DiceFace>();
        for(int i = 0; i < maxFaces; i++){
            addFace(new DiceFace());
        }
    }

    public int getMaxFaces(){
        return maxFaces;
    }

    public void setMaxFaces(int val){
        maxFaces = val;
    }

    public List<DiceFace> getFaces(){
        return faces;
    }

    public void addFace(DiceFace df){
        faces.Add(df);
    }
}
