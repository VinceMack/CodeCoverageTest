using UnityEngine;
using UnityEngine.Tilemaps;

public class SandTile : BaseTile
{
    // Method to Get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("sprites/tiles/sand");
    }
}



