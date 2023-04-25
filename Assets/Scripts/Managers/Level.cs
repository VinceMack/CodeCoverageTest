using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for map level data
public class Level
{
    private int levelNumber;
    private int xMin, xMax, yMin, yMax;

    private List<StairsTile_VM> ascendingStairs_VM;
    private List<StairsTile_VM> descendingStairs_VM;

    public Level()
    {
        levelNumber = -1;
        xMin = 0; xMax = 0; yMin = 0; yMax = 0;

        ascendingStairs_VM = new List<StairsTile_VM>();
        descendingStairs_VM = new List<StairsTile_VM>();
    }

    public Level(int lNum, int xMin, int xMax, int yMin, int yMax)
    {
        this.levelNumber = lNum;
        this.xMin = xMin;
        this.xMax = xMax;
        this.yMin = yMin;
        this.yMax = yMax;


        ascendingStairs_VM = new List<StairsTile_VM>();
        descendingStairs_VM = new List<StairsTile_VM>();
    }

    public void setLevel(int lNum, Vector2Int position, int xLength, int yLength)
    {
        levelNumber = lNum;
        xMin = position.x;
        yMin = position.y;
        xMax = xMin + xLength;
        yMax = yMin = yLength;
    }

    public void AddAscendingStairs_VM(StairsTile_VM stairs)
    {
        ascendingStairs_VM.Add(stairs);
    }

    public void AddDescendingStairs_VM(StairsTile_VM stairs)
    {
        descendingStairs_VM.Add(stairs);
    }

    public int getXMin() { return xMin; }
    public int getXMax() { return xMax; }
    public int getYMin() { return yMin; }
    public int getYMax() { return yMax; }
    public int getLevelNumber() { return levelNumber; }

    public StairsTile_VM getAscendingStairs_VM(Vector3Int currentPosition)
    {
        // Get closest stairs
        if (ascendingStairs_VM.Count > 0)
        {
            float xDistance = currentPosition.x - ascendingStairs_VM[0].position.x;
            float yDistance = currentPosition.y - ascendingStairs_VM[0].position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            int closestStairsIndex = 0;
            float shortestDistance = distance;
            for (int i = 1; i < ascendingStairs_VM.Count; i++)
            {
                xDistance = currentPosition.x - ascendingStairs_VM[i].position.x;
                yDistance = currentPosition.y - ascendingStairs_VM[i].position.y;
                distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestStairsIndex = i;
                }
            }
            return ascendingStairs_VM[closestStairsIndex];
        }
        else return null;
    }

    public StairsTile_VM getDescendingStairs_VM(Vector3Int currentPosition)
    {
        // Get closest stairs
        if (descendingStairs_VM.Count > 0)
        {
            float xDistance = currentPosition.x - descendingStairs_VM[0].position.x;
            float yDistance = currentPosition.y - descendingStairs_VM[0].position.y;
            float distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

            int closestStairsIndex = 0;
            float shortestDistance = distance;
            for (int i = 1; i < descendingStairs_VM.Count; i++)
            {
                xDistance = currentPosition.x - descendingStairs_VM[i].position.x;
                yDistance = currentPosition.y - descendingStairs_VM[i].position.y;
                distance = Mathf.Sqrt(Mathf.Pow(xDistance, 2) + Mathf.Pow(yDistance, 2));

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    closestStairsIndex = i;
                }
            }
            return descendingStairs_VM[closestStairsIndex];
        }
        else return null;
    }
}
