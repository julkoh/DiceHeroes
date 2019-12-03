using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public static class GameController
{
    private static Player player = new Player(20,5,6,4);
    private static List<EnemyType> enemyTypes = new List<EnemyType>(){
        EnemyType.BRUTE,
        EnemyType.BANDIT,
        EnemyType.BRIGAND,
        EnemyType.DEALER,
        EnemyType.JUNKIE,
        EnemyType.RACKETEER
    };
    private static int enemyAmount = 1;
    public static List<Tile> tiles = new List<Tile>();
    public static Vector3 position;
    public static Tile currentTile;
    public static bool mapScene=true;
    public static bool shopScene=true;
    public static int karma = 0;
    public static int maxshop = 1;
    public static string nextScene;
    public static  List<LineRenderer> linesHistory = new List<LineRenderer>();
    public static DiceFace nextDiceFaceCustomization = new DiceFace();

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

    public static DiceFace getNextDiceFaceCustomization(){
        return nextDiceFaceCustomization;
    }

    public static void setNextDiceFaceCustomization(DiceFace df){
        nextDiceFaceCustomization = df;
    }

    public static void Reset(){
        tiles.Clear();
        linesHistory.Clear();
        mapScene = true;
        karma = 0;
        enemyTypes.Clear();
    }
}
