using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private List<List<Effect>> abilities = new List<List<Effect>>();
    private CombatController combatController;
    private List<Effect> chosenAbility;
    private Character chosenTarget;

    public static GameObject Create(GameObject enemyPrefab, Vector3 pos, CombatController cb){
        GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity);
            go.transform.SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>().transform, false);
            go.GetComponent<Enemy>().setCombatController(cb);
            go.GetComponent<Enemy>().refreshHUD();
            return go;
    }

    public List<List<Effect>> getAbilities(){
        return abilities;
    }

    public void addAbility(List<Effect> e){
        abilities.Add(e);
    }

    public CombatController getCombatController(){
        return combatController;
    }

    public void setCombatController(CombatController cb){
        combatController = cb;
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        setMaxHP(Random.Range(10,21));
        setCurrentHP(getMaxHP());
        setArmor(0);
        addAbility(new List<Effect>{
            new Damage(Random.Range(3,5), "")
        });
        addAbility(new List<Effect>{
            new Damage(Random.Range(1,3), ""),
            new Shield(Random.Range(1,3), "")
        });
    }

    /// <summary>
    /// Apply an ability effect on a target (WIP)
    /// </summary>
    public void chooseAbilityAndTarget(){
        chosenAbility = abilities[Random.Range(0,abilities.Count)];
        chosenTarget = combatController.getPlayer().GetComponent<Player>();
        gameObject.transform.Find("Intent").Find("Value").gameObject.GetComponent<Text>().text = ""+chosenAbility[0].getValue();
    }

    /// <summary>
    /// Apply the chosen ability effect on the chosen target
    /// </summary>
    public void useAbility(){
        foreach(Effect e in chosenAbility){
            e.apply(this, chosenTarget);
        }
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
