using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewWorld : MonoBehaviour
{
    [SerializeField] List<NewTile> tiles = new List<NewTile>();
    [SerializeField] private int width, height, depth;
    [SerializeField] private int x, y, z;

    [ContextMenu("CreateMap")]
    public void CreateMap()
    {
        int count = 0;
        for(int k = 0; k < this.depth; k++)
        {
            for(int j = 0; j < this.height; j++)
            {
                for(int i = 0; i < this.width; i++)
                {
                    tiles.Add(new NewTile(i, j, k, "Tile" + count));
                    count++;
                }
            }
        }
    }

    [ContextMenu("PrintTile")]
    public void PrintTile()
    {
        Debug.Log(GetTileAtPosition(x, y, z).tileName);
    }

    public NewTile GetTileAtPosition(int x, int y, int z)
    {
        return tiles[(z*this.width*this.height) + (y*this.width) + x];
    }
}

public class NewTile
{
    public int x, y, z;
    public bool isNavigable;
    public string tileName;

    public NewTile(int x, int y, int z, string name)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.tileName = name;
    }
}