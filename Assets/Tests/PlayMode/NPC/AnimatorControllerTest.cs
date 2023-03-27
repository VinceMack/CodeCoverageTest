using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class AnimatorControllerTest
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("PanelManagerTest");
		}

		[UnityTest]
		public IEnumerator AnimationChangingTest()
		{
            //Arrange
			yield return new WaitForSeconds(5f);

			GameObject character = GameObject.Find("TestCharacter");

            //Act
            var result = character.GetComponent<AnimatorController>().GetAnimBool("walking");
            var result2 = character.GetComponent<AnimatorController>().GetAnimFloat("xDirection");

            //Assert
			Assert.NotNull(result);
            Assert.NotNull(result2);
		}
	}
}