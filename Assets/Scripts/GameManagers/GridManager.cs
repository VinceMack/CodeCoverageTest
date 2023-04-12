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

    // Constants for the size of the grid
    public static readonly int MAX_HORIZONTAL = 50;
    public static readonly int MIN_HORIZONTAL = 0;
    public static readonly int MAX_VERTICAL = 25;
    public static readonly int MIN_VERTICAL = 0;

    // Method to reset grid values for pathfinding
    public static void ResetGrid()
    {
        // Iterate through each tile in the grid and reset its properties
        for (int x = GridManager.MIN_HORIZONTAL; x < GridManager.MAX_HORIZONTAL; x++)
        {
            for (int y = GridManager.MIN_VERTICAL; y < GridManager.MAX_VERTICAL; y++)
            {
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(x, y, 0));

                // Check if tile is not null and debug if it is
                if (tile == null)
                {
                    Debug.LogError("tile is null @ resetGrid");
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

    // Method to create and Set a random tile for each position in the grid
    public static void GenerateRandomTileMap()
    {
        for (int x = MIN_HORIZONTAL; x < MAX_HORIZONTAL; x++)
        {
            for (int y = MIN_VERTICAL; y < MAX_VERTICAL; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // Generate a random tile based on the random value and Set its properties
                int random = Random.Range(1, 5);
                if (random == 4)
                {
                    random = Random.Range(3, 5); // reduce the chance of rock, replaced decreased chances with chance to increase water or sand
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
    }

    // Method to initialize the GridManager
    public static void InitializeGridManager()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        tileMap = GameObject.Find("Grid").GetComponent<Tilemap>();
    }

}
