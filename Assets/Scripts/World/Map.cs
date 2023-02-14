using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<Layer> layers = new List<Layer>();

    [SerializeField] private int worldLength, worldHeight, worldDepth;
    [SerializeField] private GameObject grassTilePrefab;
    [SerializeField] private GameObject oceanTilePrefab;
    [SerializeField] private int distanceBetweenLayers;

    private void Start() 
    {
        TestSpawnWorld();
    }

    // Returns a tile at the specified coordiantes
    public Tile GetTile(int x, int y, int z)
    {
        return(layers[z].GetTile(x,y));
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
            GameObject newLayer = new GameObject();
            newLayer.name = "Layer" + k;
            newLayer.transform.SetParent(transform);
            newLayer.transform.position = new Vector3(k*distanceBetweenLayers, 0, 0);
            newLayer.AddComponent<Layer>();
            for(int j = 0; j < height; j++)
            {
                for(int i = 0; i < length; i++)
                {
                    GameObject latestTile;

                    if(i == length - 1 || i == 0 || j == height - 1 || j == 0)
                    {
                        latestTile = Instantiate(oceanTilePrefab, new Vector3(), new Quaternion());
                        latestTile.GetComponent<Tile>().InitializeTile(this, i, j, 0, false);
                    }
                    else
                    {
                        latestTile = Instantiate(grassTilePrefab, new Vector3(), new Quaternion());
                        latestTile.GetComponent<Tile>().InitializeTile(this, i, j, 0, true);
                    }
                    newLayer.GetComponent<Layer>().AddTile(latestTile.GetComponent<Tile>());
                    latestTile.transform.SetParent(newLayer.transform);               
                    latestTile.transform.position = new Vector3(origin.x + i + newLayer.transform.position.x, origin.y - j, 0);    
                }
            }
            layers.Add(newLayer.GetComponent<Layer>());
        }
    }

    [ContextMenu("TestSpawnWorld")]
    private void TestSpawnWorld()
    {
        SpawnWorld(worldLength, worldHeight, worldDepth, new Vector3(0.5f, 0.5f, 0));
    }
}
