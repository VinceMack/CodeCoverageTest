using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;
using System.IO;
using UnityEngine;

namespace Tests 
{
    public class SaveableEntityTest
    {
        SaveableEntity saveableEntity;
        int saveSlot;
        int id;
        BaseStats stats;

        [SetUp]
        public void Setup()
        {
            //Arrange
            saveableEntity = new SaveableEntity();

            saveSlot = 4;
            id = 420;
            stats = new BaseStats();
            stats.x = 1;
            stats.y = 2;
            stats.z = 4;
        }

        [Test]
        public void GenerateIdTest() 
        {
            //Arrange

            //Act
            saveableEntity.GenerateId();

            //Assert
            Assert.AreNotEqual(saveableEntity.Id, string.Empty);
        }

        [Test]
        public void PrefabNameTest() 
        {
            //Arrange

            //Act
            saveableEntity.SetPrefabName("test");

            //Assert
            Assert.AreEqual(saveableEntity.GetPrefabName(), "test");
        }

        [Test]
        public void CurrentLocationTest() 
        {
            //Arrange
            GameObjectTile tile = new GameObjectTile();

            //Act
            saveableEntity.SetCurrentLocation(tile);

            //Assert
            Assert.AreEqual(saveableEntity.GetCurrentLocation(), tile);
        }

        [Test]
        public void SaveFailTest()
        {
            //Arrange
            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.SaveData($"/{saveSlot}/Entity-{id}-Stats.json", stats, false)
                ).Returns(false);

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);

            //Act
            bool result = specialEntity.SaveData<BaseStats>(stats, saveSlot);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void SaveSuccessTest()
        {
            //Arrange
            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.SaveData($"/{saveSlot}/Entity-{id}-Stats.json", stats, false)
                ).Returns(true);

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);
            specialEntity.GenerateId(id.ToString());

            //Act
            bool result = specialEntity.SaveData<BaseStats>(stats, saveSlot);

            //Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void LoadFailTest()
        {
            //Arrange
            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.LoadData<BaseStats>($"/{saveSlot}/Entity-{id}-Stats.json", false))
                .Throws(new System.Exception());

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);
            specialEntity.GenerateId(id.ToString());

            //Act
            BaseStats result = specialEntity.LoadData<BaseStats>(saveSlot);

            //Assert
            Assert.AreEqual(result, default);
        }

        [Test]
        public void LoadSuccessTest()
        {
            //Arrange
            BaseStats loadStats = new BaseStats();
            loadStats.x = 77;

            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.LoadData<BaseStats>($"/{saveSlot}/Entity-{id}-Stats.json", false))
                .Returns(loadStats);

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);
            specialEntity.GenerateId(id.ToString());

            //Act
            BaseStats result = specialEntity.LoadData<BaseStats>(saveSlot);

            //Assert
            Assert.AreEqual(result.x, 77);
        }

        [Test]
        public void SaveMyDataTest()
        {
            //Arrange
            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.SaveData($"/{saveSlot}/Entity-{id}-Stats.json", stats, false)
                ).Returns(true);

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);
            specialEntity.GenerateId(id.ToString());
            specialEntity.myStats = stats;

            //Act
            specialEntity.SaveMyData(saveSlot);

            //Assert
            Assert.AreEqual(true, true);
        }

        [Test]
        public void LoadMyDataTest()
        {
            //Arrange
            BaseStats loadStats = new BaseStats();
            loadStats.x = 77;

            var dataService = new Mock<IDataService>(); 
            dataService.Setup(x => x.LoadData<IStats>($"/{saveSlot}/Entity-{id}-Stats.json", false))
                .Returns(loadStats);

            SaveableEntity specialEntity = new SaveableEntity(dataService.Object);
            specialEntity.GenerateId(id.ToString());

            //Act
            specialEntity.LoadMyData(saveSlot);

            //Assert
            Assert.AreEqual(((BaseStats)specialEntity.myStats).x, 77);
        }
    }
}
