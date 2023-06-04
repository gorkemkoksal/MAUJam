using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem
{
    private int height, width;
    private float cellsize;
    private GridObject[,] gridObjectsArray;
    public GridSystem(int height, int width, float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellsize = cellSize;

        gridObjectsArray = new GridObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var gridPosition = new GridPosition(x, z);
                new GridObject(this, gridPosition);
                gridObjectsArray[x, z] = new GridObject(this, gridPosition);
            }
        }
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z);
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
                GameObject.Instantiate(debugPrefab,GetWorldPosition(x,z),Quaternion.identity);
            }
        }
    }
}
