using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : PlacedObject
{
    public List<Item> contents = new List<Item>();

    public GlobalStorage globalStorage;

    public Vector3 coordinate;

    /// Basic constructor for chest - sets it up so that it hooks up w/ GlobalStorage system
    public Chest(GlobalStorage gs)
    {
        globalStorage = gs;
        globalStorage.inventories.Add(this);
    }

    /// Constructor for chest - sets it up so that it hooks up w/ GlobalStorage system
    /// Also sets the coordinate of the chest
    public Chest(GlobalStorage gs, Vector3 coord)
    {
        globalStorage = gs;
        globalStorage.inventories.Add(this);
        coordinate = coord;
    }

    /// <summary>
    /// <para>Checks if the chest contains the specified item</para>
    /// <para>Returns true if it does, false if it doesn't</para>
    /// </summary>
    public bool DoesContainItem(Item item)
    {
        bool contains = contents.Contains(item);
        return contains;
    }

    /// <summary>
    /// <para>Adds an item to the chest. If it's a new item, also adds it to GlobalStorage</para>
    /// <para>Returns the current total quantity of the specified item</para>
    /// <para>Returns -1 if an error has occured</para>
    /// </summary>
    public int ItemAddedToChest(Item item)
    {
        bool containsItem = false;
        int quantity = -1;

        foreach (var i in contents)
        {
            if (i.Name.Equals(item.Name))
            {
                containsItem = true;
                i.Quantity += item.Quantity;
                quantity = i.Quantity;
            }
        }

        if (!containsItem)
        {
            contents.Add(item);
            globalStorage.AddItem(item, this);
            quantity = item.Quantity;
        }

        return quantity;
    }

    /// <summary>
    /// Deletes item from chest AND removes entry in GlobalStorage for specific item if quantity hits 0
    /// <para>Returns quantity of item that was deleted from chest (0 - n)</para>
    /// <para>Returns -1 if an error has occured</para>
    /// <para>... ie: item not found, error in GlobalStorage, item quantity > chest quantity</para>
    /// </summary>
    public int RemoveItemFromChest(string itemName, int quantity)
    {
        int remainingQuantity = 0;

        foreach (var i in contents)
        {
            if (i.Name.Equals(itemName))
            {
                if (i.Quantity < quantity)
                {
                    return -1;
                }

                i.Quantity -= quantity;
                remainingQuantity = i.Quantity;

                if (i.Quantity == 0)
                {
                    contents.Remove(i);
                    int response = globalStorage.DeleteItem(i, this);
                    if (response == -1)
                    {
                        return -1;
                    }
                }

                break;
            }
        }

        return remainingQuantity;
    }

    /// <summary>
    /// Deletes item from chest AND removes entry in GlobalStorage for specific item if quantity hits 0
    /// <para>Returns quantity of item that was deleted from chest (0 - n)</para>
    /// <para>Returns -1 if an error has occured</para>
    /// <para>... ie: item not found, error in GlobalStorage, item quantity > chest quantity</para>
    /// </summary>
    public int RemoveItemFromChest(Item item)
    {
        int remainingQuantity = 0;

        foreach (var i in contents)
        {
            if (i.Name.Equals(item.Name))
            {
                if (item.Quantity <= i.Quantity)
                {
                    i.Quantity -= item.Quantity;
                    remainingQuantity = i.Quantity;
                    if (remainingQuantity == 0)
                    {
                        int response = globalStorage.DeleteItem(item, this);
                        if (response == -1)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
        }

        return remainingQuantity;
    }

    /// <summary>
    /// Deletes item from chest AND removes entry in GlobalStorage for specific item
    /// <para>Returns quantity of item that was deleted from chest (0 - n)</para>
    /// <para>Returns -1 if an error has occured</para>
    /// <para>... ie: item not found, error in GlobalStorage, item quantity > chest quantity</para>
    /// </summary>
    public int DeleteItemFromChest(Item item)
    {
        int remainingQuantity = -1;

        foreach (var i in contents)
        {
            if (i.Name.Equals(item.Name))
            {
                remainingQuantity = i.Quantity;
                contents.Remove(i);
                break;
            }
        }

        int response = globalStorage.DeleteItem(item, this);
        return response == -1 ? -1 : remainingQuantity;
    }

    
}