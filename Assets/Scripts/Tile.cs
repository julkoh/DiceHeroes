using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Events;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public GameObject tile;
    int layer;
    public List<LineRenderer> lines=new List<LineRenderer>();
    GameObject mapcontroller;
    public Button button;
    public Vector3 position;

    public Tile(){
    }
    public void Create(int x, int y,int layer, GameObject prefabTile)
    {
        this.layer=layer;
        position=new Vector3(x,y);
        tile = Instantiate(prefabTile, position,Quaternion.identity);
        tile.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform,false);
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
