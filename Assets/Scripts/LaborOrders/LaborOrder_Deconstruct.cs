using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class LaborOrder_Deconstruct : LaborOrder_Base_VM
{
    private static float BASE_TTC = 3f;
    private GameObject target;

    // constructor
    public LaborOrder_Deconstruct(GameObject target)
    {
        laborType = LaborType.Deconstruct;
        timeToComplete = BASE_TTC;
        this.target = target;
        location = Vector3Int.FloorToInt(target.transform.position);
    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        pawn.path.Clear();

        yield return new WaitForSeconds(timeToComplete);
        Transform parentTransform = GameObject.Find("Objects").transform;
        // Remove the resources from the tile at the location of the labor order
        BaseTile_VM tile = GridManager.GetTile(location);

        if (tile != null)
        {
            if (tile.resource != null) // Add this null check
            {
                Item itemComponent = tile.resource.GetComponent<Item>();
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