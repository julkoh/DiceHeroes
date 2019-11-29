﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("CombatScene");
    }

    public void Exit(){
        Application.Quit(0);
    }
}
