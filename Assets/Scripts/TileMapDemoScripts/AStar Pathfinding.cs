using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarPathFinding
{
    private Tilemap tileMap;
    private int _width, _height;

    List<BaseTile> openList = new List<BaseTile>();
    HashSet<BaseTile> closedSet = new HashSet<BaseTile>();
    

    public AStarPathFinding(Tilemap tileMap)
    {
        this.tileMap = tileMap;
        _width = tileMap.size.x / 2;
        _height = tileMap.size.y / 2;
    }

    public List<Vector3> getPath(Vector3Int current, Vector3Int target)
    {
        BaseTile currentNode = (BaseTile)tileMap.GetTile(current);
        currentNode.distance = 0;

        openList.Add(currentNode);
        List<Vector3> path = new List<Vector3>();

        while (currentNode != (BaseTile)tileMap.GetTile(new Vector3Int(target.x, target.y, 0)))
        {   
            checkAdjacentNodes(currentNode);
            BaseTile nextNode = getNextNode();
            currentNode.gCost = 0;
            currentNode.hCost = Mathf.Abs(currentNode.x - nextNode.x) + Mathf.Abs(currentNode.y - nextNode.y);
            currentNode.fCost = currentNode.gCost + currentNode.hCost;
            if (nextNode != null)
            {
                
                // Get target node
                nextNode = (BaseTile)tileMap.GetTile(new Vector3Int(target.x, target.y, 0));
                
                // Get path
                while (nextNode.parent != null)
                {
                    Debug.Log(nextNode);
                    path.Add(new Vector3(nextNode.x, nextNode.y, 0));
                    nextNode = nextNode.parent;
                }

                resetGrid();
                return path;
            }
            currentNode = nextNode;
        }

        return path;
    }
    

    private void resetGrid()
    {
        for(int x=-1*_width; x<_width; x++) {
            for(int y=-1*_height; y<_height; y++) {
                BaseTile tile = (BaseTile)tileMap.GetTile(new Vector3Int(x, y, 0));
                tile.visited = false;
                tile.distance = -1;
                tile.parent = null;
                tile = null;
	        }
	    }
    }

    private BaseTile getNextNode()
    {
        /*
        BaseTile nextNode = null;
        for (int x = -1 * _width; x < _width; x++)
        {
            for (int y = -1 * _height; y < _height; y++)
            {
                BaseTile tile = (BaseTile)tileMap.GetTile(new Vector3Int(x, y, 0));

                if (!tile.Collision() && !tile.visited && tile.distance > 0)
                {
                    if (nextNode == null) nextNode = tile;
                    else if (tile.distance < nextNode.distance) nextNode = tile;
                }
                tile = null;
            }
        }
        return nextNode;
        */

        BaseTile nextNode = null;
        int lowestTotalCost = int.MaxValue;

        foreach (BaseTile tile in openList)
        {
            int totalCost = tile.distance + tile.fCost;

            if (totalCost < lowestTotalCost)
            {
                lowestTotalCost = totalCost;
                nextNode = tile;
            }
        }

        openList.Remove(nextNode);
        closedSet.Add(nextNode);

        return nextNode;
    }

    public void checkAdjacentNodes(BaseTile currentNode)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (y == 0 && x == 0) continue; // Skip center node
                if (currentNode.x + x >= -1 * _width && currentNode.x + x < _width &&
                   currentNode.y + y >= -1 * _height && currentNode.y + y < _height)
                {
                    BaseTile adjacentTile = (BaseTile)tileMap.GetTile(new Vector3Int(currentNode.x + x, currentNode.y + y, 0));

                    float dx = Mathf.Abs(currentNode.x + x - adjacentTile.x);
                    float dy = Mathf.Abs(currentNode.y + y - adjacentTile.y);
                    int distance = (int)(Mathf.Max(dx, dy) * 10);

                    int newCost = currentNode.gCost + distance;
                    if (newCost < adjacentTile.gCost || !openList.Contains(adjacentTile))
                    {
                        adjacentTile.gCost = newCost;
                        adjacentTile.hCost = distance;
                        adjacentTile.parent = currentNode;

                        if (!openList.Contains(adjacentTile))
                        {
                            openList.Add(adjacentTile);
                        }
                    }
                    adjacentTile = null;
                }
            }
        }
        currentNode.visited = true;
    }

    /*
     * using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinding
{
    private Tilemap tileMap;
    private int _width, _height;

    List<BaseTile> openList = new List<BaseTile>();
    HashSet<BaseTile> closedSet = new HashSet<BaseTile>();

    public PathFinding(Tilemap tileMap)
    {
        this.tileMap = tileMap;
        _width = tileMap.size.x / 2;
        _height = tileMap.size.y / 2;
    }

    public List<Vector3> getPath(Vector3Int current, Vector3Int target, int currentCost)
    {
        BaseTile currentNode = (BaseTile)tileMap.GetTile(current);
        currentNode.distance = 0;
        currentNode.heuristicCost = CalculateHeuristic(current, target);

        while (true)
        {
            checkAdjacentNodes(currentNode, target);
            BaseTile nextNode = getNextNode();

            if (nextNode == null)
            {
                List<Vector3> path = new List<Vector3>();

                // Get target node
                nextNode = (BaseTile)tileMap.GetTile(new Vector3Int(target.x, target.y, 0));

                // Get path
                while (nextNode.parent != null)
                {
                    path.Add(new Vector3(nextNode.x, nextNode.y, 0));
                    nextNode = nextNode.parent;
                }

                resetGrid();
                return path;
            }
            currentNode = nextNode;
        }
    }

    private void resetGrid()
    {
        for(int x=-1*_width; x<_width; x++) {
            for(int y=-1*_height; y<_height; y++) {
                BaseTile tile = (BaseTile)tileMap.GetTile(new Vector3Int(x, y, 0));
                tile.visited = false;
                tile.distance = -1;
                tile.heuristicCost = -1;
                tile.parent = null;
                tile = null;
            }
        }
    }

    private BaseTile getNextNode()
    {
        BaseTile nextNode = null;
        int lowestTotalCost = int.MaxValue;

        foreach (BaseTile tile in openList)
        {
            int totalCost = tile.distance + tile.heuristicCost;

            if (totalCost < lowestTotalCost)
            {
                lowestTotalCost = totalCost;
                nextNode = tile;
            }
        }

        openList.Remove(nextNode);
        closedSet.Add(nextNode);

        return nextNode;
    }

    private void checkAdjacentNodes(BaseTile currentNode, Vector3Int target)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (y == 0 && x == 0) continue; // Skip

     */


}
