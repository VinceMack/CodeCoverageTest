using UnityEngine;
using UnityEngine.Tilemaps;

#nullable enable

// enum of the different types of tiles
public enum TileType { GENERIC, GRASS, ROCK, WATER, STAIRS }

public class BaseTile_VM : Tile
{
    // properties of the tile
    protected TileType type;                           // type of the tile
    protected GameObject? resource;                    // resource on the tile (can be null)
    protected int resourceCount;                       // number of resources on the tile
    protected Vector3 position;                        // position of the tile in 3D space

    // properties used for pathfinding
    public int distance;                               // distance from starting tile
    public bool visited;                               // flag to indicate if the tile has been visited
    protected bool isCollision;                        // flag to indicate if the tile can be collided with
    public BaseTile_VM? parent = null;                 // parent tile used in pathfinding (can be null)

    // This is a ScriptableObject, so no constructor is called when it is created

    // method to return the resource of the tile
    public GameObject? GetResource()
    {
        return resource;
    }

    // method to return the resourceCount of the tile
    public int GetResourceCount()
    {
        return resourceCount;
    }

    // method to return the position of the tile
    public Vector3 GetPosition()
    {
        return position;
    }

    // method to return the x position of the tile
    public int GetXPosition()
    {
        return (int)position.x;
    }

    // method to return the y position of the tile
    public int GetYPosition()
    {
        return (int)position.y;
    }

    // method to Set the properties of the tile
    public void SetTileData(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile_VM parent)
    {
        type = tileType;
        isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
        this.distance = distance;
        this.visited = visited;
        this.parent = parent;
    }

    // override of the GetTileData method from the Tile class
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // load a generic sprite if no sprite is found
        Sprite genericSprite = Resources.Load<Sprite>("generic");
        if (genericSprite == null)
        {
            Debug.LogError("Error loading 'generic' sprite.");
            return;
        }

        // Set the tile sprite to the generic sprite and the tile type to GENERIC
        tileData.sprite = genericSprite;
        type = TileType.GENERIC;
    }

    // method to Get the collision flag of the tile
    public bool GetCollision()
    {
        return isCollision;
    }

    // override of the RefreshTile method from the Tile class
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // call the base class RefreshTile method
        base.RefreshTile(position, tilemap);
    }

    // method to Get the tile type
    public TileType GetTileType()
    {
        return type;
    }

    // method to Set the tile information
    public void SetTileInformation(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position)
    {
        type = tileType;
        isCollision = collision;
        this.resource = resource;
        this.resourceCount = resourceCount;
        this.position = position;
    }

    // method to print the tile information to the console
    public void printTileInformation()
    {
        // print the information of the tile include the type, collision, resource, resource count, and position. left align the text and evenly space the columns.
        Debug.Log(returnTileInformation());
    }

    // method to return the tile information as a string
    public string returnTileInformation()
    {
        if (resource == null)
        {
            // print the information of the tile include the type, collision, resource, resource count, and position. left align the text using interpolation and evenly space the columns.
            return $"Tile Type: {type,-10} Collision: {isCollision,-10} Position: {position,-50}";
        }
        else
        {
            // print the information of the tile include the type, collision, resource, resource count, and position. left align the text using interpolation and evenly space the columns.
            return $"Tile Type: {type,-10} Collision: {isCollision,-10} Resource: {resource.name,-10} ResourceCount: {resourceCount,-10} Position: {position,-50}";
        }
    }

    // method to Set the tile type
    public void SetType(TileType tileType)
    {
        type = tileType;
    }

    // method to Set the collision flag of the tile
    public void SetCollision(bool collision)
    {
        isCollision = collision;
    }

    // method to Set the resource on the tile
    public void SetResource(GameObject resource)
    {
        this.resource = resource;
    }

    // method to Set the number of resources on the tile
    public void SetResourceCount(int resourceCount)
    {
        this.resourceCount = resourceCount;
    }

    // method to Set the position of the tile
    public void SetPosition(Vector3 position)
    {
        this.position = position;
    }

}