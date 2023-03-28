using UnityEngine;
using UnityEngine.Tilemaps;

#nullable enable

// enum of the different types of tiles
public enum TileType { GENERIC, GRASS, ROCK, WATER }

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

    // method to return the position of the tile
    public Vector3 getPosition()
    {
        return position;
    }

    // method to return the x position of the tile
    public int getXPosition()
    {
        return (int)position.x;
    }

    // method to return the y position of the tile
    public int getYPosition()
    {
        return (int)position.y;
    }

    // method to set the properties of the tile
    public void setTileData(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position, int distance, bool visited, BaseTile_VM parent)
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

        // set the tile sprite to the generic sprite and the tile type to GENERIC
        tileData.sprite = genericSprite;
        type = TileType.GENERIC;
    }

    // method to get the collision flag of the tile
    public bool getCollision()
    {
        return isCollision;
    }

    // override of the RefreshTile method from the Tile class
    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // call the base class RefreshTile method
        base.RefreshTile(position, tilemap);
    }

    // method to get the tile type
    public TileType getType()
    {
        return type;
    }

    // method to set the tile information
    public void setTileInformation(TileType tileType, bool collision, GameObject resource, int resourceCount, Vector3 position)
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
            return $"Tile Type: {type,-10} Collison: {isCollision,-10} Position: {position,-50}";
        }
        else
        {
            // print the information of the tile include the type, collision, resource, resource count, and position. left align the text using interpolation and evenly space the columns.
            return $"Tile Type: {type,-10} Collison: {isCollision,-10} Resource: {resource.name,-10} ResourceCount: {resourceCount,-10} Position: {position,-50}";
        }
    }

    // method to set the tile type
    public void setType(TileType tileType)
    {
        type = tileType;
    }

    // method to set the collision flag of the tile
    public void setCollision(bool collision)
    {
        isCollision = collision;
    }

    // method to set the resource on the tile
    public void setResource(GameObject resource)
    {
        this.resource = resource;
    }

    // method to set the number of resources on the tile
    public void setResourceCount(int resourceCount)
    {
        this.resourceCount = resourceCount;
    }

    // method to set the position of the tile
    public void setPosition(Vector3 position)
    {
        this.position = position;
    }

}