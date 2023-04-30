using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class QuestUITests
	{

        // This LoadScene will be universal for all playmode tests
		[SetUp]
		public void Setup()
		{
            // Arrange
			SceneManager.LoadScene("QuestTestScene");
		}

        // These are the actual tests
		[UnityTest]
		public IEnumerator ColonyInfoPanelOpeningTest()
		{
            //Arrange
			yield return new WaitForSeconds(0.5f);

            GameObject.Find("Canvas/ActionList/Scroll View/Viewport/Content/CardBackground (5)").GetComponent<Button>().onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);

            Transform quest = GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/AvailableQuestContent/Scroll View/Viewport/Content").transform.GetChild(0);
            quest.GetChild(3).GetComponent<Button>().onClick.Invoke();

			yield return new WaitForSeconds(0.5f);

			quest = GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/AvailableQuestContent/Scroll View/Viewport/Content").transform.GetChild(0);
            quest.GetChild(4).GetComponent<Button>().onClick.Invoke();

            yield return new WaitForSeconds(0.5f);

            GameObject.Find("Canvas/QuestInfo/QuestListContent/CurrentQuestButton").GetComponent<Button>().onClick.Invoke();

			yield return new WaitForSeconds(1f);

            //Assert
			Assert.AreEqual(GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/ActiveQuestContent/Scroll View/Viewport/Content").transform.childCount, 1);
		}
	}
}