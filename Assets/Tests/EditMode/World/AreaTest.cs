using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class AreaTest
    {
        Area testArea;

        [SetUp]
        public void Setup()
        {
            // Arrange
            testArea = new Area(new Vector2(1f, 1f), new Vector2(-1f, -1f));
        }

        [Test]
        public void GetCornersTest()
        {
            // Arrange
            // Act
            List<Vector2> corners = testArea.GetCorners();

            // Assert
            Assert.AreEqual(4, corners.Count);
        }

        [Test]
        public void GetTwoCornersTest()
        {
            // Arrange
            // Act
            List<Vector2> corners = testArea.GetTwoCorners();

            // Assert
            Assert.AreEqual(2, corners.Count);
        }

        [Test]
        public void GetCenterTest()
        {
            // Arrange
            // Act
            Vector2 center = testArea.GetCenter();

            // Assert
            Assert.AreEqual(0, center.x);
            Assert.AreEqual(0, center.y);
        }
    }
}
