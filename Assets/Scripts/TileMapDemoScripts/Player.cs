using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    private List<Vector3> path;         // Player's path to target location
    private int currentLevel;           // Player's current level
    private int targetLevel;            // Level of target location
    private Vector3Int targetLocation;  // Player's target location

    private void Awake()
    {
        path = new List<Vector3>();
        currentLevel = 0;
        targetLevel = 0;
        targetLocation = new Vector3Int(0, 0, 0);
    }

    void Start()
    {
    }

    void Update()
    {
    }

    public void updateLocation(float speed)
    {
        if(path.Count > 0){
            transform.position = Vector3.MoveTowards(transform.position, path.Last(), speed);
            if (transform.position == path.Last()) path.RemoveAt(path.Count - 1);
	    }
    }

    public void updatePath(List<Vector3> newPath)
    {
        path.Clear();
        path = newPath;
    }

    public void setTargetLocation(Vector3Int targetLocation)
    {
        this.targetLocation = targetLocation;
    }

    public Vector3Int getTargetLocation()
    {
        return targetLocation;
    }

    public void setCurrentLevel(int newLevel)
    {
        currentLevel = newLevel;
    }

    public int getCurrentLevel()
    {
        return currentLevel;
    }

    public void setTargetLevel(int newTargetLevel)
    {
        targetLevel = newTargetLevel;
    }

    public int getTargetLevel()
    {
        return targetLevel;
    }

    public int getPathCount()
    {
        return path.Count;
    }
}
