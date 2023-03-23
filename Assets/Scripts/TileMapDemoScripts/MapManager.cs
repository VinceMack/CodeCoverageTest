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
    private HighlightTile highlightTile;

    private PathFinding pathFinding;

    private void Awake()
    {
        CreateGrid();
        CreateTileMap();
        InitializeTileMap();
        
        player = (Player)FindObjectOfType(typeof(Player));
        highlightTile = (HighlightTile)FindObjectOfType(typeof(HighlightTile));
        pathFinding = new PathFinding(tileMap);
    }

    void Start()
    {
        //InitializeTrigMap(); // alternative to InitializeTileMap()
    }

    void Update()
    {
        updateGameObjects();
    }

    private void updateGameObjects()
    {
        Vector3Int tileLocation = getTileLocation();
        highlightTile.transform.position = tileLocation;

        if (Input.GetMouseButtonDown(0)){
            TileBase clickedTile = tileMap.GetTile(tileLocation); // Get clicked tile
            player.updatePath(pathFinding.getPath(Vector3Int.FloorToInt(player.transform.position), tileLocation));
        }

        player.updateLocation(10.0f * Time.deltaTime);
    }

    private Vector3Int getTileLocation()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return tileMap.WorldToCell(mousePosition);
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

    // Create random test map
    private void InitializeTrigMap()
    {
        GlobalInstance.Instance.prefabList.InitializePrefabDictionary();

        // coords go from -max to +max
        int maxX = 100;
        int maxY = 75;

        // create random parameters
        float a = UnityEngine.Random.Range(0.0f, 6.28318f);
        float b = UnityEngine.Random.Range(0.0f, 6.28318f);
        float c = UnityEngine.Random.Range(0.0f, 6.28318f);
        float d = UnityEngine.Random.Range(0.0f, 6.28318f);
        Debug.Log("Generating map with paramters: " + a + ", " + b + ", " + c + ", " + d);

        // precalculate
        float[] yp = new float[maxY * 2 + 1];
        float[] xp = new float[maxX * 2 + 1];
        for (int y = 0; y <= 2 * maxY; y++)
            yp[y] = 4.0f * (y-maxY) / maxY;
        for (int x = 0; x <= 2 * maxX; x++)
            xp[x] = 4.0f * (x-maxX) / maxX;

        float[,] yterms = new float[2, maxY * 2 + 1];
        float[,] xterms = new float[2, maxX * 2 + 1];
        for (int y = 0; y <= 2 * maxY; y++)
        {
            yterms[0, y] = Mathf.Cos(yp[y] + b);
            yterms[1, y] = Mathf.Sin(yp[y] - c);
        }
        for (int x = 0; x <= 2 * maxX; x++)
        {
            xterms[0, x] = Mathf.Cos(xp[x] + a);
            xterms[1, x] = Mathf.Sin(xp[x] - d);
        }
        int treecount = 0;

        // generate
        // land is defined as Mathf.Pow((xp+Mathf.Cos(xp+a)-Mathf.Sin(yp-c)),2) + Mathf.Pow((yp + Mathf.Cos(yp + b) - Mathf.Sin(xp - d)), 2) <= 4
        for (int y=-maxY; y<=maxY; y++)
        {
            for(int x=-maxX; x<=maxX; x++)
            {
                float term1 = xp[x + maxX] + xterms[0, x + maxX] - yterms[1, y + maxY];
                float term2 = yp[y + maxY] + yterms[0, y + maxY] - xterms[1, x + maxX];
                if (term1*term1+term2*term2 <= 4)
                {
                    GrassTile grassTile = ScriptableObject.CreateInstance<GrassTile>();
                    grassTile.setTileInformation(x, y, 2, false);
                    tileMap.SetTile(new Vector3Int(x, y, 0), grassTile);

                    
                    if(UnityEngine.Random.Range(0,10)==0)   // create tree
                    {
                        GameObject tree = GlobalInstance.Instance.entityDictionary.InstantiateEntity("tree", "", new Vector3(x + 0.5f, y + 0.5f, 0f));
                        tree.transform.parent = grid.transform;
                        LaborOrderManager.addLaborOrder(new LaborOrder_Woodcut(tree));
                        treecount++;
                    }
                    else if (UnityEngine.Random.Range(0, 100) == 0)  // create pawn
                    {
                        GameObject pawn = GlobalInstance.Instance.entityDictionary.InstantiateEntity("pawn", "", new Vector3(x + 0.5f, y + 0.5f, 0f));
                        pawn.transform.parent = grid.transform;
                        LaborOrderManager.addPawn(pawn.GetComponent<Pawn>());
                    }

                        
                }
                else
                {
                    WaterTile waterTile = ScriptableObject.CreateInstance<WaterTile>();
                    waterTile.setTileInformation(x, y, 1, true);
                    tileMap.SetTile(new Vector3Int(x, y, 0), waterTile);
                    
                }
            }
        }

        /*Debug.Log("trees: " + treecount);
        Debug.Log("orders: " + LaborOrderManager.getNumOfLaborOrders());*/
    }
}
