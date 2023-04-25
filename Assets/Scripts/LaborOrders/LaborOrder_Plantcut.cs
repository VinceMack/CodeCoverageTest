using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Plantcut : LaborOrder_Base_VM
{
    public static GameObject resource;
    private static float BASE_TTC = 3f;
    private GameObject targetPlant;

    // constructor
    public LaborOrder_Plantcut(GameObject targetPlant)
    {
        laborType = LaborType.Plantcut;
        timeToComplete = BASE_TTC;
        if (resource == null) resource = Resources.Load<GameObject>("prefabs/items/WheatItem");
        this.targetPlant = targetPlant;
        location = Vector3Int.FloorToInt(targetPlant.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        pawn.path.Clear();

        if (targetPlant != null)
        {
            // cutting down tree
            yield return new WaitForSeconds(timeToComplete);

            if (targetPlant != null)
            {
                // delete tree
                Vector3 treePosition = targetPlant.transform.position;
                Transform treeParent = targetPlant.transform.parent;
                UnityEngine.Object.Destroy(targetPlant);

                // create wheat in tree's place
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(treePosition));
                GameObject resourceItem = Resources.Load<GameObject>("prefabs/items/Wheat");
                GameObject resourceObject = UnityEngine.Object.Instantiate(resourceItem, treePosition, Quaternion.identity);
                resourceObject.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));

                resourceObject.GetComponent<Wheat>().Itemize();
                tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile_VM> adjacentTiles = GridManager.GetAdjacentTiles(tile);
                foreach (BaseTile_VM adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        GameObject wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity);
                        wheatItem.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                        resource = wheatItem;
                        adjacentTile.SetTileInformation(adjacentTile.type, false, wheatItem, adjacentTile.resourceCount, adjacentTile.position);
                        break;
                    }
                }
            }
        }
        yield break;
    }

}
