using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DrawWalls : MonoBehaviour
{
    public Tilemap wallzTilemap;
    public Tile defaultWallTile;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void drawWallz(Cell[,] wallArray)
    {
        Utils.ClearTilemap(wallzTilemap);

        for (int x = 0; x < wallArray.GetLength(0); x++)
        {
            for (int y = 0; y < wallArray.GetLength(1); y++)
            {
                Cell daCell = wallArray[x, y];
                if (daCell.isWall)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    wallzTilemap.SetTile(position, defaultWallTile);
                }
            }
        }
    }

}
