using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LaborOrder_Base_VM
{
    // properties of the labor order
    protected LaborType laborType;                    // type of labor needed
    protected Vector3Int location;                    // location of the labor order in the grid
    protected float timeToComplete;                   // time it takes to complete the labor order
    protected int orderNumber;                   // order number of the labor order

    // constants used for random constructor
    public const float MIN_TTC = 0.5f;               // minimum time to complete
    public const float MAX_TTC = 1.0f;               // maximum time to complete

    // constructor
    public LaborOrder_Base_VM(LaborType laborType, float timeToComplete)
    {
        this.laborType = laborType;
        this.timeToComplete = timeToComplete;
        orderNumber = LaborOrderManager_VM.GetNumOfLaborOrders();
    }

    // random constructor
    public LaborOrder_Base_VM(bool isRandomConstructor)
    {
        // choose a random labor type and time to complete
        laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager_VM.GetNumberOfLaborTypes());
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);

        // choose a random location within the grid that does not have a tile with collision Set to true
        location = new Vector3Int(UnityEngine.Random.Range(GridManager.MIN_HORIZONTAL, GridManager.MAX_HORIZONTAL), UnityEngine.Random.Range(GridManager.MIN_VERTICAL, GridManager.MAX_VERTICAL), 0);

        BaseTile_VM tile = GridManager.GetTile(location);
        if (tile == null)
        {
            Debug.LogError("Tile is null.");
        }else{
            while (GridManager.GetTile(location).GetCollision() == true)
            {
                location = new Vector3Int(UnityEngine.Random.Range(GridManager.MIN_HORIZONTAL, GridManager.MAX_HORIZONTAL), UnityEngine.Random.Range(GridManager.MIN_VERTICAL, GridManager.MAX_VERTICAL), 0);
            }

            // Set the order number and add the labor order to the LaborOrderManager_VM
            orderNumber = LaborOrderManager_VM.GetNumOfLaborOrders();
        }
    }

    // default constructor
    public LaborOrder_Base_VM()
    {

    }

    // method to return the labor type of the labor order
    public LaborType GetLaborType()
    {
        return laborType;
    }

    // method to return the time to complete the labor order
    public float GetTimeToComplete()
    {
        return timeToComplete;
    }

    // method to return the order number of the labor order
    public int GetOrderNumber()
    {
        return orderNumber;
    }

    // method to return the location of the labor order
    public Vector3Int GetLaborOrderLocation()
    {
        return location;
    }

    // method to return the location of the labor order
    public Vector3 GetLocation()
    {
        return location;
    }

    // method to execute the labor order for the assigned pawn. Overridden by labor specific classes
    public virtual IEnumerator Execute(Pawn_VM pawn)
    {
        yield return new WaitForSeconds(timeToComplete);
    }
}
