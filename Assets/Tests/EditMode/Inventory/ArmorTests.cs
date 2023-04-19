using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class ArmorTests
    {
        Armor myArmor;

        [SetUp]
        public void Setup()
        {
            // Arrange
            myArmor = new Armor("testArmor", "Test Armor", false, Rarity.Common, 20, 1, "Dropped Test Armor", Slot.Chest, 2, 3, 4);
        }

        [Test]
        public void TestDuplication()
        {
            // Arrange

            // Act
            Armor newArmor = new Armor(myArmor);

            // Assert
            Assert.AreEqual(myArmor.Coverage, newArmor.Coverage);
            Assert.AreEqual(myArmor.Defense, newArmor.Defense);
            Assert.AreEqual(myArmor.Warmth, newArmor.Warmth);
        }

        [Test]
        public void TestModification()
        {
            // Arrange
            int newWarmth = 6;
            Armor newArmor = new Armor();

            // Act
            newArmor.Warmth = newWarmth;

            // Assert
            Assert.AreEqual(newWarmth, newArmor.Warmth);
        }
    }
}
