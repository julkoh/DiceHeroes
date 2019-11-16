using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    ArrayList tiles=new ArrayList();
    [SerializeField] private Transform Character;
    public GameObject prefabTile;
    
    
    void OnGUI()
    {
        //GUI.Box(new Rect(250, 500, 100, 100), "N2");
    }
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Character, new Vector3(0, 0), Quaternion.identity);
        for (int i=0; i<5; i++){
            Tile tile = new Tile();
            tile.Create(40+150*i,(Screen.height/2)-50,prefabTile);
            tiles.Add(tile);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
