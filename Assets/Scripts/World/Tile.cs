using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Map map;
    private Layer layer;
    private int x, y, z;
    private bool isNavigable = true;
    private List<PlacedObject> objectsOnTile = new List<PlacedObject>();
    
    public virtual void InitializeTile(Map map, Layer layer, int x, int y, int z, bool navigable)
    {
        this.map = map;
        this.layer = layer;
        this.x = x;
        this.y = y;
        this.z = z;
        this.isNavigable = navigable;
    }

    public (int, int) GetXYLocation()
    {
        return (x, y);
    } 

    public void AddObjectToTile(PlacedObject newObject)
    {
        objectsOnTile.Add(newObject);
    }

    public Layer GetLayer()
    {
        return layer;
    }

    public bool GetIsNavigable()
    {
        return isNavigable;
    }

    public Tile GetNavigableNeighbor()
    {
        List<Tile> neighbors = GetNeighbors(true, false);
        foreach(Tile neighbor in neighbors)
        {
            if(neighbor == null)
            {
                continue;
            }
            
            if(neighbor.GetIsNavigable())
            {
                return neighbor;
            }
        }
        return null;
    }

    // Method returns a list of tiles neighboring the current tile
    // Will return in the order N,E,S,W or N,E,S,W,NE,NW,SW,SE if diagonals included or
    // N,E,S,W,U,D if only vertical and N,E,S,W,NE,NW,SW,SE, {U,N,E,S,W,NE,NW,SW,SE (Above)}, { D,N,E,S,W,NE,NW,SW,SE (Below)} if both
    public List<Tile> GetNeighbors(bool diagonals = false, bool vertical = false)
    {
        List<Tile> neighbors = new List<Tile>();
        neighbors.Add(map.GetTile(x, y-1, z));
        neighbors.Add(map.GetTile(x+1, y, z));
        neighbors.Add(map.GetTile(x, y+1, z));
        neighbors.Add(map.GetTile(x-1, y, z));

        if(diagonals)
        {
            neighbors.Add(map.GetTile(x+1, y-1, z));
            neighbors.Add(map.GetTile(x-1, y-1, z));
            neighbors.Add(map.GetTile(x-1, y+1, z));
            neighbors.Add(map.GetTile(x+1, y+1, z));
        }

        if(vertical)
        {
            neighbors.Add(map.GetTile(x, y, z+1));
            if(diagonals)
            {
                neighbors.Add(map.GetTile(x, y-1, z+1));
                neighbors.Add(map.GetTile(x+1, y, z+1));
                neighbors.Add(map.GetTile(x, y+1, z+1));
                neighbors.Add(map.GetTile(x-1, y, z+1));
                neighbors.Add(map.GetTile(x+1, y-1, z+1));
                neighbors.Add(map.GetTile(x-1, y-1, z+1));
                neighbors.Add(map.GetTile(x-1, y+1, z+1));
                neighbors.Add(map.GetTile(x+1, y+1, z+1));
            }

            neighbors.Add(map.GetTile(x, y, z-1));
            if(diagonals)
            {
                neighbors.Add(map.GetTile(x, y-1, z-1));
                neighbors.Add(map.GetTile(x+1, y, z-1));
                neighbors.Add(map.GetTile(x, y+1, z-1));
                neighbors.Add(map.GetTile(x-1, y, z-1));
                neighbors.Add(map.GetTile(x+1, y-1, z-1));
                neighbors.Add(map.GetTile(x-1, y-1, z-1));
                neighbors.Add(map.GetTile(x-1, y+1, z-1));
                neighbors.Add(map.GetTile(x+1, y+1, z-1));
            }
        }

        return neighbors;
    }

    public Tile GetAboveNeighbor()
    {
        int layerNumber = layer.GetLayerNumber();
        if(layerNumber > 0)
        {
            return map.GetLayer(layerNumber - 1).GetTile(x, y);
        }
        return null;
    }

    public Tile GetBelowNeighbor()
    {
        int layerNumber = layer.GetLayerNumber();
        if(layerNumber < map.GetMapDepth() - 1)
        {
            return map.GetLayer(layerNumber + 1).GetTile(x, y);
        }
        return null;
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
