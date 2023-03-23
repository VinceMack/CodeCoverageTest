using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;
using System.IO;
using UnityEngine;

namespace Tests 
{
    public class JsonDataServiceTest
    {
        private JsonDataService DataService;

        [SetUp]
        public void Setup()
        {
            //Arrange
            DataService = new JsonDataService();
        }

        [Test]
        public void SaveUnencryptedData()
        {
            //Arrange
            BaseStats data = new BaseStats();
            data.x = 44;
            data.guid = "test";

            //Act
            DataService.SaveData($"/test.json", data, false);

            //Assert
            Assert.AreEqual(DataService.LoadData<BaseStats>($"/test.json", false).x, 44);

            //Clean-up
            File.Delete(Application.persistentDataPath + "/test.json");
        }

        [Test]
        public void SaveEncryptedData()
        {
            //Arrange
            BaseStats data = new BaseStats();
            data.x = 444;
            data.guid = "test";

            //Act
            DataService.SaveData($"/test.json", data, true);

            //Assert
            Assert.AreEqual(DataService.LoadData<BaseStats>($"/test.json", true).x, 444);

            //Clean-up
            File.Delete(Application.persistentDataPath + "/test.json");
        }
    }
}
