using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public GameObject prefabTile;
    GameObject canvasMap;

    
    public void OnClick(int x,int y){
        GameController.position= new Vector3(x,y);
        Vector3 inipos = GameController.currentTile.tile.transform.position;
        Tile initile = GameController.currentTile;
        
        foreach(Tile t in GameController.tiles)
        {
            if(t.position==GameController.position)
            {
                GameController.currentTile = t;
                switch(t.tiletype)
                {
                    case Tiletype.GANG1:
                        GameController.karma++;
                        GameObject.Find("Karma").GetComponent<Text>().color+=new Color(0,10,0);
                    break;
                    case Tiletype.GANG2:
                        GameController.karma--;
                        GameObject.Find("Karma").GetComponent<Text>().color+=new Color(19,0,25);
                    break;
                }
                
            }
        }
        foreach(LineRenderer lr in initile.lines)
        {
            if (inipos==lr.GetPosition(0) && GameController.currentTile.tile.transform.position==lr.GetPosition(1)){
                GameController.linesHistory.Add(lr);
            Debug.Log("Added");
            }
        }
        canvasMap.SetActive(false);
        switch(GameController.currentTile.tiletype){
            case Tiletype.GANG1:
            case Tiletype.GANG2:
            case Tiletype.BOSS:
            SceneManager.LoadScene("CombatScene",LoadSceneMode.Additive);
            break;
            case Tiletype.SHOP:
            SceneManager.LoadScene("ShopScene",LoadSceneMode.Additive);
            break;
        }
        
            
    }
    /*public Player GetPlayer(){
        return player;
    }*/
    void OnGUI()
    {
        //GUI.Box(new Rect(250, 500, 100, 100), "N2");
    }
    // Start is called before the first frame update
    void Start()
    {
        canvasMap = GameObject.Find("CanvasMap");
        Tile start = new Tile();
        start.Create(-300,0,0,prefabTile);
        GameController.tiles.Add(start);
        GameController.currentTile=start;
        GameController.position = start.position;
        for (int i=0; i<3; i++)
        {
            for (int j=1;j<4;j++)
            {
                Tile tile = new Tile();
                tile.Create(-300+150*j,150*(i-1),j,prefabTile);
                GameController.tiles.Add(tile);
            }
        }
        Tile boss=new Tile();
        boss.Create(300,0,4,prefabTile);
        GameController.tiles.Add(boss);
        foreach (Tile i in GameController.tiles)
        {
            foreach (Tile j in GameController.tiles)
            {
                // Conditions for linking two different Tiles with one layer
                if (i.getLayer() == j.getLayer()-1)
                {
                    i.Links(j);
                }
            }
            if (i.position!=start.position && i.lines.Count>1)
            {
                //Remove Links
                int ntoremove = UnityEngine.Random.Range(1,i.lines.Count);
                for (int k=0;k<ntoremove;k++)
                {
                    int removed = UnityEngine.Random.Range(0,i.lines.Count);
                    int countEntrance = 0;
                    foreach(Tile t in GameController.tiles)
                    {
                        foreach(LineRenderer l in t.lines)
                        {
                            if (l.GetPosition(1)==i.lines[removed].GetPosition(1))
                            {
                                countEntrance++;
                            }
                        }
                    }
                    if (countEntrance>1) 
                    {
                        Object.Destroy(i.lines[removed]);
                        i.lines.RemoveAt(removed);
                    }
                }
            }
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        if (GameController.mapScene)
        {
            GameController.mapScene=false;
            canvasMap.SetActive(true);
            foreach (Tile i in GameController.tiles)
            {
                //Change color of lines to see if they are usable
                foreach (LineRenderer l in i.lines)
                {
                    l.material.color = new Color(l.material.color.r,l.material.color.g,l.material.color.b,0f);
                }
                
            }
            List<Tile> tmp=new List<Tile>();
            tmp.Add(GameController.currentTile);
            while(tmp.Count!=0)
            {
                Tile t = tmp[0];
                tmp.RemoveAt(0);
                foreach(LineRenderer l in t.lines)
                {
                    l.material.color = new Color(l.material.color.r,l.material.color.g,l.material.color.b,1f);
                    foreach (Tile i in GameController.tiles)
                    {
                        if (i.tile.transform.position==l.GetPosition(1))
                        {
                            tmp.Add(i);
                        }
                    }
                }
            }
            //Change the color of active tiles and deactivate them
            foreach (Tile t in GameController.tiles)
            {  
                t.setAlpha(0.3f);
                t.button.interactable = false;
                foreach(LineRenderer lr in GameController.currentTile.lines)
                {
                    if(lr.GetPosition(1)==t.tile.transform.position)
                    {
                        t.setAlpha(1f);
                        t.button.interactable = true;
                    }
                }
            }
            foreach (LineRenderer lr in GameController.linesHistory)
            {
                lr.material.color = new Color(0,255,0,1f);
            }
        }
    }

}
