using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource
{
    public string resourceName;
    public string displayName;
    private int quantity;
    private Value value;

    public Resource(string name, string displayName, Value associatedCondition)
    {
        this.resourceName = name;
        this.displayName = displayName;
        this.value = associatedCondition;
    }

    public Resource(string name, string displayName)
    {
        this.resourceName = name;
        this.displayName = displayName;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public void AddQuantity(int addition)
    {
        quantity += addition;
    }
}
