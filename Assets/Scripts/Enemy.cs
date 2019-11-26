using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType {
    BRUTE, RACKETEER, JUNKIE, DEALER, BRIGAND, BANDIT
}

public class Enemy : Character
{
    private List<DiceFace> abilities = new List<DiceFace>();
    private CombatController combatController;
    private DiceFace chosenAbility;
    private Character chosenTarget;
    private EnemyType type;

    

    public static GameObject Create(GameObject enemyPrefab, Vector3 pos, CombatController cb){
        GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity);
        go.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
        go.GetComponent<Enemy>().setCombatController(cb);
        go.GetComponent<Enemy>().refreshHUD();
        return go;
    }

    public List<DiceFace> getAbilities(){
        return abilities;
    }

    public void addAbility(DiceFace e){
        abilities.Add(e);
    }

    public CombatController getCombatController(){
        return combatController;
    }

    public void setCombatController(CombatController cb){
        combatController = cb;
    }

    public DiceFace getChosenAbility(){
        return chosenAbility;
    }

    public void setChosenAbility(DiceFace e){
        chosenAbility = e;
    }

    public Character getChosenTarget(){
        return chosenTarget;
    }

    public void setChosenTarget(Character c){
        chosenTarget = c;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        setMaxHP(Random.Range(10,21));
        setCurrentHP(getMaxHP());
        setArmor(0);
        setType(GameController.getEnemyTypes()[Random.Range(0, GameController.getEnemyTypes().Count)]);
    }

    /// <summary>
    /// Apply an ability effect on a target (WIP)
    /// </summary>
    public void chooseAbility(){
        int i = 0;
        if(chosenAbility != null){
            i = abilities.IndexOf(chosenAbility)+1;
            if(i >= abilities.Count)
                i = 0;
        }
        setChosenAbility(abilities[i]);
        gameObject.transform.Find("Intent").Find("Value").gameObject.GetComponent<Text>().text = ""+chosenAbility.getEffect()[0].getValue();
        gameObject.transform.Find("Intent").Find("Icon").gameObject.GetComponent<Image>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Images/Effects/effect_"+chosenAbility.getFaceColor().ToString().ToLower()+".png");
    }

    /// <summary>
    /// Apply the chosen ability effect on the chosen target
    /// </summary>
    public void useAbility(){
        chosenAbility.applyEffects(this, chosenTarget);
        refreshHUD();
        chosenTarget.refreshHUD();
    }

    public void setType(EnemyType t){
        type = t;
        switch(type){
            case EnemyType.BANDIT :
                addAbility(new DiceFace(DiceFaceColor.EARTH,1));
                addAbility(new DiceFace(DiceFaceColor.EARTH,2));
                addAbility(new DiceFace(DiceFaceColor.ROCK,1));
                break;
            case EnemyType.BRIGAND :
                addAbility(new DiceFace(DiceFaceColor.WATER,1));
                addAbility(new DiceFace(DiceFaceColor.WATER,2));
                addAbility(new DiceFace(DiceFaceColor.ICE,1));
                break;
            case EnemyType.BRUTE :
                addAbility(new DiceFace(DiceFaceColor.FIRE,1));
                addAbility(new DiceFace(DiceFaceColor.FIRE,2));
                addAbility(new DiceFace(DiceFaceColor.LAVA,1));
                break;
            case EnemyType.DEALER :
                addAbility(new DiceFace(DiceFaceColor.FIRE,1));
                addAbility(new DiceFace(DiceFaceColor.WATER,1));
                addAbility(new DiceFace(DiceFaceColor.POISON,2));
                break;
            case EnemyType.JUNKIE :
                addAbility(new DiceFace(DiceFaceColor.EARTH,1));
                addAbility(new DiceFace(DiceFaceColor.WATER,1));
                addAbility(new DiceFace(DiceFaceColor.RADIATION,2));
                break;
            case EnemyType.RACKETEER :
                addAbility(new DiceFace(DiceFaceColor.FIRE,1));
                addAbility(new DiceFace(DiceFaceColor.EARTH,1));
                addAbility(new DiceFace(DiceFaceColor.PHYSICAL,2));
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("DiceFace") && other.gameObject.GetComponent<BoardDiceFace>().getAction())
        {
            combatController.useDice(other.gameObject.GetComponent<BoardDiceFace>().getSlot(), gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other){
        OnTriggerEnter2D(other);
    }
}
