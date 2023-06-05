using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance { get; private set; }
    private int turnNumber=1;
    public Action OnTurnChange;
    private bool isPlayerTurn = true;
    private void Awake()
    {
        Instance = this;
    }
    public void NextTurn()
    {
        if (!isPlayerTurn)
        {
            turnNumber++;
        }
        isPlayerTurn = !isPlayerTurn;
        OnTurnChange?.Invoke();
    }
    public bool IsPlayerTurn()=>isPlayerTurn;
    public int GetTurnNumber() => turnNumber;
}
