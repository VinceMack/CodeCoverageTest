using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class TileTest
    {
        [Test]
        public void AddObjectToTileSucceeds()
        {
            GameObjectTile testTile = new GameObjectTile();
            testTile.AddObjectToTile(new PlacedObject());
            Assert.AreEqual(1, testTile.GetObjectsOnTile().Count);
        }
    }
}
