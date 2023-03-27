using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class RockTileTest
{
    RockTile rockTile;
    int x, y, resources;
    TileType tileType;
    bool collision;

    [SetUp]
    public void Setup()
    {
        x = 2; y = 3; resources = 10;
        tileType = TileType.ROCK;
        collision = true;

        rockTile = ScriptableObject.CreateInstance<RockTile>();
        Assert.IsNotNull(rockTile);
        rockTile.InitializeTileData(x, y, tileType, collision);
        rockTile.setResources(resources);
    }

    [Test]
    public void RockTileGetResourcesTest()
    {
        Assert.AreEqual(rockTile.getResources(), resources);
    }

    [Test]
    public void RockTileSetResourcesTest()
    {
        int updatedResources = 20;
        rockTile.setResources(updatedResources);

        Assert.AreEqual(rockTile.getResources(), updatedResources);
    }
}
