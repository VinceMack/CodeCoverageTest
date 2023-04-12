using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string resourceName;

    public string displayName;

    public List<string> resourceItems = new List<string>();

    public bool isEssential;
    public Condition essentialCondition;

    private GlobalStorage globalStorage;

    // For non-essential resources
    public Resource(string backendName, string frontendName, List<string> items, bool essential, GlobalStorage gs)
    {
        resourceName = backendName;
        displayName = frontendName;
        resourceItems = items;
        isEssential = essential;
        globalStorage = gs;
    }

    // For essential resources
    public Resource(string backendName, string frontendName, List<string> items, bool essential, Condition condition, GlobalStorage gs)
    {
        resourceName = backendName;
        displayName = frontendName;
        resourceItems = items;
        isEssential = essential;
        essentialCondition = condition;
        globalStorage = gs;
    }

    // Will loop through each item which defines this resource and grab the quantity of the item via the GlobalStorage
    public int GetResourceQuantity()
    {
        int total = 0;
        foreach(string itemName in resourceItems)
        {
            //total += gs.GetItemQuantity(itemName);
        }
        return total;
    }

    // Will assess whether an essential resource's condition is met
    public bool EssentialConditionMet()
    {
        if(!isEssential)
        {
            return true;
        }

        return essentialCondition.ConditionMet(GetResourceQuantity());
    }
}
