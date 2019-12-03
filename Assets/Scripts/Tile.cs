using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    public Tiletype tiletype;

    Color c;

    public Tile(){
    }
    public void Create(int x, int y,int layer, GameObject prefabTile)
    {
        this.layer=layer;
        position=new Vector3(x,y);
        int random = UnityEngine.Random.Range(0,10);
        switch(random)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                tiletype = Tiletype.GANG1;
            break;
            case 4:
            case 5:
            case 6:
            case 7:
                tiletype = Tiletype.GANG2;
            break;
            case 8:
            case 9:
                if(GameController.maxshop>0)
                {
                    tiletype = Tiletype.SHOP;
                    GameController.maxshop--;
                }
                else
                {
                    if (UnityEngine.Random.Range(0,2)==0)
                    {
                        tiletype = Tiletype.GANG1;
                    }
                    else
                    {
                        tiletype = Tiletype.GANG2;
                    }
                }
            break;
        }
        if (layer==4){
            tiletype=Tiletype.BOSS;
        }
        initColor();
        tile = Instantiate(prefabTile, position,Quaternion.identity);
        tile.transform.SetParent(GameObject.Find("CanvasMap").GetComponent<RectTransform>().transform,false);
        tile.transform.Find("Image").GetComponent<Image>().color=c;
        mapcontroller = GameObject.Find("MapHandler");
        button = tile.transform.Find("Image").gameObject.GetComponent<Button>();
        button.onClick.AddListener(()=>{
            mapcontroller.GetComponent<MapController>().OnClick(x,y);
            //Action from type of tile
            if (tiletype==Tiletype.BOSS)
            {
                GameController.setEnemyAmount(1);
            }
            else
            {
                GameController.setEnemyAmount(UnityEngine.Random.Range(1,3));
            }
            switch(tiletype)
            {
                case Tiletype.GANG1:
                    GameController.addEnemyType(EnemyType.BANDIT);
                    GameController.addEnemyType(EnemyType.BRIGAND);
                    GameController.addEnemyType(EnemyType.JUNKIE);
                break;
                case Tiletype.GANG2:
                    GameController.addEnemyType(EnemyType.BRUTE);
                    GameController.addEnemyType(EnemyType.DEALER);
                    GameController.addEnemyType(EnemyType.RACKETEER);
                break;
                case Tiletype.SHOP:
                //add nothing
                break;
                    
                case Tiletype.BOSS:
                if (GameController.karma > 0)
                {
                //add boss Type 1
                GameController.addEnemyType(EnemyType.BOSS1);
                }
                if (GameController.karma < 0)
                {
                //add boss Type 2
                GameController.addEnemyType(EnemyType.BOSS2);
                }
                else
                {
                //add Random boss Type 1,2
                GameController.addEnemyType(EnemyType.BOSS1);
                GameController.addEnemyType(EnemyType.BOSS2);
                }
                break;
            }
        });
        //Debug.Log(x);
    }
    public void Links(Tile t){
        GameObject line = new GameObject();
        line.transform.SetParent(GameObject.Find("CanvasMap").GetComponent<RectTransform>().transform,false);
        LineRenderer lr = line.AddComponent<LineRenderer>();
        lr.material = new Material (Shader.Find ("Sprites/Default"));
        lr.material.color = Color.red;
        lr.widthMultiplier = 0.02f;
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
                //GREEN
                c = new Color(0,255,0);
            break;
            case Tiletype.GANG2:
                //PURPLE
                c = new Color(191,0,255);
            break;
            case Tiletype.SHOP:
                //YELLOW
                c = new Color(255,255,0);
            break;
            case Tiletype.BOSS:
                //WHITE
                c = new Color(255,255,255);
            break;
        }
    }
}
public enum Tiletype {
    GANG1, GANG2, SHOP, BOSS
}
