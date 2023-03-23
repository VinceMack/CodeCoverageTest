using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;
using System.IO;
using UnityEngine;

namespace Tests 
{
    public class SaveSystemManagerTest
    {
        SaveSystemManager saveManager;
        SaveSystemManager specialSaveManager;
        int saveSlot;
        BaseStats stats;

        [SetUp]
        public void Setup()
        {
            //Arrange
            saveManager = new SaveSystemManager();

            saveSlot = 5;
            stats = new BaseStats();
            stats.x = 44;
        }

        [Test]
        public void SaveDataFailTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.SaveData<BaseStats>($"/{saveSlot}/Save-{saveSlot}.json", stats, false)
                ).Returns(false);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            bool result = specialSaveManager.SaveData<BaseStats>(stats, saveSlot);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void SaveDataSuccessTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.SaveData<BaseStats>($"/{saveSlot}/Save-{saveSlot}.json", stats, false)
                ).Returns(true);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            bool result = specialSaveManager.SaveData<BaseStats>(stats, saveSlot);

            //Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        // This test may be breaking if you or the code created and there still exists 
        // a save folder one greater than the current Constant max
        public void DirectoryFailure()
        {
            //Arrange

            //Act
            var result = saveManager.LoadData<BaseStats>(Constants.SAVE_SLOT_NUMBER+1);

            //Assert
            Assert.AreEqual(result, default);
        }

        [Test]
        public void LoadDataFailTest()
        {
            //Arrange
            bool testCreation = false;
            if(!Directory.Exists(Application.persistentDataPath + $"/{saveSlot}"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + $"/{saveSlot}");
                testCreation = true;
            }

            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.LoadData<BaseStats>($"/{saveSlot}/Save-{saveSlot}.json", false)
                ).Throws(new System.Exception());
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            var result = specialSaveManager.LoadData<BaseStats>(saveSlot);

            // So that unecessary directories don't keep getting added
            if(testCreation)
            {
                Directory.Delete(Application.persistentDataPath + $"/{saveSlot}");
            }

            //Assert
            Assert.AreEqual(result, default);
        }

        [Test]
        public void LoadDataSuccessTest()
        {
            //Arrange
            bool testCreation = false;
            if(!Directory.Exists(Application.persistentDataPath + $"/{saveSlot}"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + $"/{saveSlot}");
                testCreation = true;
            }

            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.LoadData<BaseStats>($"/{saveSlot}/Save-{saveSlot}.json", false)
                ).Returns(stats);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            var result = specialSaveManager.LoadData<BaseStats>(saveSlot);

            // So that unecessary directories don't keep getting added
            if(testCreation)
            {
                Directory.Delete(Application.persistentDataPath + $"/{saveSlot}");
            }

            //Assert
            Assert.AreEqual(result.x, stats.x);
        }

        [Test]
        public void SaveInfoFailTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.SaveData($"/{saveSlot}/Save-Stats.json", It.IsAny<SaveStats>(), It.IsAny<bool>())
                ).Returns(false);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            bool result = specialSaveManager.SaveInfo(saveSlot);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void SaveInfoSuccessTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.SaveData($"/{saveSlot}/Save-Stats.json", It.IsAny<SaveStats>(), It.IsAny<bool>())
                ).Returns(true);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            bool result = specialSaveManager.SaveInfo(saveSlot);

            //Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void LoadInfoFailTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.LoadData<BaseStats>(It.IsAny<string>(), It.IsAny<bool>())
                ).Throws(new System.Exception());
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            var result = specialSaveManager.LoadInfo<BaseStats>(saveSlot);

            //Assert
            Assert.AreEqual(result, default);
        }

        [Test]
        public void LoadInfoSuccessTest()
        {
            //Arrange
            var serviceMock = new Mock<IDataService>(); 
            serviceMock.Setup(x => x.LoadData<BaseStats>(It.IsAny<string>(), It.IsAny<bool>())
                ).Returns(stats);
            specialSaveManager = new SaveSystemManager(serviceMock.Object);

            //Act
            var result = specialSaveManager.LoadInfo<BaseStats>(saveSlot);

            //Assert
            Assert.AreEqual(result.x, stats.x);
        }
    }
}
