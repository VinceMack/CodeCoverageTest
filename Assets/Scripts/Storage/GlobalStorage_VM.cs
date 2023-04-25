using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GlobalStorage_VM
{
    // dictionary of chests and their location
    public static Dictionary<Chest_VM, Vector3> chests = new Dictionary<Chest_VM, Vector3>();
    // dictionary of items and the chests they are in
    public static Dictionary<GameObject, Chest_VM> itemLocations = new Dictionary<GameObject, Chest_VM>();

    // Method to add a chest to the global storage
    public static void AddChest(Chest_VM chest, Vector3 location)
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
    public static void RemoveChest(Chest_VM chest)
    {
        // if the chest is in the global storage, remove it
        if (chests.ContainsKey(chest))
        {
            chests.Remove(chest);
        }
    }

    // Method to add an item to the global storage
    public static void AddItem(GameObject item, Chest_VM chest)
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

    public static Chest_VM GetRandomChest()
    {
        if (chests.Count == 0)
        {
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, chests.Count);
        KeyValuePair<Chest_VM, Vector3> randomChest = chests.ElementAt(randomIndex);
        return randomChest.Key;
    }

    // Method to get the closest chest to a given location
    public static Chest_VM GetClosestChest(Vector3 location)
    {
        // if there are no chests in the global storage, return null
        if (chests.Count == 0)
        {
            return null;
        }

        // initialize the closest chest and its distance
        Chest_VM closestChest = chests.ElementAt(0).Key;
        float closestDistance = Vector3.Distance(location, chests[closestChest]);

        // iterate through the chests in the global storage
        foreach (KeyValuePair<Chest_VM, Vector3> chest in chests)
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

}
