using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder : MonoBehaviour
{
    private int gridWidth = Utils.gridWidth;
    private int gridHeight = Utils.gridHeight;
    private int cellWidth = 1;
    private int cellHeight = 1;





    private Cell[,] wallArray;
    private Dictionary<Vector2, Cell> cells;

    public List<Vector2> cellsToSearch;
    public List<Vector2> searchedCells;
    public List<Vector2> finalPath;

    public PathVisualisation pathVisualizer;
    public DrawWalls drawWalls;
    private ClickHandler clickHandler;

    public GameObject gameHandlerObject;

    private GameHandler gameHandler;

    void Start()
    {
        clickHandler = GetComponent<ClickHandler>();

        wallArray = new Cell[gridWidth, gridHeight];
        cells = new Dictionary<Vector2, Cell>(); // Initialize the dictionary
        createWallz();
        drawWalls.drawWallz(wallArray);

        gameHandler = gameHandlerObject.GetComponent<GameHandler>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Vector2 tilePosition = clickHandler.GetTileAtMouse();
            //  Debug.Log("Tile Position: " + tilePosition);


            if (gameHandler.selectedMan)
            {
                Vector2 currentPosition = Utils.WorldToTilePosition(gameHandler.selectedMan.transform.position);


                // Debug.Log(currentPosition.ToString() + " to " + tilePosition.ToString());
                List<Vector2> foundpath = FindPath(currentPosition, tilePosition);

                if (foundpath.Count > 0)
                {
                    gameHandler.selectedMan.myPath = foundpath;
                    gameHandler.selectedMan.Walk();
                }

            }
            else
            {
                Debug.Log("NO MAN SELECTED");
            }

        }
    }

    private void createWallz()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 pos = new Vector2(x, y);
                Cell daCell = new Cell(pos);
                int de = Random.Range(0, 5);
                if (de == 0)
                {
                    daCell.isWall = true;
                }
                if (x == 1 && y == 1)
                {
                    daCell.isWall = false;
                }

                if (x == 19 && y == 19)
                {
                    daCell.isWall = false;
                }

                wallArray[x, y] = daCell;
                cells[pos] = daCell;
                //  Debug.Log("Added cell at position: " + pos);
            }
        }
    }







    private List<Vector2> FindPath(Vector2 startPos, Vector2 endPos)
    {


        cellsToSearch = new List<Vector2> { startPos };
        searchedCells = new List<Vector2>();
        finalPath = new List<Vector2>();

        if (!cells.ContainsKey(startPos) || !cells.ContainsKey(endPos))
        {
            Debug.Log("Start or end position is out of the grid bounds.");
            return finalPath;
        }


        cells[startPos].gCost = 0;
        cells[startPos].hCost = Utils.GetDistance(startPos, endPos);
        cells[startPos].fCost = Utils.GetDistance(startPos, endPos);

        while (cellsToSearch.Count > 0)
        {
            Vector2 cellToSearch = cellsToSearch[0];

            foreach (Vector2 pos in cellsToSearch)
            {
                Cell c = cells[pos];
                if (c.fCost < cells[cellToSearch].fCost ||
                    c.fCost == cells[cellToSearch].fCost && c.hCost == cells[cellToSearch].hCost)
                {
                    cellToSearch = pos;
                }
            }

            cellsToSearch.Remove(cellToSearch);
            searchedCells.Add(cellToSearch);

            if (cellToSearch == endPos)
            {
                Cell pathCell = cells[endPos];

                while (pathCell.position != startPos)
                {
                    finalPath.Add(pathCell.position);
                    pathCell = cells[pathCell.connection];
                }

                finalPath.Add(startPos);
                finalPath.Reverse(); // Reverse the path to start from the beginning

                //  Debug.Log("Final Path: " + string.Join(" -> ", finalPath));
                // pathVisualizer.DrawVisualisation(finalPath);



                return finalPath;
            }

            SearchCellNeighbors(cellToSearch, endPos);
        }

        if (finalPath.Count == 0)
        {
            Debug.Log("Path not found");
            return finalPath;
        }


        return finalPath;




    }

    private void SearchCellNeighbors(Vector2 cellPos, Vector2 endPos)
    {
        for (float x = cellPos.x - cellWidth; x <= cellWidth + cellPos.x; x += cellWidth)
        {
            for (float y = cellPos.y - cellHeight; y <= cellHeight + cellPos.y; y += cellHeight)
            {
                Vector2 neighborPos = new Vector2(x, y);

                if (IsDiagonalMoveBlocked(cellPos, neighborPos))
                {
                    continue;
                }

                if (cells.TryGetValue(neighborPos, out Cell c) && !searchedCells.Contains(neighborPos) && !cells[neighborPos].isWall)
                {
                    int GcostToNeighbour = cells[cellPos].gCost + Utils.GetDistance(cellPos, neighborPos);

                    if (GcostToNeighbour < cells[neighborPos].gCost || !cellsToSearch.Contains(neighborPos))
                    {
                        Cell neighbourNode = cells[neighborPos];

                        neighbourNode.connection = cellPos;
                        neighbourNode.gCost = GcostToNeighbour;
                        neighbourNode.hCost = Utils.GetDistance(neighborPos, endPos);
                        neighbourNode.fCost = neighbourNode.gCost + neighbourNode.hCost;

                        if (!cellsToSearch.Contains(neighborPos))
                        {
                            cellsToSearch.Add(neighborPos);
                        }
                    }
                }
            }
        }
    }

    private bool IsDiagonalMoveBlocked(Vector2 currentPos, Vector2 neighborPos)
    {
        // Check if the move is diagonal
        if (Mathf.Abs(currentPos.x - neighborPos.x) == 1 && Mathf.Abs(currentPos.y - neighborPos.y) == 1)
        {
            // Check if there are walls blocking the diagonal movement
            Vector2 horizontalCheck = new Vector2(neighborPos.x, currentPos.y);
            Vector2 verticalCheck = new Vector2(currentPos.x, neighborPos.y);

            if (cells.ContainsKey(horizontalCheck) && cells[horizontalCheck].isWall &&
                cells.ContainsKey(verticalCheck) && cells[verticalCheck].isWall)
            {
                return true; // Diagonal move is blocked
            }
        }

        return false; // Diagonal move is not blocked
    }


}
