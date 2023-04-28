using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Craft : LaborOrder_Base
{
    Item itemToPlace;

    // constructor
    public LaborOrder_Craft(Item item) : base()
    {
        laborType = LaborType.Craft;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager.GetNumOfLaborOrders();
        itemToPlace = item;

        // get tile with no resource
        BaseTile tile;
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
        while (tile.resource != null);
    }

    public LaborOrder_Craft(Item item, Vector2 placeLocation) : base()
    {
        laborType = LaborType.Craft;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager.GetNumOfLaborOrders();
        itemToPlace = item;

        // get tile with no resource
        location = new Vector3Int((int)placeLocation.x, (int)placeLocation.y, 0);
    }

    public override IEnumerator Execute(Pawn pawn)
    {
        pawn.path.Clear();

        // Check the requiredForCrafting list of the item to place and check if the the global inventory has enough of the required items
        bool hasEnoughResources = true;
        List<Item> removedItems = new List<Item>();
        foreach (Item requiredItem in itemToPlace.requiredForCrafting)
        {
            if (GlobalStorage.GetItemCount(requiredItem.itemName) < 1)
            {
                hasEnoughResources = false;
                break;
            }
            else
            {
                // Remove the item from the global inventory
                GlobalStorage.GetChestWithItem(requiredItem.itemName).RemoveItem(requiredItem.itemName);
                // Add the removed item to the removedItems list
                removedItems.Add(requiredItem);
            }
        }

        if(!hasEnoughResources)
        {
            Debug.Log("Not enough resources to craft " + itemToPlace.itemName);
            yield break;
        }else{
            Debug.Log("Crafting " + itemToPlace.itemName);
        }

        yield return new WaitForSeconds(timeToComplete);
        BaseTile tile = GridManager.GetTile(location);

        if (tile != null)
        {
            if (tile.resource == null)
            {
                tile.SetTileInformation(tile.type, false, itemToPlace, 1, tile.position);
                GameObject parentObjects = GameObject.Find("Objects");

                if (parentObjects != null)
                {
                    Item resource = UnityEngine.Object.Instantiate(itemToPlace, GridManager.grid.GetCellCenterWorld(location), Quaternion.identity, parentObjects.transform);
                    if (resource.GetComponent<Chest>() != null)
                    {
                        resource.GetComponent<Chest>().ResetPosition();
                    }
                    tile.SetTileInformation(tile.type, resource.isCollision, resource, 1, tile.position);
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



