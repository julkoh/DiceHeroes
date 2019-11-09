using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private List<EffectInfo> abilities;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        setMaxHP(Random.Range(10,20));
        setCurrentHP(getMaxHP());
        setArmor(0);
    }
}
