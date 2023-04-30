using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace Tests
{
    public class LaborOrder_CraftTests : MonoBehaviour
    {
        // This LoadScene will be universal for all playmode tests
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("CombinedManagersTesting", LoadSceneMode.Single);
            yield return null;
            yield return new EnterPlayMode();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
        }

        // Function 1: Test if scene for GameManager is setup correctly
        [UnityTest]
        public IEnumerator ExecuteTest()
        {
            GridManager.PopulateWithChest();
            // find the chest and add 5 wood item prefabs to it 
            GameObject chest = GameObject.Find("Chest(Clone)");
            chest.GetComponent<Chest>().AddItem("Wood");
            chest.GetComponent<Chest>().AddItem("Wood");
            chest.GetComponent<Chest>().AddItem("Wood");
            chest.GetComponent<Chest>().AddItem("Wood");
            chest.GetComponent<Chest>().AddItem("Wood");
            // try craft in a while loop until UNREACHABLE is not printed. use regex to control while.
            do
            {
                LaborOrderManager.AddCraftLaborOrder(Resources.Load<Chest>("prefabs/items/Chest"), new Vector2(52, 25));
            } while (LogHandler.UnreachableDetected);

            LaborOrderManager.AssignPawnsToLaborOrders();
            Assert.AreEqual(LaborOrderManager.GetWorkingPawnCount(), 1);

            yield return new WaitForSeconds(5);
            Assert.AreEqual(GameObject.FindObjectsOfType<Chest>().Length, 2);
        }
    }
}

public static class LogHandler
{
    public static bool UnreachableDetected { get; private set; } = false;

    static LogHandler()
    {
        Application.logMessageReceived += HandleLog;
    }

    public static void Enable()
    {
        Application.logMessageReceived += HandleLog;
    }

    public static void Disable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Log)
        {
            string pattern = @"UNREACHABLE";
            if (Regex.IsMatch(logString, pattern))
            {
                UnreachableDetected = true;
            }
            else
            {
                UnreachableDetected = false;
            }
        }
    }
}

