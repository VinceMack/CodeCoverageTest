using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Map map;
    private int x, y, z;
    private bool isNavigable = true;
    
    public virtual void InitializeTile(Map map, int x, int y, int z, bool navigable)
    {
        this.map = map;
        this.x = x;
        this.y = y;
        this.z = z;
        this.isNavigable = navigable;
        if(!navigable)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            var boxColliders = this.GetComponents<BoxCollider2D>();
            foreach(BoxCollider2D boxCollider in boxColliders)
            {
                if(!boxCollider.isTrigger)
                {
                    boxCollider.enabled = true;
                }
            }
        }
    }

    // Method returns a list of tiles neighboring the current tile
    // Will return in the order N,E,S,W or N,E,S,W,NE,NW,SW,SE if diagonals included or
    // N,E,S,W,U,D if only vertical and N,E,S,W,NE,NW,SW,SE, {U,N,E,S,W,NE,NW,SW,SE (Above)}, { D,N,E,S,W,NE,NW,SW,SE (Below)} if both
    public List<Tile> GetNeighbors(bool diagonals = false, bool vertical = false)
    {
        List<Tile> neighbors = new List<Tile>();
        neighbors.Add(map.GetTile(x, y+1, z));
        neighbors.Add(map.GetTile(x+1, y, z));
        neighbors.Add(map.GetTile(x, y-1, z));
        neighbors.Add(map.GetTile(x-1, y, z));

        if(diagonals)
        {
            neighbors.Add(map.GetTile(x+1, y+1, z));
            neighbors.Add(map.GetTile(x-1, y+1, z));
            neighbors.Add(map.GetTile(x-1, y-1, z));
            neighbors.Add(map.GetTile(x+1, y-1, z));
        }

        if(vertical)
        {
            neighbors.Add(map.GetTile(x, y, z+1));
            if(diagonals)
            {
                neighbors.Add(map.GetTile(x, y+1, z+1));
                neighbors.Add(map.GetTile(x+1, y, z+1));
                neighbors.Add(map.GetTile(x, y-1, z+1));
                neighbors.Add(map.GetTile(x-1, y, z+1));
                neighbors.Add(map.GetTile(x+1, y+1, z+1));
                neighbors.Add(map.GetTile(x-1, y+1, z+1));
                neighbors.Add(map.GetTile(x-1, y-1, z+1));
                neighbors.Add(map.GetTile(x+1, y-1, z+1));
            }

            neighbors.Add(map.GetTile(x, y, z-1));
            if(diagonals)
            {
                neighbors.Add(map.GetTile(x, y+1, z-1));
                neighbors.Add(map.GetTile(x+1, y, z-1));
                neighbors.Add(map.GetTile(x, y-1, z-1));
                neighbors.Add(map.GetTile(x-1, y, z-1));
                neighbors.Add(map.GetTile(x+1, y+1, z-1));
                neighbors.Add(map.GetTile(x-1, y+1, z-1));
                neighbors.Add(map.GetTile(x-1, y-1, z-1));
                neighbors.Add(map.GetTile(x+1, y-1, z-1));
            }
        }

        return neighbors;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
