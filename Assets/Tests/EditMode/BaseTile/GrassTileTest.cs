using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GrassTileTest
{
    GrassTile grassTile;
    int x, y;
    TileType tileType;
    bool collision;

    [SetUp]
    public void Setup()
    {
        x = 2; y = 3;
        tileType = TileType.GRASS;
        collision = false;

        grassTile = ScriptableObject.CreateInstance<GrassTile>();
        Assert.IsNotNull(grassTile);
        grassTile.InitializeTileData(x, y, tileType, collision);
    }

    [Test]
    public void GrassCollisionTest()
    {
        Assert.IsFalse(grassTile.Collision());
    }

}
