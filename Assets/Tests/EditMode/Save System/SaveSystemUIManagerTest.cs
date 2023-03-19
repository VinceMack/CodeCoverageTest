using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;
using System.IO;
using UnityEngine;

namespace Tests 
{
    public class SaveSystemUIManagerTest
    {
        SaveSystemUIManager uiManager;
        GameObject content;
        GameObject saveSlot;

        [SetUp]
        public void Setup()
        {
            //Arrange
            content = GameObject.Instantiate(new GameObject());
            var saveSlotMock = new Mock<GameObject>(); 
            saveSlotMock.Setup(x => x.GetComponent<SaveSlot>()
                ).Returns(new SaveSlot());

            uiManager = new SaveSystemUIManager(content, saveSlotMock.Object);   
        }

        // [Test]
        // public void LoadActiveSlotTest()
        // {
        //     Assert.AreEqual(uiManager.TestMoq(), true);
        // }  

        // public void OpenSaveMenuTest()
        // {
        //     //Arrange
        //     GameObject oldChild = GameObject.Instantiate(new GameObject());
        //     oldChild.transform.SetParent(content.transform);
        // }


    // [ContextMenu("GenerateSaveSlots")]
    // public void OpenSaveMenu()
    // {
    //     foreach(Transform child in content.transform)
    //     {
    //         Destroy(child.gameObject);
    //     }
    //     for(int i = 1; i <= Constants.SAVE_SLOT_NUMBER; i++)
    //     {
    //         GameObject slot = Instantiate(saveSlot);
    //         slot.transform.SetParent(content.transform);
    //         slot.GetComponent<SaveSlot>().Initialize(i, mySaveManager, this, Directory.Exists(Application.persistentDataPath + $"/{i}"));
    //     }
    // }

    }
}