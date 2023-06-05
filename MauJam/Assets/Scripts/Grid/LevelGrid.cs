using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public static LevelGrid Instance { get; private set; }
    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem<GridObject> gridSystem;
    private void Awake()
    {
        Instance = this;
        gridSystem = new GridSystem(10, 10, 2f, (GridSystem<GridObject> g, GridPosition gridPosition)=> new GridObject(g,gridPosition));
        gridSystem.CreateDebugObjects(gridDebugObjectPrefab);
    }
    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
    {
        var gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    } // buraya player ve enemy icin ortak bi interface koymak lazim player yerine
    public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
    {
        var gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnitList();
    }
    public void RemoveUnitatGridPosition(GridPosition gridPosition, Unit unit)
    {
        var gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }
    public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
    {
        RemoveUnitatGridPosition(fromGridPosition, unit);
        AddUnitAtGridPosition(toGridPosition, unit);
    }
    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);
    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);
    public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);
    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
    {
        var gridObject=gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }
    public int GetWidth() => gridSystem.GetWidth();
    public int GetHeight() => gridSystem.GetHeight();

}
