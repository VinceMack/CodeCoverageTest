using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StairsTile : BaseTile
{
    StairsTile upperLevelStairs;
    StairsTile lowerLevelStairs;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/stairs");
    }

    public override void SetTileData(TileType tileType, bool collision, Item resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile parent, int level)
    {
        this.type = tileType;
        this.isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
        this.distance = distance;
        this.visited = visited;
        this.parent = parent;
        this.level = level;

        upperLevelStairs = null;
        lowerLevelStairs = null;
    }

    public StairsTile getUpperLevelStairs()
    {
        return upperLevelStairs;
    }

    public void setUpperLevelStairs(StairsTile upperLevelStairs)
    {
        this.upperLevelStairs = upperLevelStairs;
    }

    public StairsTile getLowerLevelStairs()
    {
        return lowerLevelStairs;
    }

    public void setLowerLevelStairs(StairsTile lowerLevelStairs)
    {
        this.lowerLevelStairs = lowerLevelStairs;
    }
}




