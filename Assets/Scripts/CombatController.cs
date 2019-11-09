using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public GameController gc;
    private Player player;
    private List<Dice> diceBag; //Dices left in the dice bag of the player
    private List<Dice> boardDices = new List<Dice>(); //Dices on the player's board
    private List<GameObject> boardDiceFaces = new List<GameObject>(); //Dice faces on the player's board
    private List<Dice> usedDices = new List<Dice>();
    //private List<DiceFace> usedDiceFaces = new List<DiceFace>();
    private int enemyAmount;
    private List<GameObject> enemies = new List<GameObject>();
    private int activeCharacterID; //Active character number : -1 for the player, >0 for enemies (index in the "enemies" list)
    private Character activeCharacter;
    public GameObject diceFacePrefab;

    

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
            //Roll a random face from the dice
            DiceFace df = d.getFaces()[Random.Range(0,d.getMaxFaces()-1)];
            //Instantiate a new BoardDiceFace GameObject
            GameObject go = Instantiate(diceFacePrefab, new Vector3(100*boardDiceFaces.Count, 0, 0), Quaternion.identity);
            go.transform.parent = GameObject.Find("Canvas").GetComponent<RectTransform>().transform;
            //Apply the rolled DiceFace to the GameObject
            DiceFace dfgo = go.GetComponent<DiceFace>();
            dfgo.setFaceColor(df.getFaceColor());
            //Sets the text of the GameObject
            Text tgo =  go.GetComponent<Text>();
            tgo.color = dfgo.getColor();
            tgo.text = dfgo.getFaceColor().ToString();
            //Add reference to GameObject
            boardDiceFaces.Add(go);
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
    /// Sets up the combat environment
    /// </summary>
    void Start(){
        player = gc.getPlayer();
        diceBag = player.getDices();
        enemyAmount = Random.Range(1,2);
        for(int i = 0; i < enemyAmount; i++){
            GameObject go = gc.spawnEnemy();
            enemies.Add(go);
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
        //Move dice from board to used
        Dice d = boardDices[boardSlotID];
        usedDices.Add(d);
        boardDices.Remove(d);
    }

    /// <summary>
    /// Discard a face from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the face.</param>
    void discardDiceFace(int boardSlotID){
        //Move dice face from board to used
        GameObject go = boardDiceFaces[boardSlotID];
        //usedDiceFaces.Add(boardDiceFaces[boardSlotID]);
        boardDiceFaces.Remove(go);
        Destroy(go);
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
        boardDiceFaces[boardSlotID].GetComponent<DiceFace>().applyEffect(target);
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
            activeCharacter = enemies[activeCharacterID].GetComponent<Enemy>();
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
