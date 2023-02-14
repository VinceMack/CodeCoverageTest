using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class TweakingTest
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("SampleScene");
		}

		[UnityTest]
		public IEnumerator RuntimeTest () 
		{
			GameObject ob = GameObject.Find("TestCharacter");
			yield return null;
			Assert.AreEqual(0, ob.transform.position.z);
		}

	}
}