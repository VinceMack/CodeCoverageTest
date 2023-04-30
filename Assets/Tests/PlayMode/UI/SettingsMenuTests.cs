using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Tests
{
    public class SettingsMenuTests
    {
        [UnitySetUp]
        public IEnumerator SetUp()
        {
            SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
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
        public IEnumerator ChangeVolumeTest()
        {
            //Arrange
            yield return new WaitForSeconds(0.5f);

            //Act
            GameObject.Find("Main Menu Test/Home Screen/Buttons/Settings Button").GetComponent<Button>().onClick.Invoke();

            GameObject.Find("Main Menu Test/Settings Menu/Settings Canvas/Image/ContentBackground/Volume Background/InputField (TMP)").GetComponent<TMP_InputField>().text = 20.ToString();

            //Assert
            Assert.AreEqual(GameObject.Find("Main Menu Test/Settings Menu/Settings Canvas/Image/ContentBackground/Volume Background/Slider").GetComponent<Slider>().value, 20f);
        }

        // These are the actual tests
        [UnityTest]
        public IEnumerator ChangeFullscreenTest()
        {
            //Arrange
            yield return new WaitForSeconds(0.5f);

            //Act
            GameObject.Find("Main Menu Test/Home Screen/Buttons/Settings Button").GetComponent<Button>().onClick.Invoke();

            GameObject.Find("Main Menu Test/Settings Menu/Settings Canvas/Image/ContentBackground/Fullscreen Background/Toggle").GetComponent<Toggle>().isOn = false;

            //Assert
            Assert.AreEqual(GameObject.Find("Main Menu Test/Settings Menu/Settings Canvas/Image/ContentBackground/Fullscreen Background/Toggle").GetComponent<Toggle>().isOn, false);
        }
    }
}