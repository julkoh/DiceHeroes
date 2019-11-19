using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    ArrayList tiles=new ArrayList();
    [SerializeField] private Transform Character;
    public GameObject prefabTile;
    int charj = 1;

    void OnGUI()
    {
        //GUI.Box(new Rect(250, 500, 100, 100), "N2");
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Character, new Vector3(0, 0), Quaternion.identity);

        Tile start = new Tile();
        start.Create(-300,0,0,prefabTile);
        tiles.Add(start);
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
            if(charj>=i.getLayer()) i.setAlpha(0.1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
