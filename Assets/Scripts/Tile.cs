using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    int height;
    int width;
    GameObject tile;
    public Tile(){
        
    }
    public void Create(int x, int y,GameObject prefabTile)
    {
        height = x;
        width = y;
        tile = Instantiate(prefabTile, new Vector3(height,width),Quaternion.identity);
        tile.transform.parent = GameObject.Find("Canvas").GetComponent<RectTransform>().transform;
    }
}
