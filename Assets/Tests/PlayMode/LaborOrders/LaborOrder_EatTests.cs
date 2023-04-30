using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

namespace Tests
{
    public class LaborOrder_EatTests : MonoBehaviour
    {
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

        [UnityTest]
        public IEnumerator ExecuteTest()
        {
            GridManager.PopulateWithChest();
            GameObject chest = GameObject.Find("Chest(Clone)");
            chest.GetComponent<Chest>().AddItem("Berries");

            Pawn.DecrementAllHunger(Pawn.MAX_HUNGER - Pawn.HUNGER_RESPONSE_THRESHOLD);

            yield return new WaitForSeconds(15);

            Assert.AreEqual(chest.GetComponent<Chest>().GetItemQuantity("Berries"), 0);
        }
    }
}