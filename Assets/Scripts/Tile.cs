using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    GameObject tile;
    int layer;
    ArrayList lines=new ArrayList();
    
    public Tile(){

    }
    public void Create(int x, int y,int layer, GameObject prefabTile)
    {
        this.layer=layer;
        tile = Instantiate(prefabTile, new Vector3(x,y),Quaternion.identity);
        tile.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform,false);
    }
    public void Links(Tile t){
        GameObject line = new GameObject();
        line.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform,false);
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.material = new Material (Shader.Find ("Sprites/Default"));
        lr.material.color = Color.red;
        lr.widthMultiplier = 10f;
        lr.sortingOrder=1;
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
}
