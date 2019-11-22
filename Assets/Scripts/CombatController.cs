using System; 
using System.Collections.Generic; 
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CombatController : MonoBehaviour
{
    public GameController gc;
    private GameObject player;
    private List<Dice> diceBag; //Dices left in the dice bag of the player
    private Dice[] boardDices; //Dices on the player's board
    private GameObject[] boardDiceFaces; //Dice faces on the player's board
    private List<Dice> usedDices;
    private int enemyAmount;
    private List<GameObject> enemies;
    private int activeCharacterID; //Active character number : -1 for the player, >0 for enemies (index in the "enemies" list)
    private GameObject activeCharacter;
    public GameObject diceFacePrefab;
    public GameObject replayButton;
    private List<GameObject> diceFacesToFusion;

    public GameObject getPlayer(){
        return player;
    }

    // ========================= Combat Management =========================

    /// <summary>
    /// Sets up the combat environment
    /// </summary>
    void Start(){
        player = gc.getPlayer();
        player.GetComponent<Player>().refreshHUD();
        boardDices = new Dice[player.GetComponent<Player>().getMaxDicesOnBoard()];
        boardDiceFaces = new GameObject[player.GetComponent<Player>().getMaxDicesOnBoard()];
        diceBag = player.GetComponent<Player>().getDices();
        usedDices = new List<Dice>();
        enemyAmount = UnityEngine.Random.Range(1,2);
        enemies  = new List<GameObject>();
        for(int i = 0; i < enemyAmount; i++){
            GameObject enemyPrefab = gc.getEnemyTypesGO()[UnityEngine.Random.Range(0,gc.getEnemyTypesGO().Count)];
            GameObject go = Instantiate(enemyPrefab, new Vector3(-200 * enemies.Count, enemyPrefab.transform.position.y, 0), Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
            go.GetComponent<Enemy>().setCombatController(this);
            go.GetComponent<Enemy>().refreshHUD();
            enemies.Add(go);
        }
        activeCharacterID = -1;
        activeCharacter = player.gameObject;
        diceFacesToFusion = new List<GameObject>();
        StartTurn();
    }

    /// <summary>
    /// Ends the combat
    /// </summary>
    void EndCombat(){
        for(int i = 0; i < boardDiceFaces.Length; i++){
            if(boardDiceFaces[i])
                boardDiceFaces[i].SetActive(false);
        }
        GameObject vt = GameObject.Find("VictoryText");
        if(player.GetComponent<Player>().getCurrentHP() <= 0){
            vt.GetComponent<Text>().text = "Enemies won !";
        }
        if(enemies.Count == 0){
            vt.GetComponent<Text>().text = "Player won !";
        }
        replayButton.SetActive(true);
    }

    public void restartCombat(){
        resetBoard();
        resetEnemies();
        recoverDices();
        replayButton.SetActive(false);
        GameObject.Find("VictoryText").GetComponent<Text>().text = "";
        Player p = player.GetComponent<Player>();
        p.setCurrentHP(p.getMaxHP());
        p.setArmor(0);
        Start();
    }

    // ========================= Turn Management =========================
    
    /// <summary>
    /// Starts the current character's turn
    /// </summary>
    void StartTurn(){
        bool play = activeCharacter.GetComponent<Character>().getMatchingBuff("Ice") == null;
        activeCharacter.GetComponent<Character>().applyBuffs();
        if(activeCharacter.GetComponent<Character>().getCurrentHP() <= 0){
            if(activeCharacter.GetComponent<Character>() is Enemy){
                killEnemy(activeCharacter);
            }
            EndTurn();
        }else{
            if(activeCharacter == player){
                foreach(GameObject enemy in enemies){
                    enemy.GetComponent<Enemy>().chooseAbilityAndTarget();
                }
                if(play){
                    drawDices();
                    rollDices();
                }else{
                    EndTurn();
                }
            }else{
                if(play){
                    activeCharacter.GetComponent<Enemy>().useAbility();
                }
                EndTurn();
            }
        }
    }

    /// <summary>
    /// Ends the current character's turn
    /// </summary>
    void EndTurn(){
        if(activeCharacter == player){
            //At the end of the player's turn, discard all remaining dices on the board
            resetBoard();
            //Trigger "end of turn" event, calling onTurnFinished
            onTurnFinished();
        }else{
            if(player.GetComponent<Player>().getCurrentHP() <= 0){
                EndCombat();
            }else{
                onTurnFinished();
            }
        }
        
    }

    void onTurnFinished(){
        //Select next character
        if(activeCharacterID<enemies.Count-1){
            activeCharacterID++;
            activeCharacter = enemies[activeCharacterID];
            StartTurn();
        }else{
            //Trigger "end of global turn" event, calling onGlobalTurnFinished
            onGlobalTurnFinished();
        }
    }

    void onGlobalTurnFinished(){
        if(enemies.Count == 0){
            EndCombat();
        }else{
            activeCharacterID = -1;
            activeCharacter = player.gameObject;
            StartTurn();
        }
    }

    // =================================== Board Management ===================================

    int searchEmptySlot(){
        int i = 0;
        while(boardDiceFaces[i] != null){
            i++;
        }
        return i;
    }

    bool isBoardEmpty(){
        int i = 0;
        while(i < boardDiceFaces.Length){
            if(boardDiceFaces[i] != null){
                return false;
            }
            i++;
        }
        return true;
    }

    void addDiceFaceToBoard(DiceFace df){
        int slot = searchEmptySlot();
        Vector3 pos = new Vector3(100*slot+diceFacePrefab.transform.position.x, diceFacePrefab.transform.position.y);
        GameObject go = Instantiate(diceFacePrefab, pos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
        //Apply the rolled DiceFace to the GameObject
        BoardDiceFace dfgo = go.GetComponent<BoardDiceFace>();
        dfgo.setDiceFace(df);
        dfgo.setBasePosition(dfgo.gameObject.transform.position);
        dfgo.setSlot(slot);
        //Sets the text of the GameObject
        string faceColorName = dfgo.getDiceFace().getFaceColor().ToString();
        go.GetComponentInChildren<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Effects/effect_"+faceColorName.ToLower()+".png");
        //Add reference to GameObject
        boardDiceFaces[slot] = go;
    }

    /// <summary>
    /// Picks X dices from the dice bag and add them to the board, X being the size of the board
    /// </summary>
    void drawDices(){
        if(diceBag.Count <= 0){
            recoverDices();
        }
        int i = 0;
        while(i < player.GetComponent<Player>().getMaxDicesOnBoard() && diceBag.Count > 0){
            int rand = UnityEngine.Random.Range(0,diceBag.Count);
            boardDices[i] = diceBag[rand];
            diceBag.Remove(diceBag[rand]);
            i++;
        }
        GameObject.Find("DiceBag").GetComponentInChildren<Text>().text = diceBag.Count+"/"+player.GetComponent<Player>().getDiceAmount();
    }

    /// <summary>
    /// Rolls the dices on the board to determine the faces that the player can use
    /// </summary>
    void rollDices(){
        for(int i = 0; i < boardDices.Length; i++){
            if(boardDices[i] != null){
                Dice d = boardDices[i];
                //Roll a random face from the dice
                DiceFace df = d.getFaces()[UnityEngine.Random.Range(0,d.getMaxFaces())];
                //Instantiate a new BoardDiceFace GameObject
                addDiceFaceToBoard(df);
            }
        }
    }

    /// <summary>
    /// Discard a dice from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the dice.</param>
    void discardDice(int boardSlotID){
        //Move dice from board to used
        Dice d = boardDices[boardSlotID];
        if(d != null){
            usedDices.Add(d);
            boardDices[boardSlotID] = null;
        }
    }

    /// <summary>
    /// Discard a face from the board
    /// </summary>
    /// <param name="boardSlotID">The slot id from which to discard the face.</param>
    void discardDiceFace(int boardSlotID){
        //Move dice face from board to used
        GameObject go = boardDiceFaces[boardSlotID];
        boardDiceFaces[boardSlotID] = null;
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
    /// Returns all used dices into the dice bag
    /// </summary>
    void recoverDices(){
        diceBag.AddRange(usedDices);
        usedDices.Clear();
    }

    void resetBoard(){
        if(!isBoardEmpty()){ 
            for(int i = 0; i < boardDices.Length; i++){
                discardDiceAndFace(i);
            }
        }
    }

    // ========================= Dice Using =========================

    /// <summary>
    /// Apply a dice face's effect on the target, then discard the dice
    /// </summary>
    public void useDice(int boardSlotID, GameObject target){
        boardDiceFaces[boardSlotID].GetComponent<BoardDiceFace>().getDiceFace().applyEffects(player.GetComponent<Player>() ,target.GetComponent<Enemy>());
        discardDiceAndFace(boardSlotID);
        if(target.GetComponent<Enemy>().getCurrentHP() <= 0){
            killEnemy(target);
        }else{
            target.GetComponent<Enemy>().refreshHUD();
        }
        player.GetComponent<Player>().refreshHUD();
        if(enemies.Count == 0){
            EndCombat();
        }else if(isBoardEmpty()){
            EndTurn();
        }
    }

    // ========================= Dice fusion =========================

    public bool combinableDiceFaces(GameObject df1, GameObject df2){
        DiceFaceColor dfc1 = df1.GetComponent<BoardDiceFace>().getDiceFace().getFaceColor();
        DiceFaceColor dfc2 = df1.GetComponent<BoardDiceFace>().getDiceFace().getFaceColor();
        List<DiceFaceColor> combinableColors = new List<DiceFaceColor>{ DiceFaceColor.WATER, DiceFaceColor.EARTH, DiceFaceColor.FIRE };
        return combinableColors.Contains(dfc1) && combinableColors.Contains(dfc2);
    }

    public void combineDiceFaces(GameObject df1, GameObject df2){
        if(combinableDiceFaces(df1,df2)){
            int dfc1 = (int) df1.GetComponent<BoardDiceFace>().getDiceFace().getFaceColor();
            int dfc2 = (int) df2.GetComponent<BoardDiceFace>().getDiceFace().getFaceColor();
            DiceFaceColor dfc = new DiceFaceColorCombine().matrix[dfc1,dfc2];
            discardDiceAndFace(Array.IndexOf(boardDiceFaces, df1));
            discardDiceAndFace(Array.IndexOf(boardDiceFaces, df2));
            addDiceFaceToBoard(new DiceFace(dfc));
        }else{
            //Prevent fusion
        }
    }

    public void AddDiceFaceToFusion(GameObject df){
        if(!diceFacesToFusion.Contains(df)){
            diceFacesToFusion.Add(df);
            if(diceFacesToFusion.Count == 2){
                combineDiceFaces(diceFacesToFusion[0],diceFacesToFusion[1]);
                diceFacesToFusion.Clear();
            }
        }
    }

    public void RemoveDiceFaceToFusion(GameObject df){
        diceFacesToFusion.Remove(df);
    }

    public List<GameObject> getDicesFacesToFusion(){
        return diceFacesToFusion;
    }

    // ========================= Enemy entity Management =========================

    /// <summary>
    /// Remove an enemy from the combat
    /// </summary>
    void killEnemy(GameObject enemy){
        enemies.Remove(enemy);
        Destroy(enemy);
    }

    void resetEnemies(){
        foreach(GameObject go in enemies){
            Destroy(go);
        }
        enemies.Clear();
    }
}
