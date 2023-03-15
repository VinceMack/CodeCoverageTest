using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    private GameObject grid;
    private GameObject tileMapGameObject;
    private Tilemap tileMap;
    private Player player;

    private void Awake()
    {
        CreateGrid();
        CreateTileMap();
        InitializeTileMap();

        player = (Player)FindObjectOfType(typeof(Player));
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // If mouse left click
        {
            Vector2 mouseLocationAtClick = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int tileMapLocation = tileMap.WorldToCell(mouseLocationAtClick);
            TileBase clickedTile = tileMap.GetTile(tileMapLocation); // Get clicked tile

            // Set player position to clicked position
            player.transform.position = new Vector3(tileMapLocation.x, tileMapLocation.y, 0);
            
            // Cast from TileBase to BaseTile and check the tile type
            BaseTile baseTile = (BaseTile)clickedTile;
            switch (baseTile.getTileInformation())
            {
                case 1:
                    try
                    {
                        RockTile rockTile = (RockTile)baseTile;
                        rockTile.printInformation();
                        rockTile.printResources();
                    }
                    catch (InvalidCastException) { }
                    break;
                case 2:
                    try
                    {
                        GrassTile grassTile = (GrassTile)baseTile;
                        grassTile.printInformation();
                    }
                    catch (InvalidCastException) { }
                    break;
            }
        }

    }

    private void CreateGrid()
    {
        grid = new GameObject("Grid");
        grid.AddComponent<Grid>();
    }

    private void CreateTileMap()
    {
        tileMapGameObject = new GameObject("TileMap");
        tileMap = tileMapGameObject.AddComponent<Tilemap>();
        TilemapRenderer tilemapRenderer = tileMapGameObject.AddComponent<TilemapRenderer>();
        tileMapGameObject.transform.SetParent(grid.transform);
    }

    // Create and set tiles in TileMap
    private void InitializeTileMap()
    {
        for (int x = -16; x < 16; x++)
        {
            for (int y = -9; y < 9; y++)
            {
                if (x > 6 && x < 13 && y < 6 && y > -6)
                {
                    int resources = UnityEngine.Random.Range(1, 50);
                    RockTile rockTile = ScriptableObject.CreateInstance<RockTile>();
                    rockTile.setTileInformation(x, y, 1, true);
                    rockTile.setResources(resources);
                    tileMap.SetTile(new Vector3Int(x, y, 0), rockTile);
                }
                else
                {
                    GrassTile grassTile = ScriptableObject.CreateInstance<GrassTile>();
                    grassTile.setTileInformation(x, y, 2, false);
                    tileMap.SetTile(new Vector3Int(x, y, 0), grassTile);
                }
            }
        }

    }
}
