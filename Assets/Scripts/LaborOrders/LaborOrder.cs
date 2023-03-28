using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder
{
    protected LaborType laborType;
    protected float timeToComplete;
    protected int orderNumber;
    protected Pawn assignedPawn = null;

    private const float MIN_TTC = 0.5f;
    private const float MAX_TTC = 1.0f;

    // constructor
    public LaborOrder(LaborType laborType, float timeToComplete)
    {
        this.laborType = laborType;
        this.timeToComplete = timeToComplete;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        //LaborOrderManager.addLaborOrder(this); // needs to be updated
    }

    // random constructor
    public LaborOrder(bool isRandomConstructor)
    {
        laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager.getNumberOfLaborTypes());
        //while(laborType == LaborType.Woodcut) laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager.getNumberOfLaborTypes());
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        //LaborOrderManager.addLaborOrder(this); // needs to be updated
    }

    public LaborOrder()
    {
    }

    // returns the labor type of the labor order
    public LaborType getLaborType() {
        return laborType;
    }

    // returns the time to complete the labor order
    public float getTimeToComplete() {
        return timeToComplete;
    }

    // returns the order number of the labor order
    public int getOrderNumber() {
        return orderNumber;
    }

    // sets the assignedPawn field
    public void assignToPawn(Pawn assignedPawn)
    {
        this.assignedPawn = assignedPawn;
    }

    // executes the labor order for the assigned pawn. Overridden by labor specific classes
    public virtual IEnumerator execute()
    {
        if(assignedPawn != null)
        {
            // wait the time to complete the labor order
            yield return new WaitForSeconds(getTimeToComplete());
        }
        // add the pawn back to the queue of available pawns
        //LaborOrderManager.addPawn(assignedPawn); // needs to be updated

        yield break;
    }
}