using UnityEngine;
using UnityEngine.Tilemaps;

public class RockTile_VM : BaseTile_VM
{
    // Method to get the tile data for the tile
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Resources.Load<Sprite>("rock");
    }
}