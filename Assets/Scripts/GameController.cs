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
    public static List<Tile> tiles=new List<Tile>();
    public static Vector3 position;
    public static Tile currentTile;
    public static bool mapScene=true;
    public static  List<LineRenderer> linesHistory = new List<LineRenderer>();

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
}
