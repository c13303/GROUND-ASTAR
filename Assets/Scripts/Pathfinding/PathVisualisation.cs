using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathVisualisation : MonoBehaviour
{
    public Tilemap PathVizuMap;
    public Tile foundTile;

    public void Start()
    {

    }

    public void DrawVisualisation(List<Vector2> foundPath)
    {

        //  Debug.Log("START Drawing visual");
        Utils.ClearTilemap(PathVizuMap);

        foreach (Vector2 point in foundPath)
        {
            // Debug.Log("Drawing visual");
            Vector3Int position = new Vector3Int((int)point.x, (int)point.y, 0);
            PathVizuMap.SetTile(position, foundTile);
        }
        PathVizuMap.RefreshAllTiles();
    }

}