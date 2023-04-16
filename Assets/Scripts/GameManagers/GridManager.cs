using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// GridManager class to manage and generate grid of tiles in a scene
public class GridManager : MonoBehaviour
{
    // Static references to the Grid and Tilemap components
    public static Grid grid;
    public static Tilemap tileMap;

    // Constants for the size of a level
    public static List<Level> mapLevels;
    public static readonly int LEVEL_WIDTH = 10;
    public static readonly int LEVEL_HEIGHT = 5;


    // Method to reset grid values for pathfinding
    public static void ResetGrid(int levelNumber)
    {
        // Reset only the level used for pathfinding
        Level level = mapLevels[levelNumber];
        for(int x = level.getXMin(); x < level.getXMax(); x++)
	    {
            for(int y = level.getYMin(); y < level.getYMax(); y++)
            {
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));
                if(tile == null)
                {
                    Debug.LogError("Tile is null @ GridManager.ResetGrid()");
                    break;
		        }

                tile.visited = false;
                tile.distance = -1;
                tile.parent = null;
                tile = null;
	        }
	    }
    }

    // Method to Get the tile at a specific position in the grid
    public static BaseTile_VM GetTile(Vector3Int position)
    {
        return (BaseTile_VM)tileMap.GetTile(position);
    }

    // Method to create and add a level to the grid
    public static void CreateLevel()
    {
        int xMin, xMax, yMin, yMax;
        if(mapLevels.Count == 0)
	    {
            xMin = 0; xMax = LEVEL_WIDTH - 1; yMin = 0; yMax = LEVEL_HEIGHT;
	    }
        else
	    {
            xMin = mapLevels[mapLevels.Count - 1].getXMax() + 1;
            xMax = xMin + LEVEL_WIDTH - 1;
            yMin = 0;
            yMax = LEVEL_HEIGHT;
	    }

        // Add new level to map levels list
        mapLevels.Add(new Level(mapLevels.Count, xMin, xMax, yMin, yMax));

        // Set tiles for level
        for(int x = xMin; x < xMax; x++)
        { 
            for(int y = yMin; y < yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // Generate a random tile based on the random value and set its properties
                int random = Random.Range(1, 5);
                if(random == 4)
                {
                    random = Random.Range(3, 5); // Reduce the chance of rock, replace decreased chances with chance to increase water or sand
		        }
                switch (random)
                {
                    case 0:
                        BaseTile_VM newBaseTile_VM = ScriptableObject.CreateInstance<BaseTile_VM>();
                        tileMap.SetTile(position, newBaseTile_VM);
                        newBaseTile_VM.SetTileData(TileType.GENERIC, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                        break;
                    case 1:
                        GrassTile_VM newGrassTile_VM = ScriptableObject.CreateInstance<GrassTile_VM>();
                        tileMap.SetTile(position, newGrassTile_VM);
                        newGrassTile_VM.SetTileData(TileType.GRASS, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                        break;
                    case 2:
                        WaterTile_VM newWaterTile_VM = ScriptableObject.CreateInstance<WaterTile_VM>();
                        tileMap.SetTile(position, newWaterTile_VM);
                        newWaterTile_VM.SetTileData(TileType.WATER, false, null, 0, tileMap.GetCellCenterWorld(position), 0, false, null);
                        break;
                    case 3:
                        SandTile_VM newSandTile_VM = ScriptableObject.CreateInstance<SandTile_VM>();
                        tileMap.SetTile(position, newSandTile_VM);
                        newSandTile_VM.SetTileData(TileType.SAND, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                        break;
                    case 4:
                        RockTile_VM newRockTile_VM = ScriptableObject.CreateInstance<RockTile_VM>();
                        tileMap.SetTile(position, newRockTile_VM);
                        newRockTile_VM.SetTileData(TileType.ROCK, true, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null);
                        break;
                }
            }
	    }
        
        // Add stairs to upper and lower levels
        if (mapLevels.Count > 1)
        {
            int randomX = UnityEngine.Random.Range(xMin, xMax);
            int randomY = UnityEngine.Random.Range(yMin, yMax);

            // Set stairs in random location in upper level
            Vector3Int upperLevelStairsPosition = new Vector3Int(randomX - LEVEL_WIDTH, randomY, 0);
            StairsTile_VM upperLevelStairs = ScriptableObject.CreateInstance<StairsTile_VM>();
            tileMap.SetTile(upperLevelStairsPosition, upperLevelStairs);
            upperLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(upperLevelStairsPosition), -9, false, null);
            mapLevels[mapLevels.Count - 2].AddDescendingStairs_VM(upperLevelStairs);

            // Set stairs in lower level
            Vector3Int lowerLevelStairsPosition = new Vector3Int(randomX, randomY, 0);
            StairsTile_VM lowerLevelStairs = ScriptableObject.CreateInstance<StairsTile_VM>();
            tileMap.SetTile(lowerLevelStairsPosition, lowerLevelStairs);
            lowerLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(lowerLevelStairsPosition), -9, false, null);
            mapLevels[mapLevels.Count - 1].AddAscendingStairs_VM(lowerLevelStairs);

            // Connect upper and lower level stairs
            upperLevelStairs.setLowerLevelStairs(lowerLevelStairs);
            lowerLevelStairs.setUpperLevelStairs(upperLevelStairs);
        }
    }

    // Method to initialize the GridManager
    public static void InitializeGridManager()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tileMap = GameObject.Find("Grid").GetComponent<Tilemap>();

        mapLevels = new List<Level>();
    }

    // Spawns trees on random vacant grass tiles
    // requires GlobalInstance2 (TMPCombined) in scene
    public static void PopulateWithTrees()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach(BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject tree = GlobalInstance.Instance.entityDictionary.InstantiateEntity("tree", "", tile.position);
                tile.SetTileInformation(tile.type, false, tree, tile.resourceCount, tile.position);
            }
        }
    }

    // Spawns bushes on random vacant grass tiles
    // requires GlobalInstance2 (TMPCombined) in scene
    public static void PopulateWithBushes()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject bush = GlobalInstance.Instance.entityDictionary.InstantiateEntity("bush", "", tile.position);
                tile.SetTileInformation(tile.type, false, bush, tile.resourceCount, tile.position);
            }
        }
    }

    // Spawns bushes on random vacant sand tiles
    // requires GlobalInstance2 (TMPCombined) in scene
    //      Only intended for testing. Farms will be responsible for creating wheat.
    public static void PopulateWithWheat()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile_VM tile in allTiles)
        {
            if (tile != null && tile.type == TileType.SAND && tile.resource == null && Random.Range(0, 10) == 0)
            {
                GameObject wheat = GlobalInstance.Instance.entityDictionary.InstantiateEntity("wheat", "", tile.position);
                tile.SetTileInformation(tile.type, false, wheat, tile.resourceCount, tile.position);
            }
        }
    }
}
