using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;

/*
public class Pawn_VM_Tests
{
    private GameObject testGameObject;
    private Pawn_VM testPawn_VM;

    [SetUp]
    public void SetUp()
    {
        testGameObject = new GameObject();
        testPawn_VM = testGameObject.AddComponent<Pawn_VM>();
        testPawn_VM.InitializeLaborTypePriority(); // Add this line to initialize the LaborTypePriority array
    }

    [TearDown]
    public void TearDown()
    {
        GameObject.DestroyImmediate(testGameObject);
    }

    [Test]
    public void MoveLaborTypeToPriorityLevel_ChangesPriorityLevel()
    {
        // Arrange
        LaborType testLaborType = LaborType.FireFight;
        int initialPriorityLevel = -1;
        int newPriorityLevel = 2;

        // Find the initial priority level of the test labor type
        for (int i = 0; i < testPawn_VM.GetLaborTypePriority().Length; i++)
        {
            if (testPawn_VM.GetLaborTypePriority()[i].Contains(testLaborType))
            {
                initialPriorityLevel = i;
                break;
            }
        }

        // Check if the initial priority level is found and within the allowed range
        if (initialPriorityLevel >= 0 && initialPriorityLevel < testPawn_VM.GetLaborTypePriority().Length)
        {
            // Act
            testPawn_VM.MoveLaborTypeToPriorityLevel(testLaborType, newPriorityLevel);

            // Assert
            Assert.IsTrue(testPawn_VM.GetLaborTypePriority()[newPriorityLevel].Contains(testLaborType));
            Assert.IsTrue(testPawn_VM.GetLaborTypePriority()[newPriorityLevel].Count > 0);
        }
        else
        {
            Assert.Fail("Initial priority level not found or out of range");
        }
    }




    [Test]
    public void SetPawnName_GetPawnName_ReturnsCorrectName()
    {
        // Arrange
        string testPawnName = "TestPawn";

        // Act
        testPawn_VM.SetPawnName(testPawnName);

        // Assert
        Assert.AreEqual(testPawnName, testPawn_VM.GetPawnName());
    }

    [Test]
    public void SetPath_GetPath_ReturnsCorrectPath()
    {
        // Arrange
        List<Vector3> testPath = new List<Vector3>
        {
            new Vector3(0, 0, 0),
            new Vector3(1, 1, 1),
            new Vector3(2, 2, 2),
            new Vector3(3, 3, 3),
        };

        // Act
        testPawn_VM.SetPath(testPath);

        // Assert
        Assert.AreEqual(testPath, testPawn_VM.GetPath());
    }
}
*/