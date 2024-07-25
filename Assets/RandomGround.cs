using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomGround : MonoBehaviour
{
    public Tile[] groundtiles;
    public Tilemap groundTilemap;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRandomGround();
    }

    void GenerateRandomGround()
    {
        // Loop through each position in the grid
        for (int x = 0; x < Utils.gridWidth; x++)
        {
            for (int y = 0; y < Utils.gridHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Tile randomTile = groundtiles[Random.Range(0, groundtiles.Length)];
                groundTilemap.SetTile(tilePosition, randomTile);
            }
        }
    }
}
