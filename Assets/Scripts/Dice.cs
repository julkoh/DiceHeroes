using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;

public class Dice
{
    private List<DiceFace> faces;
    
    public Dice(int faceAmount)
    {
        faces = new List<DiceFace>();
        for(int i = 0; i < faceAmount; i++){
            addFace(new DiceFace((DiceFaceColor)new System.Random().Next(4)));
            Thread.Sleep(20);
        }
    }

    public List<DiceFace> getFaces(){
        return faces;
    }

    public void setFaces(List<DiceFace> dfs){
        faces = dfs;
    }

    public void addFace(DiceFace df){
        faces.Add(df);
    }

    public void removeFace(DiceFace df){
        faces.Remove(df);
    }
}
