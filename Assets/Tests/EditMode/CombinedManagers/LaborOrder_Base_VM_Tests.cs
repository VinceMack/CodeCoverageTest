using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LaborOrder_Base_VM_Tests
{
    LaborOrderManager_VM laborOrderManager;

    [SetUp]
    public void SetUp()
    {
        GameObject go = new GameObject();
        laborOrderManager = go.AddComponent<LaborOrderManager_VM>();
        laborOrderManager.InitializeLaborQueues();
    }

    [Test]
    public void Constructor_WithParameters_SetsPropertiesCorrectly()
    {
        //ARRANGE
        LaborType laborType = LaborType.Cook;
        float timeToComplete = 1.5f;

        //ACT
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);

        //ASSERT
        Assert.AreEqual(laborType, laborOrder.GetLaborType());
        Assert.AreEqual(timeToComplete, laborOrder.GetTimeToComplete());
    }

    [Test]
    public void DefaultConstructor_SetsPropertiesToDefault()
    {
        //ARRANGE
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM();

        //ACT
        LaborType laborType = laborOrder.GetLaborType();
        float timeToComplete = laborOrder.GetTimeToComplete();

        //ASSERT
        Assert.AreEqual(default(LaborType), laborType);
        Assert.AreEqual(0, timeToComplete);
    }

    [Test]
    public void GetLaborType_ReturnsCorrectLaborType()
    {
        //ARRANGE
        LaborType laborType = LaborType.Research;
        float timeToComplete = 2.0f;
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);

        //ACT
        LaborType result = laborOrder.GetLaborType();

        //ASSERT
        Assert.AreEqual(laborType, result);
    }

    [Test]
    public void GetTimeToComplete_ReturnsCorrectTimeToComplete()
    {
        //ARRANGE
        LaborType laborType = LaborType.Woodcut;
        float timeToComplete = 3.5f;
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);

        //ACT
        float result = laborOrder.GetTimeToComplete();

        //ASSERT
        Assert.AreEqual(timeToComplete, result);
    }

    [Test]
    public void GetOrderNumber_ReturnsCorrectOrderNumber()
    {
        //ARRANGE
        LaborType laborType = LaborType.Mine;
        float timeToComplete = 4.0f;
        LaborOrder_Base_VM laborOrder = new LaborOrder_Base_VM(laborType, timeToComplete);
        int orderNumber = 0;

        //ACT
        int result = laborOrder.GetOrderNumber();

        //ASSERT
        Assert.AreEqual(orderNumber, result);
    }
}
