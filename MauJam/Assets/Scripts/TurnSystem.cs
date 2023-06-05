using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public TurnState currentState;
    public enum TurnState
    {
        PlayerTurn,
        Transition,
        EnemyTurn
    }
    public static TurnSystem Instance { get; private set; }
    private int turnNumber=1;
    private bool isPlayerTurn = true;
    enum
    private void Awake()
    {
        Instance = this;
        currentState = TurnState.PlayerTurn;
    }
    public void NextTurn()
    {
        if (!isPlayerTurn)
        {
            turnNumber++;
        }
        isPlayerTurn = !isPlayerTurn;
    }
    public bool IsPlayerTurn()=>isPlayerTurn;
    public int GetTurnNumber() => turnNumber;
}
