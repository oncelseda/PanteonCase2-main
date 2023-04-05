using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private const int move_straight_cost = 10;
    private const int move_diagonal_cost = 14;
    [SerializeField] private GameBoard gameBoard;
    private List<Tile> openList;
    private List<Tile> closeList;
    public static Pathfinding instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        gameBoard.GetXY(startWorldPosition, out int startX, out int startY);
        gameBoard.GetXY(endWorldPosition, out int endX, out int endY);

        List<Tile> path = FindPath(startX, startY, endX, endY);
        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach(Tile tile in path){
                vectorPath.Add(tile.transform.position);
            }
            return vectorPath;
        }
    }




    public List<Tile> FindPath(int startX, int startY, int endX, int endY)
    {
        Tile startTile = gameBoard.grid[startX, startY];
        Tile endTile = gameBoard.grid[endX, endY];

        openList = new List<Tile> { startTile };
        closeList = new List<Tile>();

        for (int x = 0; x < gameBoard.width; x++)
        {
            for (int y = 0; y < gameBoard.height; y++)
            {
                Tile tile = gameBoard.grid[x, y];
                tile.gCost = int.MaxValue;
                tile.CalculateFCost();
                tile.cameFromTile = null;
            }
        }
        startTile.gCost = 0;
        startTile.hCost = CalculateDistanceCost(startTile, endTile);
        startTile.CalculateFCost();

        while (openList.Count > 0)
        {
            Tile currentTile = GetLowestFCostTile(openList);
            if (currentTile == endTile)
            {
                return CalculatePath(endTile);
            }

            openList.Remove(currentTile);
            closeList.Add(currentTile);

            foreach (Tile neigbourTile in GetNeighbourList(currentTile))
            {
                if (closeList.Contains(neigbourTile)) continue;
                if (!neigbourTile.isWalkable)
                {
                    closeList.Add(neigbourTile);
                    continue;
                }

                int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neigbourTile);
                if (tentativeGCost < neigbourTile.gCost)
                {
                    neigbourTile.cameFromTile = currentTile;
                    neigbourTile.gCost = tentativeGCost;
                    neigbourTile.hCost = CalculateDistanceCost(neigbourTile, endTile);
                    neigbourTile.CalculateFCost();

                    if (!openList.Contains(neigbourTile))
                    {
                        openList.Add(neigbourTile);
                    }
                }
            }
        }

        return null;
    }

    private List<Tile> GetNeighbourList(Tile currentTile)
    {

        List<Tile> neighbourList = new List<Tile>();

        if (currentTile.x - 1 >= 0)
        {
            neighbourList.Add(GetTile(currentTile.x - 1, currentTile.y));
            if (currentTile.y - 1 >= 0) { neighbourList.Add(GetTile(currentTile.x - 1, currentTile.y - 1)); }
            if (currentTile.y + 1 < gameBoard.height) { neighbourList.Add(GetTile(currentTile.x - 1, currentTile.y + 1)); }
        }
        if (currentTile.x + 1 < gameBoard.width)
        {
            neighbourList.Add(GetTile(currentTile.x + 1, currentTile.y));
            if (currentTile.y - 1 >= 0) { neighbourList.Add(GetTile(currentTile.x + 1, currentTile.y - 1)); }
            if (currentTile.y + 1 < gameBoard.height) { neighbourList.Add(GetTile(currentTile.x + 1, currentTile.y + 1)); }

        }
        if (currentTile.y - 1 >= 0) { neighbourList.Add(GetTile(currentTile.x, currentTile.y - 1)); }
        if (currentTile.y + 1 < gameBoard.height) { neighbourList.Add(GetTile(currentTile.x, currentTile.y + 1)); }

        return neighbourList;

    }

    private Tile GetTile(int x, int y)
    {
        return gameBoard.grid[x, y];
    }


    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentTile = endTile;
        while (currentTile.cameFromTile != null)
        {
            path.Add(currentTile.cameFromTile);
            currentTile = currentTile.cameFromTile;
        }
        path.Reverse();
        return path;
    }


    private Tile GetLowestFCostTile(List<Tile> pathTileList)
    {
        Tile lowestFCostTile = pathTileList[0];
        for (int i = 1; i < pathTileList.Count; i++)
        {
            if (pathTileList[i].fCost < lowestFCostTile.fCost)
            {
                lowestFCostTile = pathTileList[i];
            }
        }
        return lowestFCostTile;
    }


    private int CalculateDistanceCost(Tile a, Tile b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);

        return move_diagonal_cost * Mathf.Min(xDistance, yDistance) + move_straight_cost * remaining;
    }

}

