using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// PathfindingManager class to handle pathfinding on a grid of tiles
public class PathfindingManager : MonoBehaviour
{
    protected static int _width, _height;

    // Method to Get a path between two points on the grid
    public static List<Vector3> GetPath(Vector3Int current, Vector3Int tarGet)
    {
        BaseTile_VM currentNode = (BaseTile_VM)GridManager.tileMap.GetTile(current);
        currentNode.distance = 0;

        while (true)
        {
            checkAdjacentNodes(currentNode);
            BaseTile_VM nextNode = GetNextNode();

            if (nextNode == null)
            {
                List<Vector3> path = new List<Vector3>();

                // Get tarGet node
                nextNode = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(tarGet.x, tarGet.y, 0));

                // Get path
                while (nextNode.parent != null)
                {
                    path.Add(new Vector3(nextNode.GetXPosition(), nextNode.GetYPosition(), 0));
                    nextNode = nextNode.parent;
                }

                reSetGrid();
                return path;
            }
            currentNode = nextNode;
        }
    }

    // Method to reSet grid values for pathfinding
    protected static void reSetGrid()
    {
        // Iterate through each tile in the grid and reSet its properties
        for (int x = GridManager.MIN_HORIZONTAL; x < GridManager.MAX_HORIZONTAL; x++)
        {
            for (int y = GridManager.MIN_VERTICAL; y < GridManager.MAX_VERTICAL; y++)
            {
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));

                // Check if tile is not null and debug if it is
                if (tile == null)
                {
                    Debug.LogError("tile is null @ reSetGrid");
                    break;
                }

                tile.visited = false;
                tile.distance = -1;
                tile.parent = null;
                tile = null;
            }
        }
    }

    // Method to Get the next node in the pathfinding process
    protected static BaseTile_VM GetNextNode()
    {
        BaseTile_VM nextNode = null;
        // Iterate through each tile in the grid and find the next tile to process
        for (int x = GridManager.MIN_HORIZONTAL; x < GridManager.MAX_HORIZONTAL; x++)
        {
            for (int y = GridManager.MIN_VERTICAL; y < GridManager.MAX_VERTICAL; y++)
            {
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));

                // Check if tile is not null and debug if it is
                if (tile == null)
                {
                    Debug.LogError("tile is null @ GetNextNode");
                    return null;
                }

                // Check if the tile is traversable and has not been visited yet
                if (!tile.GetCollision() && !tile.visited && tile.distance > 0)
                {
                    // Set the next node if it has not been Set yet or if it has a shorter distance to the starting node
                    if (nextNode == null)
                        nextNode = tile;
                    else if (tile.distance < nextNode.distance)
                        nextNode = tile;
                }
                tile = null;
            }
        }
        return nextNode;
    }

    // Method to check and update adjacent nodes in the pathfinding process
    public static void checkAdjacentNodes(BaseTile_VM currentNode)
    {
        // Iterate through each adjacent tile and update its properties if necessary
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                // Skip the center node
                if (y == 0 && x == 0)
                {
                    continue;
                }

                // Calculate the position of the adjacent tile
                int newX = currentNode.GetXPosition() + x;
                int newY = currentNode.GetYPosition() + y;

                // Check if the adjacent tile is within the bounds of the grid
                if (newX >= GridManager.MIN_HORIZONTAL && newX < GridManager.MAX_HORIZONTAL && newY >= GridManager.MIN_VERTICAL && newY < GridManager.MAX_VERTICAL)
                {

                    BaseTile_VM adjacentTile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(newX, newY, 0));

                    // Check if tile is not null and debug if it is
                    if (adjacentTile == null)
                    {
                        Debug.LogError("adjacentTile is null @ checkAdjacentNodes");
                        break;
                    }

                    // Calculate the distance between the current node and the adjacent node
                    float xdistance = currentNode.GetXPosition() - newX;
                    float ydistance = currentNode.GetYPosition() - newY;
                    int distance = (int)(Mathf.Sqrt(Mathf.Pow(xdistance, 2) + Mathf.Pow(ydistance, 2)) * 10);

                    // Update the adjacent node's properties if necessary
                    if (currentNode.distance + distance < adjacentTile.distance || adjacentTile.distance == -1)
                    {
                        adjacentTile.distance = currentNode.distance + distance;
                        adjacentTile.parent = currentNode;
                    }

                    adjacentTile = null;
                }
            }
        }
        currentNode.visited = true;
    }

    // Start method to initialize grid dimensions
    void Start()
    {
        // Calculate the width and height of the grid
        _width = (GridManager.MAX_HORIZONTAL - GridManager.MIN_HORIZONTAL) / 2;
        _height = (GridManager.MAX_VERTICAL - GridManager.MIN_VERTICAL) / 2;
    }

}
