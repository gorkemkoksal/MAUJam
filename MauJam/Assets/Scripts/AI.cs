using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AI : MonoBehaviour
{
    private float timer=2f;
    private void Start()
    {
        TurnSystem.Instance.OnTurnChange += AI_OnTurnChange;
    }
    private void Update()
    {
        if (TurnSystem.Instance.IsPlayerTurn()) { return; }

        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            TurnSystem.Instance.NextTurn();
        }
    }
    private void AI_OnTurnChange()
    {
        timer = 2f;
    }
}