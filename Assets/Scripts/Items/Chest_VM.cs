using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest_VM : Item
{
    // dictionary of item names and their quantities
    [SerializeField] public Dictionary<string, int> contents = new Dictionary<string, int>();

    // Method to add an item to the chest
    public void AddItem(GameObject item)
    {
        string itemName = item.name;

        // if the item is already in the chest, increment its quantity
        if (contents.ContainsKey(itemName))
        {
            contents[itemName]++;
        }
        // otherwise, add it to the chest
        else
        {
            contents.Add(itemName, 1);
        }
    }

    // Method to remove an item from the chest
    public void RemoveItem(GameObject item)
    {
        string itemName = item.name;

        // if the item is in the chest, decrement its quantity
        if (contents.ContainsKey(itemName))
        {
            contents[itemName]--;
        }
        // if the item's quantity is 0, remove it from the chest
        if (contents[itemName] == 0)
        {
            contents.Remove(itemName);
        }
    }

    // Method to get the quantity of an item in the chest
    public int GetItemQuantity(GameObject item)
    {
        string itemName = item.name;

        // if the item is in the chest, return its quantity
        if (contents.ContainsKey(itemName))
        {
            return contents[itemName];
        }
        // otherwise, return 0
        else
        {
            return 0;
        }
    }

    // Method to check if the chest contains an item
    public bool ContainsItem(GameObject item)
    {
        string itemName = item.name;

        // if the item is in the chest, return true
        if (contents.ContainsKey(itemName))
        {
            return true;
        }
        // otherwise, return false
        else
        {
            return false;
        }
    }

    public new void Deconstruct()
    {
        // check if the chest is empty and debug log if it is not
        if (contents.Count != 0)
        {
            Debug.Log("Chest is not empty. Cannot deconstruct.");
            return;
        }

        // remove the chest from the global storage
        GlobalStorage_VM.RemoveChest(this);
        Itemize();
    }

    void Awake()
    {
        isGatherable = false;
        isPlaceable = true;
        isDeconstructable = true;
        // initialize the contents dictionary
        contents = new Dictionary<string, int>();
        // initialize the location
        location = GridManager.GetTile(Vector3Int.RoundToInt(transform.position));
        // add the chest to the global storage
        GlobalStorage_VM.AddChest(this, transform.position);
    }

}
