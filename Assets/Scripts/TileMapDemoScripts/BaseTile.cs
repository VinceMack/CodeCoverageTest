using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// BaseTile stores required information for all tiles. All tile types should
// derive from BaseTile.
public class BaseTile : Tile
{
    public int x, y;
    private int tileInformation;

    // Pathfinding
    public int distance;
    public bool visited;
    protected bool isCollision;
    public BaseTile parent;

    public BaseTile() : base()
    {
        x = 0; y = 0;
        tileInformation = -1;

        distance = -1;
        visited = false;
        isCollision = false;
        parent = null;
    }

    public void printInformation()
    {
        Debug.Log("X: " + x + " Y: " + y + " Collision: " + isCollision);
    }

    public void setTileInformation(int x, int y, int information, bool collision)
    {
        this.x = x; this.y = y;
        tileInformation = information;
        isCollision = collision;
    }

    public int getTileInformation()
    {
        return tileInformation;
    }

    public bool Collision()
    {
        return isCollision;
    }
}

// Example of a tile derived from BaseTile
public class GrassTile : BaseTile
{
    public GrassTile() : base()
    {
    }

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
    private int rockResources;

    public RockTile() : base()
    {
        rockResources = 0;
    }

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

    public void setResources(int resources)
    {
        rockResources = resources;
    }

    public void printResources()
    {
        Debug.Log("Resources: " + rockResources);
    }
}

public class WaterTile : BaseTile
{
    public WaterTile() : base()
    {
        isCollision = true;
    }

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
