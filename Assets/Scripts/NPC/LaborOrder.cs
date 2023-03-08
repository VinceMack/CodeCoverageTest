using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LaborOrder
{
    private LaborType laborType;
    private float timeToComplete;
    private int orderNumber;

    private const float MIN_TTC = 0.5f;
    private const float MAX_TTC = 1.0f;

    // constructor
    public LaborOrder(LaborType laborType, float timeToComplete)
    {
        this.laborType = laborType;
        this.timeToComplete = timeToComplete;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        LaborOrderManager.addLaborOrder(this);
    }

    // random constructor
    public LaborOrder(bool isRandomConstructor)
    {
        laborType = (LaborType)UnityEngine.Random.Range(0, LaborOrderManager.getNumberOfLaborTypes());
        timeToComplete = UnityEngine.Random.Range(MIN_TTC, MAX_TTC);
        orderNumber = LaborOrderManager.getNumOfLaborOrders();
        LaborOrderManager.addLaborOrder(this);
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
}