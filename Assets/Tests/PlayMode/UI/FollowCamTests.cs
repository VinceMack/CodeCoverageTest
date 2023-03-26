using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class FollowCamTests
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("GameObjectTileTests");
		}

		[UnityTest]
		public IEnumerator FollowCamTest()
		{
			yield return new WaitForSeconds(1);

			GameObject cam = GameObject.Find("Main Camera");

			Assert.AreEqual(cam.transform.position, new Vector3(0, 0, cam.transform.position.z));
		}
	}
}