using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BaseTileTest
{
    BaseTile baseTile;
    int x, y;
    TileType tileType;
    bool collision;

    [SetUp]
    public void Setup()
    {
        x = 2; y = 3;
        tileType = TileType.GENERIC;
        collision = false;

        baseTile = ScriptableObject.CreateInstance<BaseTile>();
        Assert.IsNotNull(baseTile);
        baseTile.InitializeTileData(x, y, tileType, collision);
    }

    [Test]
    public void BaseTileInitializedDistanceTest()
    {
        Assert.AreEqual(baseTile.distance, -1);
    }

    [Test]
    public void BaseTileInitializedVisitedTest()
    {
        Assert.AreEqual(baseTile.visited, false);
    }

    [Test]
    public void BaseTileInitializedParentTest()
    {
        Assert.AreEqual(baseTile.parent, null);
    }

    [Test]
    public void BaseTileInitializedLocation_X_Test()
    {
        Assert.AreEqual(baseTile.x, x);
    }

    [Test]
    public void BaseTileInitializedLocation_Y_Test()
    {
        Assert.AreEqual(baseTile.y, y);
    }

    [Test]
    public void BaseTileInitializedTileTypeTest()
    {
        Assert.AreEqual(baseTile.getTileType(), tileType);
    }

    [Test]
    public void BaseTileSetTileTypeTest()
    {
        TileType tileType = TileType.GRASS;
        baseTile.setTileType(tileType);

        Assert.AreEqual(baseTile.getTileType(), tileType);
    }

    [Test]
    public void BaseTileGetCollisionTest()
    {
        Assert.AreEqual(baseTile.Collision(), collision);
    }

    [Test]
    public void BaseTileSetCollisionTest()
    {
        bool collisionUpdate = true;
        baseTile.setCollision(collisionUpdate);

        Assert.AreEqual(baseTile.Collision(), collisionUpdate);
    }

}
