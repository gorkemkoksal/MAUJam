using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem<TGridObject>
{
    private int height, width;
    private float cellsize;
    private TGridObject[,] gridObjectsArray;
    public GridSystem(int height, int width, float cellSize, Func<GridSystem<TGridObject>, GridPosition, TGridObject> createGridObject)
    {
        this.height = height;
        this.width = width;
        this.cellsize = cellSize;

        gridObjectsArray = new TGridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var gridPosition = new GridPosition(x, z);
                gridObjectsArray[x, z] = createGridObject(this,gridPosition);
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.x, 0, gridPosition.z);
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellsize), Mathf.RoundToInt(worldPosition.z / cellsize));
    }
    public void CreateDebugObjects(Transform debugPrefab)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var gridPosition = new GridPosition(x, z);

                Transform debugTransform = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                var gridDebugObject = debugTransform.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition) as GridObject);
            }
        }
    }
    public TGridObject GetGridObject(GridPosition gridPosition) => gridObjectsArray[gridPosition.x, gridPosition.z];
    public bool IsValidGridPosition(GridPosition gridPosition) => gridPosition.x >= 0 && gridPosition.z >= 0 && gridPosition.x < width && gridPosition.z < height;

    public int GetWidth() => width;
    public int GetHeight() => height;
}
