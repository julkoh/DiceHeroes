using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerGO;
    private Player player;
    public List<GameObject> enemyTypesGO;

    // Start is called before the first frame update
    void Start()
    {
        player = playerGO.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player getPlayer(){
        return player;
    }
    public GameObject spawnEnemy(){
        return (GameObject)Instantiate(enemyTypesGO[Random.Range(0,enemyTypesGO.Count-1)]);
    }
}
