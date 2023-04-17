using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapLevel
{
    private int levelNumber;
    private int xMin, xMax, yMin, yMax;
    private Vector3Int? stairsPosition;

    public TileMapLevel()
    {
        levelNumber = -1;
        xMin = 0; xMax = 0; yMin = 0; yMax = 0;
        stairsPosition = null;
    }

    public TileMapLevel(int lNum, int xMin, int xMax, int yMin, int yMax)
    {
        this.levelNumber = lNum;
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
        stairsPosition = null; 
    }

    public TileMapLevel(int lNum, Vector2Int position, int xLength, int yLength)
    {
        levelNumber = lNum;
        xMin = position.x;
        yMin = position.y;
        xMax = xMin + xLength;
        yMax = yMin + yLength;
    }

    public void setLevel(int lNum, Vector2Int position, int xLength, int yLength)
    {
        levelNumber = lNum;
        xMin = position.x;
        yMin = position.y;
        xMax = xMin + xLength;
        yMax = yMin = yLength;
    }

    public void setStairsPosition(Vector3Int stairsPosition)
    {
        this.stairsPosition = stairsPosition;
    }

    public int getXMin() { return xMin; }
    public int getXMax() { return xMax; }
    public int getYMin() { return yMin; }
    public int getYMax() { return yMax; }
    public int getLevelNumber() { return levelNumber; }
    public Vector3Int getStairsPosition()
    {
        if (stairsPosition != null)
        {
            return (Vector3Int)stairsPosition;
        }
        else return new Vector3Int(0, 0, 0);
    }
}
