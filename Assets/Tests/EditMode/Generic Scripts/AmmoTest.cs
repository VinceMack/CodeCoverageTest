using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests {
    public class AmmoTest
    {
        private const int DEFAULT_AMMO = 10;
        private Ammo ammo;

        [SetUp]
        public void Setup()
        {
            //Arrange
            ammo = new Ammo();
            ammo.SetMaxAmmo(DEFAULT_AMMO);
            ammo.FillAmmo();
        }

        [Test]
        public void CanUseAmmoTest()
        {
            //Arrange

            //Act
            bool result = ammo.CanUseAmmo(2);

            //Assert
            Assert.AreEqual(result, true);
        }

        [Test]
        public void CannotUseAmmoTest()
        {
            //Arrange

            //Act
            bool result = ammo.CanUseAmmo(12);

            //Assert
            Assert.AreEqual(result, false);
        }

        [Test]
        public void UseAmmoTest()
        {
            //Arrange

            //Act
            ammo.UseAmmo(DEFAULT_AMMO - 1);

            //Assert
            Assert.AreEqual(ammo.CanUseAmmo(1), true);
            Assert.AreEqual(ammo.CanUseAmmo(2), false);
        }

        [Test]
        public void UseAllAmmo()
        {
            //Arrange

            //Act
            ammo.UseAllAmmo();

            //Assert
            Assert.AreEqual(ammo.CanUseAmmo(1), false);
        }

        [Test]
        public void AddAmmo()
        {
            //Arrange
            ammo.UseAmmo(DEFAULT_AMMO+1);

            //Act
            ammo.AddAmmo(DEFAULT_AMMO+1);

            //Assert
            Assert.AreEqual(ammo.CanUseAmmo(DEFAULT_AMMO), true);
        }
    }
}
