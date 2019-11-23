using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Events;
using UnityEngine.Events;
using System;


public class Tile : MonoBehaviour
{
    public GameObject tile;
    int layer;
    public List<LineRenderer> lines=new List<LineRenderer>();
    GameObject mapcontroller;
    public Button button;
    public Vector3 position;
    Tiletype tiletype;
    Color c;

    public Tile(){
    }
    public void Create(int x, int y,int layer, GameObject prefabTile)
    {
        this.layer=layer;
        position=new Vector3(x,y);
        tiletype = (Tiletype) UnityEngine.Random.Range(0,Enum.GetNames(typeof(Tiletype)).Length);
        initColor();
        tile = Instantiate(prefabTile, position,Quaternion.identity);
        tile.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform,false);
        tile.transform.Find("Image").GetComponent<Image>().color=c;
        mapcontroller = GameObject.Find("MapHandler");
        button = tile.transform.Find("Image").gameObject.GetComponent<Button>();
        button.onClick.AddListener(()=>{mapcontroller.GetComponent<MapController>().OnClick(x,y);});
        //Debug.Log(x);
    }
    public void Links(Tile t){
        GameObject line = new GameObject();
        line.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform,false);
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.material = new Material (Shader.Find ("Sprites/Default"));
        lr.material.color = Color.red;
        lr.widthMultiplier = 10f;
        lr.sortingOrder=0;
        lr.SetPosition(0,tile.transform.position);
        lr.SetPosition(1,t.tile.transform.position);
        lines.Add(lr);
    }

    public void setAlpha(float alpha){
        Color tempColor = tile.GetComponentInChildren<Image>().color;
        tempColor.a = alpha;
        tile.GetComponentInChildren<Image>().color = tempColor;
    }

    public int getLayer(){
        return layer;
    }
    public void initColor(){
        switch(tiletype){
            case Tiletype.GANG1:
                c = new Color(0,255,0);
            break;
            case Tiletype.GANG2:
                c = new Color(191,0,255);
            break;
            case Tiletype.SHOP:
                c = new Color(255,255,0);
            break;
            case Tiletype.EVENT:
                c = new Color(255,255,255);
            break;
        }
    }
}
public enum Tiletype {
    GANG1, GANG2, SHOP, EVENT
}
