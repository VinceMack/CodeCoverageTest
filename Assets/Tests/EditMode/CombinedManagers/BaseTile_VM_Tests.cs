using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

/*
public class BaseTile_VM_Tests
{
    [Test]
    public void GettersAndSetters_WorkCorrectly()
    {
        // Arrange
        BaseTile_VM tile = ScriptableObject.CreateInstance<BaseTile_VM>();
        TileType tileType = TileType.GRASS;
        bool collision = true;
        GameObject resource = new GameObject("TestResource");
        int resourceCount = 5;
        Vector3 position = new Vector3(1, 1, 0);

        // Act
        tile.SetType(tileType);
        tile.SetCollision(collision);
        tile.SetResource(resource);
        tile.SetResourceCount(resourceCount);
        tile.SetPosition(position);

        // Assert
        Assert.AreEqual(tileType, tile.GetTileType());
        Assert.AreEqual(collision, tile.GetCollision());
        Assert.AreEqual(resource, tile.GetResource());
        Assert.AreEqual(resourceCount, tile.GetResourceCount());
        Assert.AreEqual(position, tile.GetPosition());
    }

    [Test]
    public void GetTileType_ReturnsCorrectType()
    {
        // Arrange
        BaseTile_VM tile = ScriptableObject.CreateInstance<BaseTile_VM>();
        TileType tileType = TileType.ROCK;
        tile.SetType(tileType);

        // Act
        TileType result = tile.GetTileType();

        // Assert
        Assert.AreEqual(tileType, result);
    }

    [Test]
    public void ReturnTileInformation_WithResource_ReturnsCorrectString()
    {
        // Arrange
        BaseTile_VM tile = ScriptableObject.CreateInstance<BaseTile_VM>();
        TileType tileType = TileType.WATER;
        bool collision = false;
        GameObject resource = new GameObject("TestResource");
        int resourceCount = 3;
        Vector3 position = new Vector3(2, 2, 0);
        tile.SetTileInformation(tileType, collision, resource, resourceCount, position);

        // Act
        string result = tile.returnTileInformation();

        // Assert
        Assert.IsTrue(result.Contains("Tile Type: WATER"));
        Assert.IsTrue(result.Contains("Collision: False"));
        Assert.IsTrue(result.Contains("Resource: TestResource"));
        Assert.IsTrue(result.Contains("ResourceCount: 3"));
        Assert.IsTrue(result.Contains("Position: (2.00, 2.00, 0.00)"));
    }

    [Test]
    public void ReturnTileInformation_WithoutResource_ReturnsCorrectString()
    {
        // Arrange
        BaseTile_VM tile = ScriptableObject.CreateInstance<BaseTile_VM>();
        TileType tileType = TileType.GRASS;
        bool collision = true;
        Vector3 position = new Vector3(2, 2, 0);
        tile.SetTileInformation(tileType, collision, null, 0, position);

        // Act
        string result = tile.returnTileInformation();

        // Assert
        Assert.IsTrue(result.Contains("Tile Type: GRASS"));
        Assert.IsTrue(result.Contains("Collision: True"));
        Assert.IsFalse(result.Contains("Resource:"));
        Assert.IsFalse(result.Contains("ResourceCount"));
        Assert.IsTrue(result.Contains("Position: (2.00, 2.00, 0.00)"));
    }
}
*/