using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapController : MonoBehaviour
{
    List<Tile> tiles=new List<Tile>();
    //Player player;
    public GameObject prefabTile;
    Vector3 position;
    Tile currentTile;
    
    public void OnClick(int x,int y){
        position= new Vector3(x,y);
        foreach(Tile t in tiles) if(t.position==position) currentTile = t;
        SceneManager.LoadScene("CombatScene");
        Debug.Log(position);
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
        Tile start = new Tile();
        start.Create(-300,0,0,prefabTile);
        tiles.Add(start);
        currentTile=start;
        position = start.position;
        for (int i=0; i<3; i++)
        {
            for (int j=1;j<4;j++)
            {
                Tile tile = new Tile();
                tile.Create(-300+150*j,150*(i-1),j,prefabTile);
                tiles.Add(tile);
            }
        }
        Tile Boss=new Tile();
        Boss.Create(300,0,4,prefabTile);
        tiles.Add(Boss);
        foreach (Tile i in tiles)
        {
            foreach (Tile j in tiles)
            {
                // Conditions for linking two different Tiles with one layer
                if (i.getLayer() == j.getLayer()-1)
                {
                    i.Links(j);
                }
            }
            // If not Clickable set low alpha
            //if(currentTile.getLayer()>=i.getLayer()) i.setAlpha(0.1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Tile i in tiles)
        {
            //Change color of lines to see if they are usable
            if(currentTile.getLayer()>i.getLayer() || (currentTile.getLayer()==i.getLayer() && currentTile.position!=i.position)){
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
            if(currentTile.getLayer()>=i.getLayer())
            {
                i.setAlpha(0.3f);
                i.button.interactable = false;
            } 
            else i.setAlpha(1f);
        }
    }

}
