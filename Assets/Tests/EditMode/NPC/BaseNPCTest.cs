using UnityEngine;
using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;
using Moq;
using TMPro;

namespace Tests {
    public class BaseNPCTest
    {
        [Test]
        public void GetNPCStatsTest()
        {
            // Arrange
            GameObject obj = MonoBehaviour.Instantiate(new GameObject());
            BaseNPC npc = obj.AddComponent<BaseNPC>();

            // Act
            NPCStats stats = npc.GetNPCStats();

            // Assert
            Assert.NotNull(stats);
        }
    }
}