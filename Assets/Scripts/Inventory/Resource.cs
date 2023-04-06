using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string resourceName;

    public string displayName;

    public List<string> resourceItems = new List<string>();

    public bool isEssential;

    public Resource(string backendName, string frontendName, List<string> items, bool essential)
    {
        resourceName = backendName;
        displayName = frontendName;
        resourceItems = items;
        isEssential = essential;
    }

    public int GetResourceQuantity()
    {
        int total = 0;
        foreach(string itemName in resourceItems)
        {
            //total += GlobalStorage.GetItemQuantity(itemName);
        }
        return total;
    }
}
