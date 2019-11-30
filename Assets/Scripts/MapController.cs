using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    public GameObject prefabTile;

    
    public void OnClick(int x,int y){
        GameController.position= new Vector3(x,y);
        foreach(LineRenderer lr in GameController.currentTile.lines){
            if (GameController.currentTile.position==lr.GetPosition(0) && GameController.position==lr.GetPosition(1))
            GameController.linesHistory.Add(lr);
        }
        foreach(Tile t in GameController.tiles) 
            if(t.position==GameController.position) {
                GameController.currentTile = t;
                GameController.setTile(GameController.currentTile);
                //GameObject.Find("CameraMap").SetActive(false);
                GameObject.Find("CanvasMap").SetActive(false);
                SceneManager.LoadScene("CombatScene",LoadSceneMode.Additive);
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
        if(!GameController.mapInit)
        {
            GameController.mapInit=true;
            Tile start = new Tile();
            start.Create(-300,0,0,prefabTile);
            GameController.tiles.Add(start);
            GameController.currentTile=start;
            if(GameController.getTile().position == new Vector3(0,0,0) && GameController.getTile().getLayer() == 0){
                GameController.setTile(GameController.currentTile);
                GameController.position = start.position;
            }else{
                GameController.currentTile = GameController.getTile();
                GameController.position = GameController.currentTile.position;
            }
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
                        Object.Destroy(i.lines[removed]);
                        i.lines.RemoveAt(removed);
                    }
                }
                Debug.Log("Count" + i.lines.Count);
            }
        }
    }

    private void ChangedActiveScene(Scene current, Scene next)
    {

    GameObject.Find("CameraMap").SetActive(true);
    GameObject.Find("CanvasMap").SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

        foreach (LineRenderer lr in GameController.linesHistory)
        {
            lr.material.color = new Color(0,255,0,1f);
        }
        foreach (Tile i in GameController.tiles)
        {
            //Change color of lines to see if they are usable
            if(GameController.currentTile.getLayer()>i.getLayer() || (GameController.currentTile.getLayer()==i.getLayer() && GameController.currentTile.position!=i.position)){
                foreach (LineRenderer l in i.lines){
                    l.material.color = new Color(l.material.color.r,l.material.color.g,l.material.color.b,0f);
                }
            }
            else{
                foreach (LineRenderer l in i.lines){
                    l.material.color = new Color(l.material.color.r,l.material.color.g,l.material.color.b,1f);
                }
            }
            //Change the color of active tiles and deactivate them
            if(GameController.currentTile.getLayer()+1!=i.getLayer())
            {
                i.setAlpha(0.3f);
                i.button.interactable = false;
            } 
            else i.setAlpha(1f);
        }
    }

}
