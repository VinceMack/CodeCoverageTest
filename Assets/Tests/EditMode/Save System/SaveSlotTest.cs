using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests 
{
    public class SaveSlotTest
    {
        SaveSlot testSlot;
        SaveSystemUIManager uiManager;
        int saveSlot = 4;

        [SetUp]
        public void Setup()
        {
            //Arrange
            testSlot = new SaveSlot();
            TextMeshProUGUI textName = new TextMeshProUGUI();
            TextMeshProUGUI textInfo = new TextMeshProUGUI();
            testSlot.SetTexts(textName, textInfo);

            uiManager = new SaveSystemUIManager();
        }

        [Test]
        public void NoSaveDetectedTest() 
        {
            //Arrange

            //Act
            testSlot.Initialize(saveSlot, new SaveSystemManager(), new SaveSystemUIManager(), false);

            //Assert
            Assert.AreEqual(testSlot.GetSaveInfoText(), "No save detected.");
        }

        [Test]
        public void SaveDetectedTest() 
        {
            //Arange
            SaveStats stats = new SaveStats();
            stats.dateLastPlayed = "2";
            stats.timeLastPlayed = "1";
            
            var saveManager = new Mock<ISaveSystemManager>(); 
            saveManager.Setup(x => x.LoadInfo<SaveStats>(It.IsAny<int>())).Returns(stats);

            //Act
            testSlot.Initialize(saveSlot, saveManager.Object, new SaveSystemUIManager(), true);

            //Assert
            Assert.AreEqual(testSlot.GetSaveInfoText(), "Last Played: 2 1");
        }

        [Test]
        public void SelectSlotTest() 
        {
            //Arrange

            //Act
            testSlot.Initialize(saveSlot, new SaveSystemManager(), uiManager, false);
            testSlot.SelectSlot();

            //Assert
            Assert.AreEqual(uiManager.activeSlot, saveSlot);
        }
    }
}
