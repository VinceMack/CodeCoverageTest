using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviour
{
    public List<Chest> inventories = new List<Chest>();
    public Dictionary<Item, List<Chest>> itemReferences = new Dictionary<Item, List<Chest>>();

    /// <summary>
    /// <para>Adds a chest to itemReferences</para>
    /// <para>Returns the current total number of chests with that item</para>
    /// </summary>
    public int AddItem(Item item, Chest chest)
    {
        if(itemReferences.ContainsKey(item))
        {
            itemReferences[item].Add(chest);
        }
        else
        {
            itemReferences.Add(item, new List<Chest>{ chest });
        }

        return itemReferences[item].Count;
    }

    /// <summary>
    /// <para>Gets chest that contains that specified item (first one in the list)</para>
    /// <para>Returns null if no chest contains that item</para>
    /// <para>Returns the list of chests that contains said item</para>
    /// </summary>
    public List<Chest> GetChestWithItem(Item item)
    {
        if(itemReferences.ContainsKey(item))
        {
            return itemReferences[item];
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
        float closest = Mathf.Infinity;

        foreach (var chest in itemReferences[item])
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
        if (itemReferences.ContainsKey(item))
        {
            int count = itemReferences[item].Count - 1;
            List<Chest> newChestList = itemReferences[item];
            newChestList.Remove(chest);
            itemReferences[item] = newChestList;
            return count;
        }

        return -1;
    }
}