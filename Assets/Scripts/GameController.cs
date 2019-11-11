using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> enemyTypesGO;

    public GameObject getPlayer(){
        return player;
    }

    public List<GameObject> getEnemyTypesGO(){
        return enemyTypesGO;
    }

}
