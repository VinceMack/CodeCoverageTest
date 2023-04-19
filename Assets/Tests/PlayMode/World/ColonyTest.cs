using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class ColonyTest
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("NPCDamageTesting");
		}

		[UnityTest]
		public IEnumerator ResourceAdditions()
		{
            //Arrange
			yield return new WaitForSeconds(5f);
        }
    }
}
