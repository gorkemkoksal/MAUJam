using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private GridPosition gridPosition;
    private MoveAction moveAction;
    [SerializeField] private bool isEnemy;
    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
    }
    private void Start()
    {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }
    private void Update()
    {
        print(TurnSystem.Instance.IsPlayerTurn());
        var newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
        if (Input.GetMouseButtonDown(0) && TurnSystem.Instance.IsPlayerTurn()) //deneysel
        {
            var mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (moveAction.IsValidActionGridPosition(mouseGridPosition))
            {
                moveAction.Move(mouseGridPosition);
            }
        }
    }
    public MoveAction GetMoveAction() => moveAction;
    public GridPosition GetGridPosition() => gridPosition;
    public bool IsEnemy=> isEnemy;
}
