using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private List<DiceFace> abilities = new List<DiceFace>();
    private CombatController combatController;
    private DiceFace chosenAbility;
    private Character chosenTarget;

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
        addAbility(new DiceFace(DiceFaceColor.NEUTRAL));
        addAbility(new DiceFace(DiceFaceColor.EARTH));
        setChosenAbility(abilities[0]);
    }

    /// <summary>
    /// Apply an ability effect on a target (WIP)
    /// </summary>
    public void chooseAbility(){
        int i = abilities.IndexOf(chosenAbility)+1;
        if(i >= abilities.Count)
            i = 0;
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
