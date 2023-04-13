using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests
{
    public class GlobalStorageTests
    {
        private GlobalStorage globalStorage;

        [Test]
        public void addItemTest()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);
            
            Item item = ItemList.itemList["wood"];
            int result = globalStorage.AddItem(item, chest);

            Assert.AreEqual(result, 1);
        }

        [Test]
        public void addExistingItemTest()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);
            Chest secondChest = new Chest(globalStorage);
            Item item = ItemList.itemList["wood"];

            globalStorage.AddItem(item, chest);
            int result = globalStorage.AddItem(item, secondChest);

            Assert.AreEqual(result, 2);
        }

        [Test]
        public void getChestWithItemTest()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);

            Item item = ItemList.itemList["wood"];
            globalStorage.AddItem(item, chest);

            var result = globalStorage.GetChestWithItem(item);

            Assert.AreNotEqual(result.Count, 0);
        }

        [Test]
        public void getChestWithItemNullReturnTest()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);

            var result = globalStorage.GetChestWithItem(ItemList.itemList["bread"]);

            Assert.AreEqual(result, null);
        }

        [Test]
        public void getClosestChestWithItem()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);
            chest.coordinate = new Vector3(0, 0, 0);

            Item item = ItemList.itemList["wood"];
            globalStorage.AddItem(item, chest);

            Chest result = globalStorage.GetClosestChestWithItem(item, new Vector3(0, 0, 0));

            Assert.AreNotEqual(result, Mathf.Infinity);
        }

        [Test]
        public void DeleteItemTest()
        {
            globalStorage = new GlobalStorage();
            Chest chest = new Chest(globalStorage);

            Item item = ItemList.itemList["wood"];
            item.Quantity = 10;

            globalStorage.AddItem(item, chest);
            
            int result = globalStorage.DeleteItem(item, chest);

            Assert.AreNotEqual(result, -1);
        }
    }
}