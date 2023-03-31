using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class WaterTileTest
{
    WaterTile waterTile;
    int x, y;
    TileType tileType;
    bool collision;

    [SetUp]
    public void Setup()
    {
        x = 2; y = 3;
        tileType = TileType.WATER;
        collision = true;

        waterTile = ScriptableObject.CreateInstance<WaterTile>();
        waterTile.InitializeTileData(x, y, tileType, collision);
    }

    [Test]
    public void WaterCollisionTest()
    {
        Assert.IsTrue(waterTile.Collision());
    }

    [Test]
    public void WaterTileExistenceTest()
    {
        Assert.IsNotNull(waterTile);
    }

}
