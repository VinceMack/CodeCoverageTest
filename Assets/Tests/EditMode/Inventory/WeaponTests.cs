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
            weapon = new Weapon();
            
            //Act

            //Assert
            Assert.AreEqual(weapon.Damage, 2);
        }
    }
}