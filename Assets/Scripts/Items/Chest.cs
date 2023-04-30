using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Item
{
    // dictionary of item names and their quantities
    [SerializeField] public Dictionary<string, int> contents = new Dictionary<string, int>();

    // Method to add an item to the chest
    public void AddItem(string itemName)
    {
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
    public void RemoveItem(string itemName)
    {
        // if the item is in the chest, decrement its quantity
        if (contents.ContainsKey(itemName))
        {
            contents[itemName]--;
        }else{
            Debug.Log("Item not in chest");
            return;
        }

        // if the item's quantity is 0, remove it from the chest
        if (contents[itemName] == 0)
        {
            contents.Remove(itemName);
        }
    }

    // Method to get the quantity of an item in the chest
    public int GetItemQuantity(string itemName)
    {
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
    public bool ContainsItem(string itemName)
    {
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
        GlobalStorage.RemoveChest(this);
        Itemize();
    }

    /// <summary>
    /// <para>Checks if the chest contains the specified item</para>
    /// <para>Returns true if it does, false if it doesn't</para>
    /// </summary>
    public int ItemCountInChest(string itemName)
    {
        if (!contents.ContainsKey(itemName))
        {
            return 0;
        }
        return contents[itemName];
    }

    public override void Awake()
    {
        isCollision = true;
        itemName = "Chest";
        isGatherable = false;
        isPlaceable = true;
        isDeconstructable = true;
        // initialize the contents dictionary
        contents = new Dictionary<string, int>();
        // initialize the location
        location = GridManager.GetTile(Vector3Int.RoundToInt(transform.position));
        // add the chest to the global storage
        GlobalStorage.AddChest(this, transform.position);
    }

    public void ResetPosition()
    {
        location = GridManager.GetTile(new Vector3Int((int)Mathf.Ceil(transform.position.x), (int)Mathf.Ceil(transform.position.y), 0));
    }

}




