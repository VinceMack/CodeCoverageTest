using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Mine : LaborOrder_Base
{
    public Item resource;
    private static float BASE_TTC = 3f;
    private Item targetPlant;

    // constructor
    public LaborOrder_Mine(Item targetPlant)
    {
        laborType = LaborType.Mine;
        timeToComplete = BASE_TTC;
        if (resource == null)
        {
            int random = UnityEngine.Random.Range(0, 10);
            if (random == 1)
                resource = Resources.Load<Item>("prefabs/items/Coin");
            else
                resource = Resources.Load<Item>("prefabs/items/RockResource");
        }
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
                
                Item resourceObject = UnityEngine.Object.Instantiate(resource, treePosition, Quaternion.identity).GetComponent<Item>();
                resourceObject.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                tile.SetTileInformation(tile.type, false, resourceObject, tile.resourceCount, tile.position);

                // set the resource of the tile
                tile.resource = resourceObject;
            }
        }
        yield break;
    }

}



