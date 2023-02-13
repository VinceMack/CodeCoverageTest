using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<List<Tile>> tiles;
    [SerializeField] private int worldLength, worldHeight, worldDepth;
    [SerializeField] private GameObject testTilePrefab;

    // Returns a tile at the specified coordiantes
    public Tile GetTile(int x, int y, int z)
    {
        return(tiles[z][y*worldLength+x]);
    }

    public void SpawnWorld(GameObject tilePrefab, int length, int height, int depth, Vector3 origin)
    {
        this.worldLength = length;
        this.worldHeight = height;
        this.worldDepth = depth;
        for(int k = 0; k < depth; k++)
        {
            for(int j = 0; j < height; j++)
            {
                for(int i = 0; i < length; i++)
                {
                    GameObject latestTile = Instantiate(tilePrefab, new Vector3(origin.x + i, origin.y - j, origin.z - k), new Quaternion());
                    latestTile.transform.SetParent(transform);
                    bool navigable = true;
                    if(i == length - 1 || i == 0 || j == height - 1 || j == 0)
                    {
                        navigable = false;
                    }
                    latestTile.GetComponent<Tile>().InitializeTile(this, i, j, k, navigable);
                }
            }
        }
    }

    [ContextMenu("TestSpawnWorld")]
    private void TestSpawnWorld()
    {
        SpawnWorld(testTilePrefab, worldLength, worldHeight, worldDepth, new Vector3(0.5f, 0.5f, 0));
    }
}
