﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ActiveOnlyDuringState : MonoBehaviour
{
    public GameStates myActiveState;
    private void Awake()
    {
        GameManager.Instance.CheckMyGameStates.AddListener(EnableOrDisableMyself);
    }
     void EnableOrDisableMyself(GameStates currentGameState)
    {
        if (myActiveState == currentGameState)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
 
}
