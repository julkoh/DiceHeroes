﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public GameController gc;
    private Player player;
    private List<Dice> diceBag; //Dices left in the dice bag of the player
    private List<Dice> boardDices = new List<Dice>(); //Dices on the player's board
    private List<DiceFace> boardDiceFaces = new List<DiceFace>(); //Dice faces on the player's board
    private List<Dice> usedDices = new List<Dice>();
    //private List<DiceFace> usedDiceFaces = new List<DiceFace>();
    private int enemyAmount;
    private List<Enemy> enemies;
    private int activeCharacterID; //Active character number : -1 for the player, >0 for enemies (index in the "enemies" list)
    private Character activeCharacter;

    

    /// <summary>
    /// Picks X dices from the dice bag and add them to the board, X being the size of the board
    /// </summary>
    void drawDices(){
        int i = 0;
        while(i < player.getMaxDicesOnBoard() && diceBag.Count > 0){
            boardDices.Add(diceBag[Random.Range(0,diceBag.Count-1)]);
            i++;
        }
    }

    /// <summary>
    /// Rolls the dices on the board to determine the faces that the player can use
    /// </summary>
    void rollDices(){
        foreach(Dice d in boardDices){
            boardDiceFaces.Add(d.getFaces()[Random.Range(0,d.getMaxFaces()-1)]);
        }
    }
    
    /// <summary>
    /// Starts the current character's turn
    /// </summary>
    void StartTurn(){
        if(activeCharacter == player){
            drawDices();
            rollDices();
        }
    }

    /// <summary>
    /// Sets up the comabt environment
    /// </summary>
    void Initialize(){
        player = gc.getPlayer();
        diceBag = player.getDices();
        enemyAmount = Random.Range(1,2);
        for(int i = 0; i < enemyAmount; i++){
            GameObject go = gc.spawnEnemy();
            enemies.Add(go.GetComponent<Enemy>());
        }
        activeCharacterID = -1;
        activeCharacter = player;
        StartTurn();
    }

    /// <summary>
    /// Discard a dice from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the dice.</param>
    void discardDice(int boardSlotID){
        //Move dice face from board to used
        //usedDiceFaces.Add(boardDiceFaces[boardSlotID]);
        boardDiceFaces.Remove(boardDiceFaces[boardSlotID]);
        //Move dice from board to used
        usedDices.Add(boardDices[boardSlotID]);
        boardDices.Remove(boardDices[boardSlotID]);
    }

    /// <summary>
    /// Discard a face from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the face.</param>
    void discardDiceFace(int boardSlotID){
        //Move dice face from board to used
        //usedDiceFaces.Add(boardDiceFaces[boardSlotID]);
        boardDiceFaces.Remove(boardDiceFaces[boardSlotID]);
    }

    /// <summary>
    /// Discard a dice and its face from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the dice and face.</param>
    void discardDiceAndFace(int boardSlotID){
        discardDiceFace(boardSlotID);
        discardDice(boardSlotID);
    }

    /// <summary>
    /// Apply a dice face's effect on the target, then discard the dice
    /// </summary>
    void useDice(int boardSlotID, Character target){
        boardDiceFaces[boardSlotID].applyEffect(target);
        discardDiceAndFace(boardSlotID);
    }

    /// <summary>
    /// Ends the current character's turn
    /// </summary>
    void EndTurn(){
        if(activeCharacter == player){ //At the end of the player's turn, discard all remaining dices on the board
            for(int i = 0; i < boardDices.Count; i++){
                discardDiceAndFace(i);
            }
        }
        //Trigger "end of turn" event, calling onTurnFinished
    }

    void onTurnFinished(){
        //Select next character
        if(activeCharacterID<enemies.Count-1){
            activeCharacterID++;
            activeCharacter = enemies[activeCharacterID];
            StartTurn();
        }else{
            //Trigger "end of global turn" event, calling onGlobalTurnFinished
        }
    }

    void onGlobalTurnFinished(){
        //Apply on time effects (DoTs, HoTs) for all characters

        //Then start a new global turn
        activeCharacterID = -1;
        activeCharacter = player;
        StartTurn();
    }

}
