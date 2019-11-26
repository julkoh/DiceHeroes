using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GameController
{
    private static Player player = new Player(20,0,6,4);
    private static List<EnemyType> enemyTypes = new List<EnemyType>(){
        EnemyType.BRUTE
    };
    private static int enemyAmount = 1;
    private static Tile tile = new Tile();

    public static Player getPlayer(){
        return player;
    }

    public static void setPlayer(Player p){
        player = p;
    }

    public static List<EnemyType> getEnemyTypes(){
        return enemyTypes;
    }

    public static void setEnemyTypes(List<EnemyType> et){
        enemyTypes = et;
    }

    public static void addEnemyType(EnemyType et){
        enemyTypes.Add(et);
    }

    public static void clearEnemytypes(){
        enemyTypes.Clear();
    }

    public static int getEnemyAmount(){
        return enemyAmount;
    }

    public static void setEnemyAmount(int i){
        enemyAmount = i;
    }

    public static Tile getTile(){
        return tile;
    }

    public static void setTile(Tile t){
        tile = t;
    }
}
