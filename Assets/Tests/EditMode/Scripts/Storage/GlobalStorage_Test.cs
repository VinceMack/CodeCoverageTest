using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test
{
    public class GlobalStorage_Test
    {

        [Test]
        public void GlobalStorage_AddChest_NewChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage.AddChest(chest, location);

            Assert.AreEqual(1, GlobalStorage.chests.Count);
        }

        [Test]
        public void GlobalStorage_AddChest_ExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(0, 0, 0);
            Vector3 newLocation = new Vector3(1, 1, 1);

            GlobalStorage.AddChest(chest, location);
            GlobalStorage.AddChest(chest, newLocation);

            Assert.AreEqual(GlobalStorage.chests[chest], newLocation);
        }

        [Test]
        public void GlobalStorage_RemoveChest_ExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage.AddChest(chest, location);
            GlobalStorage.RemoveChest(chest);

            Assert.AreEqual(0, GlobalStorage.chests.Count);
        }

        [Test]
        public void GlobalStorage_RemoveChest_NonExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();

            GlobalStorage.RemoveChest(chest);

            Assert.AreEqual(0, GlobalStorage.chests.Count);
        }

        [Test]
        public void GlobalStorage_GetClosestChest_NoChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            if (GlobalStorage.GetClosestChest(new Vector3(0, 0, 0)) != null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GlobalStorage_GetClosestChest_ExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage.AddChest(chest, location);

            Assert.AreEqual(chest, GlobalStorage.GetClosestChest(location));
        }

        [Test]
        public void GlobalStorage_GetClosestChest_MultipleExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(5, 5, 5);

            GameObject game2 = new GameObject();
            game2.AddComponent<Chest>();
            var chest2 = game2.GetComponent<Chest>();
            Vector3 location2 = new Vector3(1, 1, 1);

            GlobalStorage.AddChest(chest, location);
            GlobalStorage.AddChest(chest2, location2);

            Assert.AreEqual(chest2, GlobalStorage.GetClosestChest(new Vector3(0.5f, 0.5f, 0.5f)));
        }

        [Test]
        public void GlobalStorage_GetItemCount_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            Chest chest = game.GetComponent<Chest>();
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

            chest.AddItem(item.itemName);
            GlobalStorage.chests.Add(chest, new Vector3(0, 0, 0));

            Assert.AreEqual(1, GlobalStorage.GetItemCount("testItem"));
        }


        [Test]
        public void GlobalStorage_GetChestsWithItem_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            game.AddComponent<Item>();
            Chest chest = game.GetComponent<Chest>();
            Item item = game.GetComponent<Item>();
            item.itemName = "testItem";
            GameObject objItemObj = new GameObject();
            Item objItem = objItemObj.AddComponent<Item>();
            objItem.itemName = "testItem";

            chest.AddItem(objItem.itemName);
            GlobalStorage.chests.Add(chest, new Vector3(0, 0, 0));
            List<Chest> listOfChests = GlobalStorage.GetChestsWithItem(item.itemName);

            Assert.AreEqual(chest, listOfChests[0]);
        }

        [Test]
        public void GlobalStorage_GetRandomChest_ExistingChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            Chest chest = game.GetComponent<Chest>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage.AddChest(chest, location);

            Assert.AreEqual(chest, GlobalStorage.GetRandomChest());
        }

        [Test]
        public void GlobalStorage_GetRandomChest_NoChest_Pass()
        {
            GlobalStorage.chests = new Dictionary<Chest, Vector3>();

            if (GlobalStorage.GetRandomChest() != null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }
    }
}
