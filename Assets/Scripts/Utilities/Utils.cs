using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Utils
{
    public const int gridWidth = 20;
    public const int gridHeight = 20;

    public static Vector2 tileSize = new Vector2(0.32f, 0.32f);
    public static Vector2 gridOrigin = Vector2.zero;

    public static TextMesh Label(string text, int x, int y)
    {
        // Create a new GameObject
        GameObject textGameObject = new GameObject("TextMeshObject");

        // Add a TextMesh component to the GameObject
        TextMesh textMesh = textGameObject.AddComponent<TextMesh>();

        // Set the text
        textMesh.text = text;

        // Customize the TextMesh properties
        textMesh.fontSize = 8;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.color = Color.white;

        // Position the GameObject in the world
        textGameObject.transform.position = new Vector3(x, y, 0);
        TextMesh mytextMesh = textGameObject.GetComponent<TextMesh>();
        return mytextMesh;
    }

    public static void ClearTilemap(Tilemap tilemap)
    {
        // Get the bounds of the Tilemap
        BoundsInt bounds = tilemap.cellBounds;

        // Iterate through all positions within the bounds
        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            // Set each tile to null to clear it
            tilemap.SetTile(position, null);
        }

        // Optionally, refresh the Tilemap to update the changes
        tilemap.RefreshAllTiles();
    }

    public static int GetDistance(Vector2 pos1, Vector2 pos2)
    {
        Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

        int lowest = Mathf.Min(dist.x, dist.y);
        int highest = Mathf.Max(dist.x, dist.y);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 14 + horizontalMovesRequired * 10;
    }

    public static Vector3 TileToWorld(Vector2 tilePosition)
    {
        // Calculate the world position
        float worldX = gridOrigin.x + tilePosition.x * tileSize.x + tileSize.x / 2;
        float worldY = gridOrigin.y + tilePosition.y * tileSize.y + tileSize.y / 2;

        // Return the world position
        return new Vector3(worldX, worldY, 0);
    }

    public static Vector2 WorldToTilePosition(Vector3 worldPosition)
    {
        // Calculate the tile position based on the world position and tile size
        int tileX = Mathf.FloorToInt(worldPosition.x / tileSize.x);
        int tileY = Mathf.FloorToInt(worldPosition.y / tileSize.y);

        return new Vector2(tileX, tileY);
    }

}
