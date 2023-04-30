using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for map level data
public class Level
{
    private int levelNumber;
    private int xMin, xMax, yMin, yMax;

    private List<StairsTile> ascendingStairs;
    private List<StairsTile> descendingStairs;

    public Level()
    {
        levelNumber = -1;
        xMin = 0; xMax = 0; yMin = 0; yMax = 0;

        ascendingStairs = new List<StairsTile>();
        descendingStairs = new List<StairsTile>();
    }

    public Level(int lNum, int xMin, int xMax, int yMin, int yMax)
    {
        this.levelNumber = lNum;
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;


        ascendingStairs = new List<StairsTile>();
        descendingStairs = new List<StairsTile>();
    }

    public void setLevel(int lNum, int xMin, int xMax, int yMin, int yMax)
    {
        this.levelNumber = lNum;
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;
    }

    public void AddAscendingStairs(StairsTile stairs)
    {
        ascendingStairs.Add(stairs);
    }

    public void AddDescendingStairs(StairsTile stairs)
    {
        descendingStairs.Add(stairs);
    }

    public int getXMin() { return xMin; }
    public int getXMax() { return xMax; }
    public int getYMin() { return yMin; }
    public int getYMax() { return yMax; }
    public int getLevelNumber() { return levelNumber; }

    public List<StairsTile> getAllAscendingStairs()
    {
        return ascendingStairs;
    }

    public List<StairsTile> getAllDescendingStairs()
    {
        return descendingStairs;
    }

    public StairsTile getAscendingStairs(Vector3Int currentPosition)
    {
        // Get closest stairs
        if (ascendingStairs.Count > 0)
        {
            float xDistance = currentPosition.x - ascendingStairs[0].position.x;
            float yDistance = currentPosition.y - ascendingStairs[0].position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            int closestStairsIndex = 0;
            float shortestDistance = distance;
            for (int i = 1; i < ascendingStairs.Count; i++)
            {
                xDistance = currentPosition.x - ascendingStairs[i].position.x;
                yDistance = currentPosition.y - ascendingStairs[i].position.y;
                distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestStairsIndex = i;
                }
            }
            return ascendingStairs[closestStairsIndex];
        }
        else return null;
    }

    public StairsTile getDescendingStairs(Vector3Int currentPosition)
    {
        // Get closest stairs
        if (descendingStairs.Count > 0)
        {
            float xDistance = currentPosition.x - descendingStairs[0].position.x;
            float yDistance = currentPosition.y - descendingStairs[0].position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            int closestStairsIndex = 0;
            float shortestDistance = distance;
            for (int i = 1; i < descendingStairs.Count; i++)
            {
                xDistance = currentPosition.x - descendingStairs[i].position.x;
                yDistance = currentPosition.y - descendingStairs[i].position.y;
                distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestStairsIndex = i;
                }
            }
            return descendingStairs[closestStairsIndex];
        }
        else return null;
    }
}




