using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Place : LaborOrder_Base_VM
{
    GameObject itemToPlace;

    // constructor
    public LaborOrder_Place(GameObject item) : base()
    {
        laborType = LaborType.Place;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager_VM.GetNumOfLaborOrders();
        itemToPlace = item;

        // get tile with no resource
        BaseTile_VM tile;
        do
        {
            // Get a random level
            int randomLevelIndex = UnityEngine.Random.Range(0, GridManager.mapLevels.Count);
            Level level = GridManager.mapLevels[randomLevelIndex];
            // Get a random x and y
            int randomX = UnityEngine.Random.Range(level.getXMin(), level.getXMax());
            int randomY = UnityEngine.Random.Range(level.getYMin(), level.getYMax());

            // Set labor order location
            location = new Vector3Int(randomX, randomY, 0);
            tile = GridManager.GetTile(location);
        }
        while(tile.resource != null);
    }

    public override IEnumerator Execute(Pawn_VM pawn)
    {
        pawn.path.Clear();

        yield return new WaitForSeconds(timeToComplete);
        BaseTile_VM tile = GridManager.GetTile(location);

        if (tile != null)
        {
            if (tile.resource == null)
            {
                tile.SetTileInformation(tile.type, false, itemToPlace, 1, tile.position);
                GameObject parentObjects = GameObject.Find("Objects");

                if (parentObjects != null)
                {
                    GameObject resource = UnityEngine.Object.Instantiate(itemToPlace, GridManager.grid.GetCellCenterWorld(location), Quaternion.identity, parentObjects.transform);
                }
                else
                {
                    Debug.LogWarning("Objects GameObject not found. Could not set parent for the instantiated item.");
                }
            }
            else
            {
                Debug.LogWarning("Tile already contains an item. Cannot place the new item on this tile.");
            }
        }
        else
        {
            Debug.LogWarning("No tile found at the specified location.");
        }
        yield return null;
    }

}