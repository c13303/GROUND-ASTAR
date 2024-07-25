using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2 position;
    public int fCost;
    public int gCost;
    public int hCost;
    public Vector2 connection;
    public bool isWall;

    public Cell(Vector2 pos)
    {
        position = pos;
    }
}