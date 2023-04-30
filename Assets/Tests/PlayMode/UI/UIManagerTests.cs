using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Tests 
{
	public class UIManagerTests
	{
		[UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("QuestTestScene", LoadSceneMode.Single);
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
		public IEnumerator TestModeChanging()
		{
            //Arrange
            yield return new WaitForSeconds(0.5f);
            
            //Act
            UIManager.ToggleMode(1);
            UIManager.ToggleMode(2);
            UIManager.ToggleMode(7);
            UIManager.ToggleMode(8);
            UIManager.ToggleMode(8);

            //Assert
            Assert.AreEqual((int)UIManager.myMode, 1);
        }
    }
}