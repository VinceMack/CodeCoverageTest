using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test 
{
    public class GlobalStorage_VM_Test
    {

        [Test]
        public void GlobalStorage_VM_AddChest_NewChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage_VM.AddChest(chest, location);

            Assert.AreEqual(1, GlobalStorage_VM.chests.Count);
        }

        [Test]
        public void GlobalStorage_VM_AddChest_ExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(0, 0, 0);
            Vector3 newLocation = new Vector3(1, 1, 1);

            GlobalStorage_VM.AddChest(chest, location);
            GlobalStorage_VM.AddChest(chest, newLocation);

            Assert.AreEqual(GlobalStorage_VM.chests[chest], newLocation);
        }

        [Test]
        public void GlobalStorage_VM_RemoveChest_ExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage_VM.AddChest(chest, location);
            GlobalStorage_VM.RemoveChest(chest);

            Assert.AreEqual(0, GlobalStorage_VM.chests.Count);
        }

        [Test]
        public void GlobalStorage_VM_RemoveChest_NonExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();

            GlobalStorage_VM.RemoveChest(chest);

            Assert.AreEqual(0, GlobalStorage_VM.chests.Count);
        }

        [Test]
        public void GlobalStorage_VM_AddItem_NewItem_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            GlobalStorage_VM.AddItem(item, chest);

            Assert.AreEqual(chest, GlobalStorage_VM.itemLocations[item]);
        }

        [Test]
        public void GlobalStorage_VM_AddItem_ExistingItem_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            chest.name = "testChest";
            var newChest = game.AddComponent<Chest_VM>();
            newChest.name = "newChest";
            GameObject item = new GameObject();
            item.name = "testItem";

            GlobalStorage_VM.AddItem(item, chest);
            GlobalStorage_VM.AddItem(item, chest);

            Assert.AreNotEqual(newChest, GlobalStorage_VM.itemLocations[item]);
        }

        [Test]
        public void GlobalStorage_VM_GetClosestChest_NoChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            if (GlobalStorage_VM.GetClosestChest(new Vector3(0, 0, 0)) != null)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }
        }

        [Test]
        public void GlobalStorage_VM_GetClosestChest_ExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage_VM.AddChest(chest, location);

            Assert.AreEqual(chest, GlobalStorage_VM.GetClosestChest(location));
        }

        [Test]
        public void GlobalStorage_VM_GetClosestChest_MultipleExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();

            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(5, 5, 5);

            GameObject game2 = new GameObject();
            game2.AddComponent<Chest_VM>();
            var chest2 = game2.GetComponent<Chest_VM>();
            Vector3 location2 = new Vector3(1, 1, 1);

            GlobalStorage_VM.AddChest(chest, location);
            GlobalStorage_VM.AddChest(chest2, location2);

            Assert.AreEqual(chest2, GlobalStorage_VM.GetClosestChest(new Vector3(0.5f, 0.5f, 0.5f)));
        }

        [Test]
        public void GlobalStorage_VM_GetItemCount_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            Chest_VM chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);
            GlobalStorage_VM.chests.Add(chest, new Vector3(0, 0, 0));

            Assert.AreEqual(1, GlobalStorage_VM.GetItemCount("testItem"));
        }

        [Test]
        public void GlobalStorage_VM_GetChestWithItem_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            game.AddComponent<Item>();
            Chest_VM chest = game.GetComponent<Chest_VM>();
            Item item = game.GetComponent<Item>();
            item.itemName = "testItem";
            GameObject objItem = new GameObject();
            objItem.name = "testItem";

            chest.AddItem(objItem);
            GlobalStorage_VM.chests.Add(chest, new Vector3(0, 0, 0));
            List<Chest_VM> listOfChests = GlobalStorage_VM.GetChestWithItem(item);

            Assert.AreEqual(chest, listOfChests[0]);
        }

        [Test]
        public void GlobalStorage_VM_GetRandomChest_ExistingChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            Chest_VM chest = game.GetComponent<Chest_VM>();
            Vector3 location = new Vector3(0, 0, 0);

            GlobalStorage_VM.AddChest(chest, location);

            Assert.AreEqual(chest, GlobalStorage_VM.GetRandomChest());
        }

        [Test]
        public void GlobalStorage_VM_GetRandomChest_NoChest_Pass()
        {
            GlobalStorage_VM.chests = new Dictionary<Chest_VM, Vector3>();
            GlobalStorage_VM.itemLocations = new Dictionary<GameObject, Chest_VM>();

            if (GlobalStorage_VM.GetRandomChest() != null)
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