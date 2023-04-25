using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PathfindingManager class to compute paths
public class PathfindingManager : MonoBehaviour
{

    public static List<Vector3Int> GetAdjacentTiles(Vector3Int tile)
    {
        List<Vector3Int> adjacentTiles = new List<Vector3Int>
        {
            new Vector3Int(tile.x + 1, tile.y, tile.z),
            new Vector3Int(tile.x - 1, tile.y, tile.z),
            new Vector3Int(tile.x, tile.y + 1, tile.z),
            new Vector3Int(tile.x, tile.y - 1, tile.z)
        };

        adjacentTiles.RemoveAll(t => GridManager.GetTile(t) == null || GridManager.GetTile(t).isCollision);

        return adjacentTiles;
    }

    public static List<Vector3> GetPath(Vector3Int start, Vector3Int target, int level, bool isDirectPathing)
    {
        GridManager.ResetGrid(level);
        PriorityQueue<BaseTile_VM> unvisited = new PriorityQueue<BaseTile_VM>();
        BaseTile_VM startTile = GridManager.GetTile(start);

        List<BaseTile_VM> targetTiles;
        if (!isDirectPathing)
        {
            List<Vector3Int> adjacentTilePositions = GetAdjacentTiles(target);
            targetTiles = adjacentTilePositions.ConvertAll(GridManager.GetTile);
        }
        else
        {
            targetTiles = new List<BaseTile_VM> { GridManager.GetTile(target) };
        }

        startTile.distance = 0;
        unvisited.Enqueue(startTile, 0);

        while (unvisited.Count > 0)
        {
            BaseTile_VM currentTile = unvisited.Dequeue();

            if (targetTiles.Contains(currentTile))
            {
                return ReconstructPath(currentTile);
            }

            currentTile.visited = true;

            foreach (BaseTile_VM neighbor in GetNeighbors(currentTile, level))
            {
                if (!neighbor.visited && !neighbor.isCollision)
                {
                    int cost = 0;
                    switch (neighbor.type)
                    {
                        case TileType.GENERIC:
                            cost = 1; // should not reach
                            break;
                        case TileType.GRASS:
                            cost = 1;
                            break;
                        case TileType.SAND:
                            cost = 4;
                            break;
                        case TileType.STONE:
                            cost = 2;
                            break;
                        case TileType.WATER:
                            cost = 0; // Water is impassable; should not reach
                            break;
                        case TileType.ROCK:
                            cost = 0; // Rock is impassable; should not reach
                            break;
                        default:
                            cost = 1; // should not reach
                            break;
                    }

                    int tentativeDistance = currentTile.distance + 10 + cost;

                    if (tentativeDistance < neighbor.distance || neighbor.distance == -1)
                    {
                        neighbor.distance = tentativeDistance;
                        neighbor.parent = currentTile;
                        unvisited.Enqueue(neighbor, tentativeDistance);
                    }
                }
            }
        }

        return new List<Vector3>(); // Return empty path if no path is found
    }


    private static List<BaseTile_VM> GetNeighbors(BaseTile_VM tile, int level)
    {
        List<BaseTile_VM> neighbors = new List<BaseTile_VM>();

        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if ((y == 0 && x == 0) || (y != 0 && x != 0))
                {
                    continue;
                }

                int newX = tile.GetXPosition() + x;
                int newY = tile.GetYPosition() + y;

                if (newX >= GridManager.mapLevels[level].getXMin() &&
                   newX < GridManager.mapLevels[level].getXMax() &&
                   newY >= GridManager.mapLevels[level].getYMin() &&
                   newY < GridManager.mapLevels[level].getYMax())
                {
                    neighbors.Add(GridManager.GetTile(new Vector3Int(newX, newY, 0)));
                }
            }
        }

        return neighbors;
    }

    private static List<Vector3> ReconstructPath(BaseTile_VM targetTile)
    {
        List<Vector3> path = new List<Vector3>();
        BaseTile_VM currentNode = targetTile;

        while (currentNode.parent != null)
        {
            path.Add(new Vector3(currentNode.GetXPosition(), currentNode.GetYPosition(), 0));
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }
}

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, int>> elements = new List<KeyValuePair<T, int>>();

    public int Count
    {
        get { return elements.Count; }
    }

    public void Enqueue(T item, int priority)
    {
        elements.Add(new KeyValuePair<T, int>(item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;

        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex);
        return bestItem;
    }

    public bool Contains(T item)
    {
        return elements.Exists(element => element.Key.Equals(item));
    }

    public void Clear()
    {
        elements.Clear();
    }
}