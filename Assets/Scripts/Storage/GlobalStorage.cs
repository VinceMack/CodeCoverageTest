using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GlobalStorage
{
    // dictionary of chests and their location
    public static Dictionary<Chest, Vector3> chests = new Dictionary<Chest, Vector3>();
    // dictionary of items and the chests they are in
    public static Dictionary<GameObject, Chest> itemLocations = new Dictionary<GameObject, Chest>();

    // Method to add a chest to the global storage
    public static void AddChest(Chest chest, Vector3 location)
    {
        // if the chest is already in the global storage, update its location
        if (chests.ContainsKey(chest))
        {
            chests[chest] = location;
        }
        // otherwise, add it to the global storage
        else
        {
            chests.Add(chest, location);
        }
    }

    // Method to remove a chest from the global storage
    public static void RemoveChest(Chest chest)
    {
        // if the chest is in the global storage, remove it
        if (chests.ContainsKey(chest))
        {
            chests.Remove(chest);
        }
    }

    // Method to add an item to the global storage
    public static void AddItem(GameObject item, Chest chest)
    {
        // if the item is already in the global storage, update its chest
        if (itemLocations.ContainsKey(item))
        {
            itemLocations[item] = chest;
        }
        // otherwise, add it to the global storage
        else
        {
            itemLocations.Add(item, chest);
        }
    }

    public static Chest GetRandomChest()
    {
        if (chests.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, chests.Count);
        KeyValuePair<Chest, Vector3> randomChest = chests.ElementAt(randomIndex);
        return randomChest.Key;
    }

    // Method to get the closest chest to a given location
    public static Chest GetClosestChest(Vector3 location)
    {
        // if there are no chests in the global storage, return null
        if (chests.Count == 0)
        {
            return null;
        }

        // initialize the closest chest and its distance
        Chest closestChest = chests.ElementAt(0).Key;
        float closestDistance = Vector3.Distance(location, chests[closestChest]);

        // iterate through the chests in the global storage
        foreach (KeyValuePair<Chest, Vector3> chest in chests)
        {
            // if the chest is closer than the closest chest, update the closest chest and its distance
            if (Vector3.Distance(location, chest.Value) < closestDistance)
            {
                closestChest = chest.Key;
                closestDistance = Vector3.Distance(location, chest.Value);
            }
        }

        // return the closest chest
        return closestChest;
    }


    /// <summary>
    /// Loops through each storage to find a given ItemName
    /// Returns the count of the item across all storages
    /// </summary>
    public static int GetItemCount(string itemName) 
    {
        int total = 0;

        foreach(KeyValuePair<Chest, Vector3> kvp in chests)
        {
            total += kvp.Key.ItemCountInChest(itemName);
        }

        return total;
    }

    /// <summary>
    /// <para>Gets chest that contains that specified item (first one in the list)</para>
    /// <para>Returns null if no chest contains that item</para>
    /// <para>Returns the list of chests that contains said item</para>
    /// </summary>
    public static List<Chest> GetChestsWithItem(Item item)
    {
        List<Chest> returnList = new List<Chest>();

        foreach(KeyValuePair<Chest, Vector3> kvp in chests)
        {
            if(kvp.Key.ItemCountInChest(item.itemName) > 0)
            {
                returnList.Add(kvp.Key);
            }
        }

        return returnList;
    }

    public static Chest GetClosestChestWithItem(Item item, Vector3 location)
    {
        List<Chest> chests = GetChestsWithItem(item);

        if (chests.Count == 0)
        {
            return null;
        }

        Chest closestChest = chests[0];
        float closestDistance = Vector3.Distance(location, closestChest.transform.position);

        foreach (Chest chest in chests)
        {
            if (Vector3.Distance(location, chest.transform.position) < closestDistance)
            {
                closestChest = chest;
                closestDistance = Vector3.Distance(location, chest.transform.position);
            }
        }

        return closestChest;
    }
}



