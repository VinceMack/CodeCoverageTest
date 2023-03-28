using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class SaveSystemUIManagerPlayModeTests
	{

		// [OneTimeSetUp]
		// public void LoadScene()
		// {
		// 	SceneManager.LoadScene("BasicSaveLoadTest");
		// }

		[UnityTest]
		public IEnumerator OpenSaveMenuTest()
		{
			SceneManager.LoadScene("BasicSaveLoadTest");
			yield return new WaitForSeconds(1);

			GameObject mine = GameObject.Find("Save System Panel");
			yield return null;
			mine.SetActive(true);
			mine.GetComponent<SaveSystemUIManager>().OpenSaveMenu();

			Assert.AreEqual(mine.GetComponent<SaveSystemUIManager>().GetContent().transform.childCount, Constants.SAVE_SLOT_NUMBER+1);
		}

		[UnityTest]
		public IEnumerator SaveLoadTest()
		{
			SceneManager.LoadScene("BasicSaveLoadTest");

			yield return new WaitForSeconds(1);
			EntityDictionary ed = GlobalInstance.Instance.entityDictionary;
			ed.entityDictionary.Clear();
			PrefabList pl = GlobalInstance.Instance.prefabList;

			ed.InstantiateEntity(pl.prefabList[0].entityName);

			GameObject mine = GameObject.Find("Save System Panel");
			yield return null;
			
			//Act
			mine.GetComponent<SaveSystemUIManager>().Save(0);
			mine.GetComponent<SaveSystemUIManager>().Load(0);

			//Assert
			Assert.AreEqual(ed.entityDictionary.Count, 1);

			//Clean-up
			ed.entityDictionary.Clear();
		}

		[UnityTest]
		public IEnumerator ActiveSlotTest()
		{
			SceneManager.LoadScene("BasicSaveLoadTest");

			yield return new WaitForSeconds(1);
			EntityDictionary ed = GlobalInstance.Instance.entityDictionary;
			ed.entityDictionary.Clear();
			PrefabList pl = GlobalInstance.Instance.prefabList;

			ed.InstantiateEntity(pl.prefabList[0].entityName);

			GameObject mine = GameObject.Find("Save System Panel");
			yield return null;
			
			//Act
			mine.GetComponent<SaveSystemUIManager>().activeSlot = 0;
			mine.GetComponent<SaveSystemUIManager>().SaveActiveSlot();
			mine.GetComponent<SaveSystemUIManager>().LoadActiveSlot();
			mine.GetComponent<SaveSystemUIManager>().DeleteActiveSlot();

			//Assert
			Assert.AreEqual(ed.entityDictionary.Count, 1);

			//Clean-up
			ed.entityDictionary.Clear();			
		}
	}
}