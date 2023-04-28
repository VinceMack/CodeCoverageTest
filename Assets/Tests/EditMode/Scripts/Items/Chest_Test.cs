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
            Chest chest = game.AddComponent<Chest>();
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

            chest.AddItem(item.itemName);

            Assert.AreEqual(1, chest.contents["testItem"]);
        }

        [Test]
        public void Chest_AddItem_ExistingItem_Pass()
        {
            GameObject game = new GameObject();
            Chest chest = game.AddComponent<Chest>();
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

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
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

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
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

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
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

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
            GameObject itemObj = new GameObject();
            Item item = itemObj.AddComponent<Item>();
            item.itemName = "testItem";

            int quantity = chest.GetItemQuantity(item.itemName);

            Assert.AreEqual(0, quantity);
        }


        [Test]
        public void Chest_ContainsItem_DoesContain_Pass()
        {
            GameObject chest = new GameObject();
            chest.AddComponent<Chest>();
            GameObject item = new GameObject();
            item.AddComponent<Item>();

            chest.GetComponent<Chest>().AddItem(item.GetComponent<Item>().itemName);

            bool containsItem = chest.GetComponent<Chest>().ContainsItem(item.GetComponent<Item>().itemName);

            Assert.IsTrue(containsItem);
        }

        [Test]
        public void Chest_ContainsItem_DoesNotContain_Pass()
        {
            GameObject chest = new GameObject();
            chest.AddComponent<Chest>();
            GameObject item = new GameObject();
            item.AddComponent<Item>();

            bool containsItem = chest.GetComponent<Chest>().ContainsItem(item.GetComponent<Item>().itemName);

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


