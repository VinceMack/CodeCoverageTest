using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests
{
    public class ExamplePlayModeTest
    {

        // This LoadScene will be universal for all playmode tests
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Will load scene by this name,
            // NOTE: Scene needs to be added to the Build Settings
            SceneManager.LoadScene("ChestPlacementTesting", LoadSceneMode.Single);
            yield return null;
            yield return new EnterPlayMode();
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            yield return new ExitPlayMode();
        }

        // These are the actual tests
        [UnityTest]
        public IEnumerator ColonyInfoPanelOpeningTest()
        {
            //Arrange
            // Wait for a bit to allow the scene to load
            yield return new WaitForSeconds(0.5f);

            // This will grab the GameObject in the scene by the name of "TestCharacter"
            GameObject canvas = GameObject.Find("Canvas");

            // This format is needed if you wanted to grab a gameobject which is a child of another or many
            GameObject colonyInfoButton = GameObject.Find("Canvas/ActionList/Scroll View/Viewport/Content/CardBackground (4)");

            //Act
            // This simulates a click on a gameObject
            colonyInfoButton.GetComponent<Button>().onClick.Invoke();

            GameObject colonyInfoPanel = GameObject.Find("Canvas/ColonyOverview");

            //Assert
            Assert.AreEqual(colonyInfoPanel.activeSelf, true);
        }
    }
}



