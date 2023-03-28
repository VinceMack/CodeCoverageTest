using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests
{
    public class StorageTests
    {
        private Storage storage;

        [Test]
        public void addWorkerTest()
        {
            storage = new Storage();
            Pawn pawn = new Pawn();
            int result = storage.addWorker(pawn);
            Assert.AreEqual(result, 1);
        }

        //TODO: removeWorkerTest()

        [Test]
        public void changeZoneTaskTest()
        {
            storage = new Storage();
            LaborOrder laborOrder = new LaborOrder();
            storage.changeZoneTask(laborOrder);
            Assert.AreEqual(storage.zoneAssignedOrder, laborOrder);
        }

        [Test]
        public void changeFilterTest()
        {
            storage = new Storage();
            Item item = new Item();

            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });
            Assert.AreEqual(storage.filter[0], item);
        }

        [Test]
        public void addStorageItemTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            int result = storage.addItem(item);
            Assert.AreEqual(result, 100);
        }

        [Test]
        public void incrementItemTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            storage.addItem(item);
            int result = storage.incrementItem(item.Name, 10);
            Assert.AreEqual(result, 110);
        }

        [Test]
        public void decrementItemTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            storage.addItem(item);
            int result = storage.decrementItem(item.Name, 10);
            Assert.AreEqual(result, 90);
        }

        [Test]
        public void isValidItemFailTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            Item item2 = new Item();
            item2.Name = "Test Item 2";

            bool result = storage.isValidItem(item2);
            Assert.AreEqual(result, false);
        }

        [Test]
        public void isValidItemPassTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            bool result = storage.isValidItem(item);
            Assert.AreEqual(result, true);
        }

        [Test]
        public void findFilterItemTest()
        {
            storage = new Storage();
            Item item = new Item();
            item.Name = "Test Item";
            item.Quantity = 100;
            storage.changeFilter(new System.Collections.Generic.List<Item>() { item });

            int result = storage.findFilterItem(item);
            Assert.AreEqual(result, 0);
        }
    }
}