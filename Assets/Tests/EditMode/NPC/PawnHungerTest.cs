using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PawnHungerTest
    {
        [Test]
        public void EatFromInventoryFoodFoundTest()
        {
            Pawn_VM pawn = new Pawn_VM();
            Item berryItem = new Item("Berries", "Berries", true, Rarity.Common, 1, 1, "", 10);
            pawn.items.Add("Berries", berryItem);
            pawn.hunger = 1;
            int startingHunger = pawn.hunger;

            bool found = PawnHunger.EatFromInventory(pawn);

            Assert.IsTrue(found);
            Assert.IsTrue(pawn.hunger > startingHunger);
            Assert.IsFalse(pawn.items.ContainsKey("Berries"));
        }

        [Test]
        public void EatFromInventoryFoodNotFoundTest()
        {
            Pawn_VM pawn = new Pawn_VM();
            pawn.hunger = 1;
            pawn.items.Clear();

            bool found = PawnHunger.EatFromInventory(pawn);

            Assert.IsFalse(found);
        }
    }
}
