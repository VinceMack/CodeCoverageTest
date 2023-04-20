using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class BaseNPCTest
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("NPCDamageTesting");
		}

		[UnityTest]
		public IEnumerator DamageTest()
		{
            //Arrange
			yield return new WaitForSeconds(5f);

			BaseNPC character = GameObject.Find("OldWomanNPC/OldWoman").GetComponent<BaseNPC>();
            character.GetHealth().SetUpHealth(5);

            //Act
            character.TakeDamage(2);

            yield return new WaitForSeconds(2.5f);

            //Assert
			Assert.AreEqual(3, character.GetHealth().GetCurrentHealth());
		}

        [UnityTest]
		public IEnumerator DeathTest()
		{
            //Arrange
			yield return new WaitForSeconds(5f);

			BaseNPC character = GameObject.Find("OldManNPC/OldMan").GetComponent<BaseNPC>();
            character.GetHealth().SetUpHealth(5);

            //Act
            character.TakeDamage(5);

            //Assert
			Assert.AreEqual(0, character.GetHealth().GetCurrentHealth());
		}
	}
}
