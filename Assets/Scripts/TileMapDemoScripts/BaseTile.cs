using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// BaseTile stores required information for all tiles. All tile types should
// derive from BaseTile.
public class BaseTile : Tile
{
    public int x, y;
    protected TileType tileType;

    // Pathfinding
    public int distance;
    public bool visited;
    protected bool isCollision;
    public BaseTile parent;

    //A* Pathfinding Info
    public int fCost, gCost, hCost = 0;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    private void InitializePathfindingVariables()
    {
        distance = -1;
        visited = false;
        parent = null;
    }

    public void InitializeTileData(int x, int y, TileType tileType, bool collision)
    {
        this.x = x; this.y = y;
        this.tileType = tileType;

        isCollision = collision;

        InitializePathfindingVariables();
    }    

    public void debugPrintInformation()
    {
        Debug.Log("X: " + x + " Y: " + y + " Collision: " + isCollision + " TileType: " + tileType);
    }

    public void setTileType(TileType newTileType)
    {
        tileType = newTileType;
    }

    public TileType getTileType()
    {
        return tileType;
    }

    public void setCollision(bool collision)
    {
        isCollision = collision;
    }

    public bool Collision()
    {
        return isCollision;
    }
}

// Example of a tile derived from BaseTile
public class GrassTile : BaseTile
{
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);
        
        // Reads from Assets/Resources folder
        Sprite sprite = Resources.Load<Sprite>("grass"); // Resources/grass.png
        tileData.sprite = sprite; // Set the tile's sprite
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }
}

public class RockTile : BaseTile
{
    int resources;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        Sprite sprite = Resources.Load<Sprite>("rock");
        tileData.sprite = sprite;
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

    public void setResources(int newResources)
    {
        resources = newResources;
    }

    public int getResources()
    {
        return resources;
    }

}

public class WaterTile : BaseTile
{
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        // Reads from Assets/Resources folder
        Sprite sprite = Resources.Load<Sprite>("water"); // Resources/water.png
        tileData.sprite = sprite; // Set the tile's sprite
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        base.RefreshTile(position, tilemap);
    }

}
