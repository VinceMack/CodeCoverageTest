using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

public class LaborOrderManager_VM_Tests
{
    private LaborOrderManager_VM laborOrderManager;
    private GameObject laborOrderManagerGO;

    [SetUp]
    public void SetUp()
    {
        // create new GO for adding the LaborOrderManager_VM component
        laborOrderManagerGO = new GameObject();
        laborOrderManager= laborOrderManagerGO.AddComponent<LaborOrderManager_VM>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(laborOrderManagerGO);
    }

    [Test]
    public void TestNumberOfLaborTypes()
    {
        // Arrange
        int expectedLaborTypes = Enum.GetNames(typeof(LaborType)).Length;

        // Act
        int actualLaborTypes = LaborOrderManager_VM.GetNumberOfLaborTypes();

        // Assert
        Assert.AreEqual(expectedLaborTypes, actualLaborTypes);
    }
}