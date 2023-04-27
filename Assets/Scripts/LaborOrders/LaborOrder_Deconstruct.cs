using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LaborOrder_Deconstruct : LaborOrder_Base
{
    private static float BASE_TTC = 3f;
    private Item target;

    // constructor
    public LaborOrder_Deconstruct(Item target)
    {
        laborType = LaborType.Deconstruct;
        timeToComplete = BASE_TTC;
        this.target = target;
        location = Vector3Int.FloorToInt(target.transform.position);
    }

    public override IEnumerator Execute(Pawn pawn)
    {
        pawn.path.Clear();

        yield return new WaitForSeconds(timeToComplete);
        Transform parentTransform = GameObject.Find("Objects").transform;
        // Remove the resources from the tile at the location of the labor order
        BaseTile tile = GridManager.GetTile(location);

        if (tile != null)
        {
            if (tile.resource != null) // Add this null check
            {
                Item itemComponent = tile.resource.GetComponent<Item>();
                Debug.Log(itemComponent);
                if (itemComponent != null && itemComponent.isDeconstructable)
                {
                    tile.SetTileInformation(tile.type, false, tile.resource, tile.resourceCount, tile.position);
                    itemComponent.Deconstruct();
                }
                else
                {
                    Debug.LogWarning("Tile does not contain an item. Cannot destroy an item on this tile.");
                }
            }
            else
            {
                Debug.LogWarning("Tile resource is null. Cannot destroy an item on this tile.");
            }
        }
        else
        {
            Debug.LogWarning("No tile found at the specified location.");
        }

        yield return null;
    }


}


