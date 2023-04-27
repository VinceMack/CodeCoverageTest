using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test
{
    public class Chest_Test
    {
        [Test]
        public void Chest_AddItem_NewItem_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);

            Assert.AreEqual(1, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_AddItem_ExistingItem_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);
            chest.AddItem(item.itemName);

            Assert.AreEqual(2, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_RemoveItem_QuantityGT0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);
            chest.AddItem(item.itemName);
            chest.RemoveItem(item.itemName);

            Assert.AreEqual(1, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_RemoveItem_QuantityEQ0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);
            chest.RemoveItem(item.itemName);

            bool containsKey = chest.contents.ContainsKey("testItem");
            Assert.IsFalse(containsKey);
        }

        [Test]
        public void Chest_GetItemQuantity_QuantityGT0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);
            chest.AddItem(item.itemName);

            int quantity = chest.GetItemQuantity(item.itemName);

            Assert.AreEqual(2, quantity);
        }

        [Test]
        public void Chest_GetItemQuantity_QuantityEQ0_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            int quantity = chest.GetItemQuantity(item.itemName);

            Assert.AreEqual(0, quantity);
        }

        [Test]
        public void Chest_ContainsItem_DoesContain_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();
            Item item = new Item();
            item.name = "testItem";

            chest.AddItem(item.itemName);

            bool containsItem = chest.ContainsItem(item.itemName);

            Assert.IsTrue(containsItem);
        }

        [Test]
        public void Chest_ContainsItem_DoesNotContain_Pass()
        {
            GameObject game = new GameObject();
            Item item = new Item();
            game.AddComponent<Chest>();
            var chest = game.GetComponent<Chest>();

            item.name = "testItem";

            bool containsItem = chest.ContainsItem(item.itemName);

            Assert.IsFalse(containsItem);
        }

        [Test]
        public void Chest_Deconstruct_Empty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();

            game.GetComponent<Chest>().Deconstruct();

            Assert.Pass();
        }

        [Test]
        public void Chest_Deconstruct_NotEmpty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            game.GetComponent<Chest>().contents.Add("testItem", 1);

            game.GetComponent<Chest>().Deconstruct();

            Assert.Pass();
        }

        [Test]
        public void Chest_ItemCountInChest_Empty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();

            int count = game.GetComponent<Chest>().ItemCountInChest("testItem");

            Assert.AreEqual(0, count);
        }

        [Test]
        public void Chest_ItemCountInChest_NotEmpty_Pass()
        {
            GameObject game = new GameObject();
            game.AddComponent<Chest>();
            game.GetComponent<Chest>().contents.Add("testItem", 1);

            int count = game.GetComponent<Chest>().ItemCountInChest("testItem");

            Assert.AreEqual(1, count);
        }

        // [Test]
        // public void Chest_Awake_Pass()
        // {
        //     GameObject game = new GameObject();
        //     game.AddComponent<Chest>();
        //     game.SetActive(true);

        //     var chest = game.GetComponent<Chest>();

        //     Assert.Pass();
        // }

        // [Test]
        // public void Chest_ResetPosition_Pass()
        // {
        //     GameObject game = new GameObject();
        //     game.AddComponent<GameManager>();
        //     game.AddComponent<Chest>();

        //     game.GetComponent<Chest>().ResetPosition();
        //     Assert.Pass();
        // }
    }
}


