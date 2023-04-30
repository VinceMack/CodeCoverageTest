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
		public IEnumerator QuestAcceptTest()
		{
            //Arrange
			yield return new WaitForSeconds(0.5f);

            GameObject.Find("Canvas/ActionList/Scroll View/Viewport/Content/CardBackground (5)").GetComponent<Button>().onClick.Invoke();
            GameObject.Find("Canvas/ActionList/Scroll View/Viewport/Content/CardBackground (5)").GetComponent<Button>().onClick.Invoke();
            GameObject.Find("Canvas/ActionList/Scroll View/Viewport/Content/CardBackground (5)").GetComponent<Button>().onClick.Invoke();
            
            yield return new WaitForSeconds(0.5f);

            Transform quest = GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/AvailableQuestContent/Scroll View/Viewport/Content").transform.GetChild(0);
            quest.GetChild(3).GetComponent<Button>().onClick.Invoke();

			yield return new WaitForSeconds(0.5f);

			quest = GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/AvailableQuestContent/Scroll View/Viewport/Content").transform.GetChild(0);
            quest.GetChild(4).GetComponent<Button>().onClick.Invoke();

            yield return new WaitForSeconds(0.5f);

            GameObject.Find("Canvas/QuestInfo/QuestListContent/CurrentQuestButton").GetComponent<Button>().onClick.Invoke();
            GameObject.Find("Canvas/QuestInfo/QuestListContent/AvailableQuestButton").GetComponent<Button>().onClick.Invoke();
            GameObject.Find("Canvas/QuestInfo/QuestListContent/CurrentQuestButton").GetComponent<Button>().onClick.Invoke();

			yield return new WaitForSeconds(1f);

            //Assert
			Assert.AreEqual(GameObject.Find("Canvas/QuestInfo/QuestListContent/QuestContentOutline/AvailableQuestBackground/ActiveQuestContent/Scroll View/Viewport/Content").transform.childCount, 1);

            GameObject.Find("GameManager").GetComponent<QuestManager>().CancelAllQuests();
		}
	}
}