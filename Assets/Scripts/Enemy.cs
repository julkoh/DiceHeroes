﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    private List<EffectInfo> abilities = new List<EffectInfo>();
    private CombatController combatController;
    private EffectInfo chosenAbility;
    private Character chosenTarget;

    public List<EffectInfo> getAbilities(){
        return abilities;
    }

    public void addAbility(EffectInfo ei){
        abilities.Add(ei);
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
        setMaxHP(Random.Range(10,20));
        setCurrentHP(getMaxHP());
        setArmor(0);
        EffectInfo ability = new EffectInfo(new Damage(), Random.Range(1,10), "");
        addAbility(ability);
    }

    /// <summary>
    /// Apply an ability effect on a target (WIP)
    /// </summary>
    public void chooseAbilityAndTarget(){
        chosenAbility = abilities[Random.Range(0,abilities.Count-1)];
        chosenTarget = combatController.getPlayer().GetComponent<Player>();
        gameObject.transform.Find("Intent").Find("Value").gameObject.GetComponent<Text>().text = ""+chosenAbility.getValue();
    }

    /// <summary>
    /// Apply the chosen ability effect on the chosen target
    /// </summary>
    public void useAbility(){
        chosenAbility.applyEffect(this, chosenTarget);
        chosenTarget.refreshHUD();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("DiceFace") && other.gameObject.GetComponent<DiceFace>().getAction())
        {
            combatController.useDice(other.gameObject.GetComponent<DiceFace>().getSlot(), gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other){
        OnTriggerEnter2D(other);
    }
}
