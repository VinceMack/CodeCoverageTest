using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test
{
	public class Chest_VM_Test
    {
        [Test]
        public void Chest_VM_AddItem_NewItem_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);

            Assert.AreEqual(1, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_VM_AddItem_ExistingItem_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);
            chest.AddItem(item);

            Assert.AreEqual(2, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_VM_RemoveItem_QuantityGT0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);
            chest.AddItem(item);
            chest.RemoveItem(item);

            Assert.AreEqual(1, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_VM_RemoveItem_QuantityEQ0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);
            chest.RemoveItem(item);

            bool containsKey = chest.contents.ContainsKey("testItem");
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Chest_VM_GetItemQuantity_QuantityGT0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);
            chest.AddItem(item);

            int quantity = chest.GetItemQuantity(item);

            Assert.AreEqual(2, quantity);
        }

        [Test]
        public void Chest_VM_GetItemQuantity_QuantityEQ0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            int quantity = chest.GetItemQuantity(item);

            Assert.AreEqual(0, quantity);
        }

        [Test]
        public void Chest_VM_ContainsItem_DoesContain_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();
            GameObject item = new GameObject();
            item.name = "testItem";

            chest.AddItem(item);

            bool containsItem = chest.ContainsItem(item);

            Assert.IsTrue(containsItem);
        }

        [Test]
        public void Chest_VM_ContainsItem_DoesNotContain_Pass()
        {
            GameObject game = new GameObject();
            GameObject item = new GameObject();
            game.AddComponent<Chest_VM>();
            var chest = game.GetComponent<Chest_VM>();

            item.name = "testItem";

            bool containsItem = chest.ContainsItem(item);

            Assert.IsFalse(containsItem);
        }

        [Test]
        public void Chest_VM_Deconstruct_Empty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();

            game.GetComponent<Chest_VM>().Deconstruct();

            Assert.Pass();
        }

        [Test]
        public void Chest_VM_Deconstruct_NotEmpty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            game.GetComponent<Chest_VM>().contents.Add("testItem", 1);

            game.GetComponent<Chest_VM>().Deconstruct();

            Assert.Pass();
        }

        [Test]
        public void Chest_VM_ItemCountInChest_Empty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();

            int count = game.GetComponent<Chest_VM>().ItemCountInChest("testItem");

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Chest_VM_ItemCountInChest_NotEmpty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest_VM>();
            game.GetComponent<Chest_VM>().contents.Add("testItem", 1);

            int count = game.GetComponent<Chest_VM>().ItemCountInChest("testItem");

            Assert.AreEqual(1, count);
        }

        // [Test]
        // public void Chest_VM_Awake_Pass()
        // {
        //     GameObject game = new GameObject();
        //     game.AddComponent<Chest_VM>();
        //     game.SetActive(true);

        //     var chest = game.GetComponent<Chest_VM>();

        //     Assert.Pass();
        // }

        // [Test]
        // public void Chest_VM_ResetPosition_Pass()
        // {
        //     GameObject game = new GameObject();
        //     game.AddComponent<GameManager>();
        //     game.AddComponent<Chest_VM>();

        //     game.GetComponent<Chest_VM>().ResetPosition();
        //     Assert.Pass();
        // }
    }
}