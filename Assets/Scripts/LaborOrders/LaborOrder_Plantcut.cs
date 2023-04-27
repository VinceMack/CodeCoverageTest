using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Plantcut : LaborOrder_Base
{
    public static Item resource;
    private static float BASE_TTC = 3f;
    private Item targetPlant;

    // constructor
    public LaborOrder_Plantcut(Item targetPlant)
    {
        laborType = LaborType.Plantcut;
        timeToComplete = BASE_TTC;
        if (resource == null) resource = Resources.Load<Item>("prefabs/items/WheatItem");
        this.targetPlant = targetPlant;
        location = Vector3Int.FloorToInt(targetPlant.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn pawn)
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
                UnityEngine.Object.Destroy(targetPlant.gameObject);

                // create wheat in tree's place
                BaseTile tile = (BaseTile)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(treePosition));
                Item resourceItem = Resources.Load<Item>("prefabs/items/Wheat").GetComponent<Item>();
                Item resourceObject = UnityEngine.Object.Instantiate(resourceItem, treePosition, Quaternion.identity).GetComponent<Item>();
                resourceObject.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));

                resourceObject.GetComponent<Wheat>().Itemize();
                tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile> adjacentTiles = GridManager.GetAdjacentTiles(tile);
                foreach (BaseTile adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        Item wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity).GetComponent<Item>();
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



