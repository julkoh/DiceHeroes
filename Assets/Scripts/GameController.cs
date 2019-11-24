using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameController
{
    private static Player player = new Player(20,0,6,4);
    private static List<Enemy> enemyTypes = new List<Enemy>();

    public static Player getPlayer(){
        return player;
    }

    public static void setPlayer(Player p){
        player = p;
    }

    public static List<Enemy> getEnemyTypes(){
        return enemyTypes;
    }

}
