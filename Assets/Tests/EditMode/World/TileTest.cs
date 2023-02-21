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
            Tile testTile = new Tile();
            testTile.AddObjectToTile(new PlacedObject());
            Assert.AreEqual(1, testTile.GetObjectsOnTile().Count);
        }
    }
}
