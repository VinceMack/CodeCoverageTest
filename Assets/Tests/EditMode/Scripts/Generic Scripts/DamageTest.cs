using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests {
    public class DamageTest
    {
        private const int DEFAULT_HP = 10;
        private Damage damage;

        [SetUp]
        public void Setup()
        {
            damage = new Damage();
        }

        [Test]
        public void NoDamageDealtTest()
        {
            var health = new NonGenericHealthImplementationForTest(DEFAULT_HP);

            damage.ApplyDamage(health, 0);
            Assert.AreEqual(health.GetHP(), DEFAULT_HP);
        }

        [Test]
        public void MaxDamageDealtTest()
        {
            var health = new NonGenericHealthImplementationForTest(DEFAULT_HP);

            damage.ApplyDamage(health, DEFAULT_HP);
            Assert.AreEqual(health.GetHP(), 0);
        }

        [Test]
        public void IterativeDamageDealtTest()
        {
            for (int i = 0; i < DEFAULT_HP - 1; i++)
            {
                var health = new NonGenericHealthImplementationForTest(DEFAULT_HP);

                damage.ApplyDamage(health, i + 1);
                Assert.AreEqual(health.GetHP(), DEFAULT_HP - i - 1);
            }
        }
    }
}