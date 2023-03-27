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
    public static readonly int MAX_HORIZONTAL = 25;
    public static readonly int MIN_HORIZONTAL = 0;
    public static readonly int MAX_VERTICAL = 25;
    public static readonly int MIN_VERTICAL = 0;

    // Method to get the tile at a specific position in the grid
    public static BaseTile_VM GetTile(Vector3Int position)
    {
        return (BaseTile_VM)tileMap.GetTile(position);
    }

    // Method to create and set a random tile for each position in the grid
    public void setTileMap()
    {
        for (int x = MIN_HORIZONTAL; x < MAX_HORIZONTAL; x++)
        {
            for (int y = MIN_VERTICAL; y < MAX_VERTICAL; y++)
            {
                Vector3Int position = new Vector3Int(x, y, 0);

                // Generate a random tile based on the random value and set its properties
                int random = Random.Range(1, 4);
                switch (random)
                {
                    case 0:
                        BaseTile_VM newBaseTile = ScriptableObject.CreateInstance<BaseTile_VM>();
                        tileMap.SetTile(position, newBaseTile);
                        newBaseTile.setTileData(TileType.GENERIC, false, null, 0, tileMap.WorldToCell(position), -1, false, null);
                        break;
                    case 1:
                        GrassTile_VM newGrassTile = ScriptableObject.CreateInstance<GrassTile_VM>();
                        tileMap.SetTile(position, newGrassTile);
                        newGrassTile.setTileData(TileType.GRASS, false, null, 0, tileMap.WorldToCell(position), -1, false, null);
                        break;
                    case 2:
                        WaterTile_VM newWaterTile = ScriptableObject.CreateInstance<WaterTile_VM>();
                        tileMap.SetTile(position, newWaterTile);
                        newWaterTile.setTileData(TileType.WATER, false, null, 0, tileMap.WorldToCell(position), -1, false, null);
                        break;
                    case 3:
                        RockTile_VM newRockTile = ScriptableObject.CreateInstance<RockTile_VM>();
                        tileMap.SetTile(position, newRockTile);
                        newRockTile.setTileData(TileType.ROCK, true, null, 0, tileMap.WorldToCell(position), -1, false, null);
                        break;
                }

            }
        }
    }

    // Awake method to initialize the grid and tilemap components, and set up the grid with random tiles
    void Awake()
    {
        // Get the Grid component from the "Grid" child object
        grid = transform.Find("Grid").GetComponent<Grid>();

        // Get the Tilemap component from the "Grid" child object
        tileMap = transform.Find("Grid").GetComponent<Tilemap>();

        // Generate the grid with random tiles
        setTileMap();
    }
}
