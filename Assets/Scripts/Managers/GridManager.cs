using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// GridManager class to manage and generate grid of tiles in a scene
[ExecuteInEditMode]
public class GridManager : MonoBehaviour
{
    // Static references to the Grid and Tilemap components
    public static Grid grid;
    public static Tilemap tileMap;

    // Constants for the size of a level
    public static List<Level> mapLevels;
    public static readonly int LEVEL_WIDTH = 100;
    public static readonly int LEVEL_HEIGHT = 50;

    // Method to reset grid values for pathfinding
    public static void ResetGrid(int levelNumber)
    {
        // Reset only the level used for pathfinding
        Level level = mapLevels[levelNumber];
        for (int x = level.getXMin(); x < level.getXMax(); x++)
        {
            for (int y = level.getYMin(); y < level.getYMax(); y++)
            {
                BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));
                if (tile == null)
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
    public static BaseTile GetTile(Vector3Int position)
    {
        return (BaseTile)tileMap.GetTile(position);
    }

    // Method to create and add a level to the grid
    public static void CreateLevel()
    {
        int xMin, xMax, yMin, yMax;
        if (mapLevels.Count == 0)
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
        for (int x = xMin; x < xMax; x++)
        {
            for (int y = yMin; y < yMax; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                if (mapLevels.Count == 1) // Top layer
                {
                    Vector2 currentPos = new Vector2(x, y);
                    float sandRadius = Mathf.Min(xMax, yMax) * 0.4f;
                    float grassRadius = sandRadius * 0.8f;
                    float jaggednessScale = 0.1f;
                    Vector2 center = new Vector2(xMax / 2f, yMax / 2f);

                    float distanceFromCenter = Vector2.Distance(currentPos, center);
                    float noiseValue = Mathf.PerlinNoise(x * jaggednessScale, y * jaggednessScale);

                    // Water tiles on the outer perimeter
                    if (distanceFromCenter >= sandRadius + (sandRadius * 0.5f * noiseValue))
                    {
                        WaterTile newWaterTile = ScriptableObject.CreateInstance<WaterTile>();
                        tileMap.SetTile(position, newWaterTile);
                        newWaterTile.SetTileData(TileType.WATER, true, null, 0, tileMap.GetCellCenterWorld(position), 0, false, null, mapLevels.Count - 1);
                    }
                    // Sand tiles in jagged circular portion
                    else if (distanceFromCenter < sandRadius + (sandRadius * 0.5f * noiseValue) &&
                            distanceFromCenter >= grassRadius + (grassRadius * 0.5f * noiseValue))
                    {
                        SandTile newSandTile = ScriptableObject.CreateInstance<SandTile>();
                        tileMap.SetTile(position, newSandTile);
                        newSandTile.SetTileData(TileType.SAND, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null, mapLevels.Count - 1);
                    }
                    // Grass tiles in smaller jagged circular portion
                    else
                    {
                        GrassTile newGrassTile = ScriptableObject.CreateInstance<GrassTile>();
                        tileMap.SetTile(position, newGrassTile);
                        newGrassTile.SetTileData(TileType.GRASS, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null, mapLevels.Count - 1);
                    }
                }
                else // All layers below the top layer
                {
                    StoneTile newStoneTile = ScriptableObject.CreateInstance<StoneTile>();
                    tileMap.SetTile(position, newStoneTile);
                    newStoneTile.SetTileData(TileType.STONE, false, null, 0, tileMap.GetCellCenterWorld(position), -9, false, null, mapLevels.Count - 1);
                }
            }
        }

        // Add stairs to upper and lower levels
        if (mapLevels.Count > 1)
        {
            // Set stairs at center tile in upper level
            Vector3Int upperLevelStairsPosition = new Vector3Int(xMin + (LEVEL_WIDTH / 2) - LEVEL_WIDTH * (mapLevels.Count - 1), yMin + (LEVEL_HEIGHT / 2), 0);
            StairsTile upperLevelStairs = ScriptableObject.CreateInstance<StairsTile>();
            tileMap.SetTile(upperLevelStairsPosition, upperLevelStairs);
            upperLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(upperLevelStairsPosition), -9, false, null, mapLevels.Count - 1);
            mapLevels[mapLevels.Count - 2].AddDescendingStairs(upperLevelStairs);

            // Set stairs in lower level
            Vector3Int lowerLevelStairsPosition = new Vector3Int(xMin + (LEVEL_WIDTH / 2), yMin + (LEVEL_HEIGHT / 2), 0);
            StairsTile lowerLevelStairs = ScriptableObject.CreateInstance<StairsTile>();
            tileMap.SetTile(lowerLevelStairsPosition, lowerLevelStairs);
            lowerLevelStairs.SetTileData(TileType.STAIRS, false, null, 0, tileMap.GetCellCenterWorld(lowerLevelStairsPosition), -9, false, null, mapLevels.Count - 1);
            mapLevels[mapLevels.Count - 1].AddAscendingStairs(lowerLevelStairs);

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

    public static void PopulateWithTrees()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                Item treePrefab = Resources.Load<Item>("prefabs/items/Tree");
                Item treeInstance = UnityEngine.Object.Instantiate(treePrefab, tile.position, Quaternion.identity);
                treeInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, treeInstance, tile.resourceCount, tile.position);
            }
        }
    }

    public static void PopulateWithTree()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile tile = (BaseTile)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
            {
                Item treePrefab = Resources.Load<Item>("prefabs/items/Tree");
                Item treeInstance = UnityEngine.Object.Instantiate(treePrefab, tile.position, Quaternion.identity);
                treeInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, treeInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    public static void PopulateWithRocks()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile tile in allTiles)
        {
            if (tile != null && tile.type == TileType.STONE && tile.resource == null)
            {
                Item rockPrefab = Resources.Load<Item>("prefabs/items/Rock");
                Item rockInstance = UnityEngine.Object.Instantiate(rockPrefab, tile.position, Quaternion.identity);
                rockInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, rockInstance, tile.resourceCount, tile.position);
            }
        }
    }

    public static void PopulateWithRocksPerlin()
    {
        // Perlin noise parameters
        float scale = 0.1f;
        float threshold = 0.5f;

        // Randomization offsets
        float offsetX = UnityEngine.Random.Range(0f, 1000f);
        float offsetY = UnityEngine.Random.Range(0f, 1000f);

        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile tile in allTiles)
        {
            if (tile != null && tile.type == TileType.STONE && tile.resource == null)
            {
                // Calculate Perlin noise value for the current tile position
                float perlinValue = Mathf.PerlinNoise((tile.position.x + offsetX) * scale, (tile.position.y + offsetY) * scale);

                // Only place rock if the Perlin noise value is above the threshold
                if (perlinValue > threshold)
                {
                    Item rockPrefab = Resources.Load<Item>("prefabs/items/Rock");
                    Item rockInstance = UnityEngine.Object.Instantiate(rockPrefab, tile.position, Quaternion.identity);
                    rockInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                    tile.SetTileInformation(tile.type, true, rockInstance, tile.resourceCount, tile.position);
                }
            }
        }
    }



    public static void PopulateWithRock()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile tile = (BaseTile)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.STONE && tile.resource == null)
            {
                Item rockPrefab = Resources.Load<Item>("prefabs/items/Rock");
                Item rockInstance = UnityEngine.Object.Instantiate(rockPrefab, tile.position, Quaternion.identity);
                rockInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, rockInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    // GetAdjacentTiles
    // Returns a list of all tiles adjacent to the given tile
    public static List<BaseTile> GetAdjacentTiles(BaseTile tile)
    {
        List<BaseTile> adjacentTiles = new List<BaseTile>();
        Vector3Int[] adjacentPositions = new Vector3Int[4];
        Vector3Int tilePosition = Vector3Int.FloorToInt(tile.position);
        adjacentPositions[0] = new Vector3Int(tilePosition.x + 1, tilePosition.y, tilePosition.z);
        adjacentPositions[1] = new Vector3Int(tilePosition.x - 1, tilePosition.y, tilePosition.z);
        adjacentPositions[2] = new Vector3Int(tilePosition.x, tilePosition.y + 1, tilePosition.z);
        adjacentPositions[3] = new Vector3Int(tilePosition.x, tilePosition.y - 1, tilePosition.z);

        foreach (Vector3Int position in adjacentPositions)
        {
            BaseTile adjacentTile = (BaseTile)tileMap.GetTile(position);
            if (adjacentTile != null)
            {
                adjacentTiles.Add(adjacentTile);
            }
        }

        return adjacentTiles;
    }


    public static void PopulateWithChest()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile tile = (BaseTile)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
            {
                Item chestPrefab = Resources.Load<Item>("prefabs/items/Chest");
                Item chestInstance = UnityEngine.Object.Instantiate(chestPrefab, tile.position, Quaternion.identity);
                chestInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, chestInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    public static void PopulateWithBushes()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile tile in allTiles)
        {
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null && Random.Range(0, 10) == 0)
            {
                Item bushPrefab = Resources.Load<Item>("prefabs/items/Bush");
                Item bushInstance = UnityEngine.Object.Instantiate(bushPrefab, tile.position, Quaternion.identity);
                bushInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, bushInstance, tile.resourceCount, tile.position);
            }
        }
    }

    public static void PopulateWithBush()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile tile = (BaseTile)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
            {
                Item bushPrefab = Resources.Load<Item>("prefabs/items/Bush");
                Item bushInstance = UnityEngine.Object.Instantiate(bushPrefab, tile.position, Quaternion.identity);
                bushInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, true, bushInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    public static void PopulateWithWheatPlants()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        foreach (BaseTile tile in allTiles)
        {
            if (tile != null && tile.type == TileType.SAND && tile.resource == null && Random.Range(0, 10) == 0)
            {
                Item wheatPrefab = Resources.Load<Item>("prefabs/items/Wheat");
                Item wheatInstance = UnityEngine.Object.Instantiate(wheatPrefab, tile.position, Quaternion.identity);
                wheatInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, false, wheatInstance, tile.resourceCount, tile.position);
            }
        }
    }

    public static void PopulateWithWheatPlant()
    {
        TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        int random = Random.Range(0, allTiles.Length);
        for (int i = 0; i < allTiles.Length; i++)
        {
            BaseTile tile = (BaseTile)allTiles[(i + random) % allTiles.Length];
            if (tile != null && tile.type == TileType.SAND && tile.resource == null)
            {
                Item wheatPrefab = Resources.Load<Item>("prefabs/items/Wheat");
                Item wheatInstance = UnityEngine.Object.Instantiate(wheatPrefab, tile.position, Quaternion.identity);
                wheatInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, false, wheatInstance, tile.resourceCount, tile.position);
                break;
            }
        }
    }

    public static List<BaseTile> GetAdjacentAndDiagonalTiles(BaseTile centerTile)
    {
        List<BaseTile> tiles = new List<BaseTile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue; // Skip the center tile itself

                Vector3Int newPosition = new Vector3Int((int)centerTile.position.x + x, (int)centerTile.position.y + y, (int)centerTile.position.z);
                BaseTile tile = GetTile(newPosition);
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }
        }

        return tiles;
    }

    public static void ClearStairs()
    {
        foreach (Level level in mapLevels)
        {
            // Check for both ascending and descending stairs
            List<StairsTile> stairsTiles = new List<StairsTile>();
            stairsTiles.AddRange(level.getAllAscendingStairs());
            stairsTiles.AddRange(level.getAllDescendingStairs());

            foreach (StairsTile stairsTile in stairsTiles)
            {
                List<BaseTile> adjacentAndDiagonalTiles = GetAdjacentAndDiagonalTiles(stairsTile);
                foreach (BaseTile tile in adjacentAndDiagonalTiles)
                {
                    if (tile != null && tile.resource != null)
                    {
                        Destroy(tile.resource.gameObject);
                        tile.SetTileInformation(tile.type, false, null, tile.resourceCount, tile.position);
                    }
                }
            }
        }
    }



}




