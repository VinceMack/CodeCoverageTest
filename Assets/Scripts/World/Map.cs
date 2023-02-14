using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<List<Tile>> tiles;
    [SerializeField] private int worldLength, worldHeight, worldDepth;
    [SerializeField] private GameObject grassTilePrefab;
    [SerializeField] private GameObject oceanTilePrefab;

    // Returns a tile at the specified coordiantes
    public Tile GetTile(int x, int y, int z)
    {
        return(tiles[z][y*worldLength+x]);
    }

    [ContextMenu("RaiseZAxis")]
    public void SwitchZAxis()
    {
        Transform tmpBacker = new GameObject().transform;
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if(child.position.z == 0)
            {
                tmpBacker = child;
                child.position = new Vector3(child.position.x, child.position.y, worldDepth);
            }
            else
            {
                child.position = new Vector3(child.position.x, child.position.y, child.position.z - 1);
            }
        }
        tmpBacker.position = new Vector3(tmpBacker.position.x, tmpBacker.position.y, tmpBacker.position.z - 1);
    }

    public void SpawnWorld(int length, int height, int depth, Vector3 origin)
    {
        this.worldLength = length;
        this.worldHeight = height;
        this.worldDepth = depth;
        for(int k = 0; k < depth; k++)
        {
            GameObject depthParent = Instantiate(new GameObject(), new Vector3(0, 0, depth), new Quaternion());
            depthParent.name = "MapDepth" + k;
            depthParent.transform.SetParent(transform);
            for(int j = 0; j < height; j++)
            {
                for(int i = 0; i < length; i++)
                {
                    GameObject latestTile;

                    if(i == length - 1 || i == 0 || j == height - 1 || j == 0)
                    {
                        latestTile = Instantiate(oceanTilePrefab, new Vector3(origin.x + i, origin.y - j, origin.z - k), new Quaternion());
                        latestTile.GetComponent<Tile>().InitializeTile(this, i, j, k, false);
                    }
                    else
                    {
                        latestTile = Instantiate(grassTilePrefab, new Vector3(origin.x + i, origin.y - j, origin.z - k), new Quaternion());
                        latestTile.GetComponent<Tile>().InitializeTile(this, i, j, k, true);
                    }
                    latestTile.transform.SetParent(depthParent.transform);                   
                }
            }
        }
    }

    [ContextMenu("TestSpawnWorld")]
    private void TestSpawnWorld()
    {
        SpawnWorld(worldLength, worldHeight, worldDepth, new Vector3(0.5f, 0.5f, 0));
    }
}
