using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GlobalStorage
{
    public static Dictionary<Chest, Vector3> chests = new Dictionary<Chest, Vector3>();

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

    public static void RemoveChest(Chest chest)
    {
        // if the chest is in the global storage, remove it
        if (chests.ContainsKey(chest))
        {
            chests.Remove(chest);
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

    public static int GetItemCount(string itemName)
    {
        int total = 0;

        foreach (KeyValuePair<Chest, Vector3> kvp in chests)
        {
            total += kvp.Key.ItemCountInChest(itemName);
        }

        return total;
    }

    public static List<Chest> GetChestsWithItem(string itemName)
    {
        List<Chest> returnList = new List<Chest>();

        foreach (KeyValuePair<Chest, Vector3> kvp in chests)
        {
            if (kvp.Key.ItemCountInChest(itemName) > 0)
            {
                returnList.Add(kvp.Key);
            }
        }

        return returnList;
    }

    public static Chest GetClosestChestWithItem(string itemName, Vector3 location)
    {
        List<Chest> chests = GetChestsWithItem(itemName);

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