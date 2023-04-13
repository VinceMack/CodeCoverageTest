using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests
{
    public class WeaponTests
    {
        private Weapon weapon;

        [Test]
        public void testDamageAssignment()
        {
            //Arrange
            weapon = new Weapon("testWeapon", "Test Weapon", false, Rarity.Rare, 100, 1, "Dropped Test Weapon", Slot.WeaponLeft, 1, 2, 1);
            
            //Act

            //Assert
            Assert.AreEqual(weapon.Damage, 2);
        }
    }
}