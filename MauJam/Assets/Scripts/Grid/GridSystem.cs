using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridSystem
{
    private int height, width;
    private float cellsize;
    public GridSystem(int height, int width, float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellsize = cellSize;
    }
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z);
    }
    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellsize), Mathf.RoundToInt(worldPosition.z / cellsize));
    }
}
