using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TestUIScript
{
    public static void AddResourcesToWorld()
    {
        GridManager.PopulateWithWheatPlants();
        GridManager.PopulateWithBushes();
        GridManager.PopulateWithTrees();
        GridManager.PopulateWithRocks();
    }

    public static void SpawnChest(Vector2 chestLocation)
    {
        // TileBase[] allTiles = tileMap.GetTilesBlock(tileMap.cellBounds);
        // int random = Random.Range(0, allTiles.Length);
        // for (int i = 0; i < allTiles.Length; i++)
        // {
        //     BaseTile_VM tile = (BaseTile_VM)allTiles[(i + random) % allTiles.Length];
        //     if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
        //     {
        //         GameObject chestPrefab = Resources.Load<GameObject>("prefabs/items/Chest");
        //         GameObject chestInstance = UnityEngine.Object.Instantiate(chestPrefab, tile.position, Quaternion.identity);
        //         chestInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
        //         tile.SetTileInformation(tile.type, true, chestInstance, tile.resourceCount, tile.position);
        //         break;
        //     }
        // }
    }
}
