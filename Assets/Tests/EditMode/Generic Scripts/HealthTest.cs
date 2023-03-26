using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests {
    public class HealthTest
    {
        private const int DEFAULT_HP = 10;
        private Health health;
        private BaseNPC npc = new BaseNPC();

        [SetUp]
        public void Setup()
        {
            health = new Health(npc);
        }

        [Test]
        public void GetCurrentHealthTest()
        {
            //Arrange
            NPCStats stats = npc.GetNPCStats();
            stats.currentHealth = DEFAULT_HP;

            //Act
            var result = health.GetCurrentHealth();

            //Assert
            Assert.AreEqual(result, DEFAULT_HP);
        }

        [Test]
        public void SetUpHealthTest()
        {
            //Arrange

            //Act
            health.SetHealth(DEFAULT_HP);

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), DEFAULT_HP);
        }

        [Test]
        public void SetHealthTest()
        {
            //Arrange
            health.SetUpHealth(DEFAULT_HP);

            //Act
            health.SetHealth(DEFAULT_HP-1);

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), DEFAULT_HP-1);
        }

        [Test]
        public void DamageTest()
        {
            //Arrange
            health.SetUpHealth(DEFAULT_HP);

            //Act
            health.Damage(DEFAULT_HP+1);

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), 0);
        }

        [Test]
        public void HealTest()
        {
            //Arrange
            health.SetUpHealth(DEFAULT_HP);
            health.SetHealth(DEFAULT_HP-1);

            //Act
            health.Heal(2);

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), DEFAULT_HP);
        }

        [Test]
        public void KillTest()
        {
            //Arrange
            health.SetUpHealth(DEFAULT_HP);

            //Act
            health.Kill();

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), 0);
        }

        [Test]
        public void FullHealTest()
        {
            //Arrange
            health.SetUpHealth(DEFAULT_HP);
            health.Damage(DEFAULT_HP);

            //Act
            health.FullHeal();

            //Assert
            Assert.AreEqual(health.GetCurrentHealth(), DEFAULT_HP);
        }
    }
}