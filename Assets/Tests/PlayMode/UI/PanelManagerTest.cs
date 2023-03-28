using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tests 
{
	public class PanelManagerTest
	{

		[OneTimeSetUp]
		public void LoadScene()
		{
			SceneManager.LoadScene("PanelManagerTest");
		}

		[UnityTest]
		public IEnumerator CloseSaveSystemUITest()
		{
            //Arrange
			yield return new WaitForSeconds(1);

            PanelManager panelManager = GameObject.Find("UI Canvas").GetComponentInChildren<PanelManager>();
            GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.SetActive(true);

            //Act
            panelManager.CloseSaveSystemUI();

            //Assert
            Assert.AreEqual(GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.activeSelf, false);
        }

        [UnityTest]
		public IEnumerator EnableSaveSystemUITest()
		{
            //Arrange
			yield return new WaitForSeconds(1);

            PanelManager panelManager = GameObject.Find("UI Canvas").GetComponentInChildren<PanelManager>();

            //Act
            panelManager.EnableSaveSystemUI();

            //Assert
            Assert.AreEqual(GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.activeSelf, true);
        }

        [UnityTest]
		public IEnumerator ToggleSaveSystemUIOpenTest()
		{
            //Arrange
			yield return new WaitForSeconds(1);

            PanelManager panelManager = GameObject.Find("UI Canvas").GetComponentInChildren<PanelManager>();
            panelManager.CloseSaveSystemUI();

            //Act
            panelManager.ToggleSaveSystemUI();

            //Assert
            Assert.AreEqual(GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.activeSelf, true);
        }

        [UnityTest]
		public IEnumerator ToggleSaveSystemUICloseTest()
		{
            //Arrange
			yield return new WaitForSeconds(1);

            PanelManager panelManager = GameObject.Find("UI Canvas").GetComponentInChildren<PanelManager>();
            panelManager.EnableSaveSystemUI();

            //Act
            panelManager.ToggleSaveSystemUI();

            //Assert
            Assert.AreEqual(GameObject.Find("UI Canvas").transform.GetChild(0).gameObject.activeSelf, false);
        }
    }
}