using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StairsTile_VM : BaseTile_VM
{
    StairsTile_VM upperLevelStairs;
    StairsTile_VM lowerLevelStairs;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/stairs");
    }

    public override void SetTileData(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile_VM parent, int level)
    {
        base.SetTileData(tileType, collision, resource, resourceCount, position, distance, visited, parent, GridManager.mapLevels.Count - 1);

        upperLevelStairs = null;
        lowerLevelStairs = null;
    }

    public StairsTile_VM getUpperLevelStairs()
    {
        return upperLevelStairs;
    }

    public void setUpperLevelStairs(StairsTile_VM upperLevelStairs)
    {
        this.upperLevelStairs = upperLevelStairs;
    }

    public StairsTile_VM getLowerLevelStairs()
    {
        return lowerLevelStairs;
    }

    public void setLowerLevelStairs(StairsTile_VM lowerLevelStairs)
    {
        this.lowerLevelStairs = lowerLevelStairs;
    }
}
