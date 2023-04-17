using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public GameObject chestPrefab;
    public Colony myColony;
    public List<Chest> inventories = new List<Chest>();
    public Dictionary<Item, List<Chest>> itemReferences = new Dictionary<Item, List<Chest>>();

    [ContextMenu("TestStorage")]
    public void TestStorage()
    {
        GameObject chest = Instantiate(chestPrefab);
        Chest newChest = chest.GetComponent<Chest>();
        newChest.PlaceObject(new BaseTile(), this);

        Item chestWoodItem = new Item(ItemList.itemList["wood"]);
        chestWoodItem.Quantity = 5;
        
        newChest.ItemAddedToChest(chestWoodItem);
    }

    [ContextMenu("TEST")]
    public void TESTING()
    {
        Debug.Log(GetItemCount("wood"));
    }

    /// <summary>
    /// Will inform the colony script that resources quantities may have changed
    /// </summary>
    public void ItemChanged()
    {
        myColony.UpdateResourceList();
    }

    /// <summary>
    /// <para>Adds a chest to itemReferences</para>
    /// <para>Returns the current total number of chests with that item</para>
    /// </summary>
    public int AddItem(Item item, Chest chest)
    {
        Item key = ItemList.itemList[item.Name];

        if(itemReferences.ContainsKey(key))
        {
            itemReferences[key].Add(chest);
        }
        else
        {
            itemReferences.Add(key, new List<Chest>{ chest });
        }

        return itemReferences[key].Count;
    }

    /// <summary>
    /// <para>Gets chest that contains that specified item (first one in the list)</para>
    /// <para>Returns null if no chest contains that item</para>
    /// <para>Returns the list of chests that contains said item</para>
    /// </summary>
    public List<Chest> GetChestWithItem(Item item)
    {
        Item key = ItemList.itemList[item.Name];

        if(itemReferences.ContainsKey(key))
        {
            return itemReferences[key];
        }

        return null;
    }

    /// <summary>
    /// Gets the closest chest that contains the specified item
    /// Returns closest chest if it exists
    /// Returns null if it does not 
    /// </summary>
    public Chest GetClosestChestWithItem(Item item, Vector3 coordinate)
    {
        Chest closestChest = null;
        Item key = ItemList.itemList[item.Name];
        float closest = Mathf.Infinity;

        foreach (var chest in itemReferences[key])
        {
            float distance = Vector3.Distance(coordinate, chest.coordinate);
            if (distance < closest)
            {
                closest = distance;
                closestChest = chest;
            }
        }

        return closestChest;
    }

    /// <summary>
    /// Deletes chest from itemReferences since it should no longer contain item
    /// Returns the current total number of chests with that item
    /// Returns -1 if itemReferences does not even contain that item
    /// </summary>
    public int DeleteItem(Item item, Chest chest) {
        Item key = ItemList.itemList[item.Name];

        if (itemReferences.ContainsKey(key))
        {
            int count = itemReferences[key].Count - 1;
            List<Chest> newChestList = itemReferences[key];
            newChestList.Remove(chest);
            itemReferences[key] = newChestList;
            return count;
        }

        return -1;
    }

    /// <summary>
    /// Loops through each storage to find a given ItemName
    /// Returns the count of the item across all storages
    /// </summary>
    public int GetItemCount(string itemName) 
    {
        int total = 0;

        foreach(Chest inventory in inventories)
        {
            total += inventory.ItemCountInChest(itemName);
        } 

        return total;
    }
}