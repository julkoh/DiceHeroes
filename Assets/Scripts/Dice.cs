using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    private int maxFaces;
    private List<DiceFace> faces;

    public int getMaxFaces(){
        return maxFaces;
    }

    public List<DiceFace> getFaces(){
        return faces;
    }
}
