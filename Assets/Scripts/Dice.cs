using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Dice
{
    private int maxFaces;
    private List<DiceFace> faces;

    
    public Dice()
    {
        maxFaces = 4;
        faces = new List<DiceFace>();
        for(int i = 0; i < maxFaces; i++){
            addFace(new DiceFace((DiceFaceColor)new System.Random().Next(3)));
            Thread.Sleep(20);
            //addFace(new DiceFace(DiceFaceColor.EARTH));
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
