using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class GridManagerTests
{
    private GameObject gridManagerObject;
    private GridManager gridManager;
    private GameObject gridObject;

    [SetUp]
    public void SetUp()
    {
        gridManagerObject = new GameObject("GridManager");
        gridManager = gridManagerObject.AddComponent<GridManager>();

        gridObject = new GameObject("Grid");
        gridObject.AddComponent<Grid>();
        gridObject.AddComponent<Tilemap>();
        gridObject.transform.SetParent(gridManagerObject.transform);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gridManagerObject);
    }

    [Test]
    public void GetTile_ReturnsTileAtPosition()
    {
        gridManager.InitializeGrid();
        gridManager.GenerateTileMap();

        Vector3Int position = new Vector3Int(5, 5, 0);
        BaseTile_VM tile = GridManager.GetTile(position);

        Assert.IsNotNull(tile);
    }

    [Test]
    public void GridSize_IsCorrect()
    {
        Assert.AreEqual(25, GridManager.MAX_HORIZONTAL);
        Assert.AreEqual(0, GridManager.MIN_HORIZONTAL);
        Assert.AreEqual(25, GridManager.MAX_VERTICAL);
        Assert.AreEqual(0, GridManager.MIN_VERTICAL);
    }

    [Test]
    public void Awake_InitializesGridAndTilemapComponents()
    {
        gridManager.InitializeGrid();

        Assert.IsNotNull(GridManager.grid);
        Assert.IsNotNull(GridManager.tileMap);
    }

    [Test]
    public void SetTileMap_GeneratesCorrectNumberOfTiles()
    {
        gridManager.InitializeGrid();
        gridManager.GenerateTileMap();

        int tileCount = 0;

        for (int x = GridManager.MIN_HORIZONTAL; x < GridManager.MAX_HORIZONTAL; x++)
        {
            for (int y = GridManager.MIN_VERTICAL; y < GridManager.MAX_VERTICAL; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);
                BaseTile_VM tile = GridManager.GetTile(position);
                if (tile != null)
                {
                    tileCount++;
                }
            }
        }

        Assert.AreEqual((GridManager.MAX_HORIZONTAL - GridManager.MIN_HORIZONTAL) * (GridManager.MAX_VERTICAL - GridManager.MIN_VERTICAL), tileCount);
    }
}