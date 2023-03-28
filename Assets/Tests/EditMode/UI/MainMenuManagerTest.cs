using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{

    public class MainMenuManagerTest
    {

        MainMenuManager mainManager;
        GameObject mainMenu;

        // home screen
        GameObject homeScreen;

        // menu panels
        GameObject newMenu;
        GameObject loadMenu;
        GameObject settingsMenu;
        GameObject exitMenu;

        [SetUp]
        public void SetUp()
        {
            mainManager = new MainMenuManager();
            mainMenu = new GameObject();
            mainManager = mainMenu.AddComponent(typeof(MainMenuManager)) as MainMenuManager;

            homeScreen = new GameObject();
            newMenu = new GameObject();
            loadMenu = new GameObject();
            settingsMenu = new GameObject();
            exitMenu = new GameObject();

            mainManager.homeScreen = homeScreen;
            mainManager.newMenu = newMenu;
            mainManager.loadMenu = loadMenu;
            mainManager.settingsMenu = settingsMenu;
            mainManager.exitMenu = exitMenu;
        }

        [Test]
        public void disableMenusTest()
        {
            mainManager.disableMenus();

            Assert.IsTrue(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf);
        }

        [Test]
        public void returnHomeTest()
        {
            mainManager.returnHome();

            Assert.IsTrue(mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf);
        }

        [Test]
        public void selectNewOptionTest()
        {
            mainManager.returnHome();
            mainManager.selectOption("new");

            Assert.IsTrue(!mainManager.homeScreen.activeSelf && mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf);
        }

        [Test]
        public void selectLoadOptionTest()
        {
            mainManager.selectOption("load");

            Assert.IsTrue(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf);
        }

        [Test]
        public void selectSettingsOptionTest()
        {
            mainManager.returnHome();
            mainManager.selectOption("settings");

            Assert.IsTrue(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && mainManager.settingsMenu.activeSelf
            && !mainManager.exitMenu.activeSelf);
        }

        [Test]
        public void selectExitOptionTest()
        {
            mainManager.returnHome();
            mainManager.selectOption("exit");

            Assert.IsTrue(!mainManager.homeScreen.activeSelf && !mainManager.newMenu.activeSelf
            && !mainManager.loadMenu.activeSelf && !mainManager.settingsMenu.activeSelf
            && mainManager.exitMenu.activeSelf);
        }

        /*
        [Test]
        public void PlayGame()
        {
            mainManager.PlayGame();
            var targetScene = SceneManager.GetActiveScene().buildIndex + 1;
            var activeScene = SceneManager.GetActiveScene();

            Assert.AreEqual(targetScene, activeScene);
        }

        [Test]
        public void LoadSaveGame()
        {
            mainManager.PlayGame();
            var targetScene = SceneManager.GetActiveScene.buildIndex + 1;
            var activeScene = SceneManager.GetActiveScene();

            Assert.AreEqual(targetScene, activeScene);
        }
        */

    }

}
