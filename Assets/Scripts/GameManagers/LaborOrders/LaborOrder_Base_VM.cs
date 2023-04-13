using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LaborOrder_Base_VM
{
    public LaborType laborType      { get; private set; }           // type of labor needed
    public Vector3Int location;                                     // location of the labor order in the grid
    public float timeToComplete     { get; private set; }           // time it takes to complete the labor order
    public int orderNumber          { get; private set; }           // order number of the labor order

    public const float MIN_TTC = 1.0f;                          // minimum time to complete
    public const float MAX_TTC = 3.0f;                          // maximum time to complete

    // constructor
    public LaborOrder_Base_VM(LaborType laborType, Vector3Int location, float timeToComplete)
    {
        this.laborType = laborType;
        this.location = location;
        this.timeToComplete = timeToComplete;
        orderNumber = LaborOrderManager_VM.GetLaborOrderCount();
    }

    // random constructor
    public LaborOrder_Base_VM(bool isRandomConstructor)
    {
        // choose a random labor type and time to complete
        laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager_VM.GetLaborTypesCount());
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);

        // Get a random level
        int randomLevelIndex = UnityEngine.Random.Range(0, GridManager.mapLevels.Count);
        Level level = GridManager.mapLevels[randomLevelIndex];
        // Get a random x and y
        int randomX = UnityEngine.Random.Range(level.getXMin(), level.getXMax());
        int randomY = UnityEngine.Random.Range(level.getYMin(), level.getYMax());

        // Set labor order location
        location = new Vector3Int(randomX, randomY, 0);
        
        BaseTile_VM tile = GridManager.GetTile(location);
        if (tile == null)
        {
            Debug.LogError("Tile is null.");
        }
        else
        {
            while (GridManager.GetTile(location).isCollision == true)
            {
                randomX = UnityEngine.Random.Range(level.getXMin(), level.getXMax());
                randomY = UnityEngine.Random.Range(level.getYMin(), level.getYMax());
                location = new Vector3Int(randomX, randomY, 0);
            }

            // Set the order number and add the labor order to the LaborOrderManager_VM
            orderNumber = LaborOrderManager_VM.GetLaborOrderCount();
        }
    }

    // default constructor
    public LaborOrder_Base_VM()
    {
        laborType = LaborType.Basic;
        timeToComplete = 0.0f;
        orderNumber = LaborOrderManager_VM.GetLaborOrderCount();
    }

    // method to return the location of the labor order
    public Vector3 GetLocation()
    {
        // return GetCellCenterWorld of the location of the labor order
        return GridManager.grid.GetCellCenterWorld(location);

    }

    // method to execute the labor order for the assigned pawn. Overridden by labor specific classes
    public virtual IEnumerator Execute(Pawn_VM pawn)
    {
        yield return new WaitForSeconds(timeToComplete);
    }
}
