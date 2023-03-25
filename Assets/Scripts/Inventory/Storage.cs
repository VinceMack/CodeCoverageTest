using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour
{
    public List<Item> storageList;
    public List<Pawn> workersList;

    public List<Item> filter;

    public LaborOrder zoneAssignedOrder;

    /*
        Basic Constructor
    */
    public Storage()
    {
        storageList = new List<Item>();
        workersList = new List<Pawn>();
        filter = new List<Item>();
        zoneAssignedOrder = null;
    }

    /*
        Adds worker to the worker list.
        Returns -1 if unsuccessful.
        Returns new size of worker list if successful.
    */
    public virtual int addWorker(Pawn pawn)
    {
        if (workersList.Contains(pawn))
        {
            return -1;
        }
        
        pawn.setCurrentLaborOrder(zoneAssignedOrder);
        workersList.Add(pawn);

        return workersList.Count;
    }

    /*
        Removes worker from the worker list.
        Returns -1 if unsuccessful.
        Returns new size of worker list if successful.
    */
    public virtual int removeWorker(Pawn pawn)
    {
        int pawnIndex = -1;

        for (int i = 0; i < workersList.Count; ++i)
        {
            var p = workersList[i];
            if (p.getPawnName().ToLower().Equals(pawn.getPawnName().ToLower()))
            {
                pawnIndex = i;
                break;
            }
        }

        if (pawnIndex == -1)
        {
            return -1;
        }

        workersList.RemoveAt(pawnIndex);
        return workersList.Count;
    }

    /*
        Changes the current zone assigned order to the order passed in
    */
    public virtual void changeZoneTask(LaborOrder order)
    {
        zoneAssignedOrder = order;
    }

    /*
        Changes the current filter list to the list passed in
    */
    public virtual void changeFilter(List<Item> filter)
    {
        this.filter = filter;
    }

    /*
        Adds item to storage list if it is not already in the list. If it is in the list, then it increments the quantity of the item
        Item must be in filter list if you want to add it to the storage list
        Returns -1 if item is not on the filter list and cannot be added to the storage list. 
        Returns updated quantity of specified item if it does exist
    */
    public virtual int addItem(Item item)
    {
        int index = findFilterItem(item);
        
        if (findFilterItem(item) == -1)
        {
            return -1;
        }

        foreach(var i in storageList)
        {
            if (i.Name.ToLower().Equals(item.Name.ToLower()))
            {
                i.Quantity += item.Quantity;
                return i.Quantity;
            }
        }

        storageList.Add(item);
        return storageList[storageList.Count - 1].Quantity;
    }

    /*
        If it is in the list, then it increments the quantity of the item by the quantity passed in
        Returns -1 if item does not exist in storage list. Returns updated quantity of specified item if it does exist
    */
    public virtual int incrementItem(string itemName, int quanity)
    {
        for (int i = 0; i < storageList.Count; ++i)
        {
            if (storageList[i].Name.ToLower().Equals(itemName.ToLower()))
            {
                storageList[i].Quantity += quanity;
                return storageList[i].Quantity;
            }
        }

        return -1;
    }

    /*
        If it is in the list, then it decrements the quantity of the item by the quantity passed in
        If item does not exist, returns -1. Otherwise, returns current size of storage list
    */
    public virtual int removeItem(Item item)
    {
        if (!storageList.Contains(item))
        {
            return -1;
        }

        storageList.Remove(item);
        return storageList.Count;
    }

    /*
        Removes item from storage list if it is in the list. If it is in the list, then it decrements the quantity of the item
        Item must be in filter list if you want to remove it from the storage list
        Returns -1 if item does not exist in storage list. Then returns remaining quantity of specified item if it does exist
    */
    public virtual int decrementItem(string itemName, int quantity)
    {
        bool existsInList = false;
        int index = -1;

        for (int i = 0; i < storageList.Count; ++i)
        {
            if (storageList[i].Name.ToLower().Equals(itemName.ToLower()))
            {
                existsInList = true;
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return -1;
        }

        if (!existsInList)
        {
            return -1;
        }

        if (storageList[index].Quantity < quantity)
        {
            return -1;
        }

        storageList[index].Quantity -= quantity;
        return storageList[index].Quantity;
    }

    /*
        Returns true if the item is in the filter list. Returns false if it is not in the filter list
    */
    public virtual bool isValidItem(Item item)
    {
        foreach (var filterItem in filter)
        {
            if (item.Name.ToLower().Equals(filterItem.Name.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    /*
        Returns the index of the item in the storage list if it exists. Returns -1 if it does not exist
    */
    public virtual int findFilterItem(Item item)
    {
        for (int i = 0; i < filter.Count; ++i)
        {
            if (item.Name.ToLower().Equals(filter[i].Name.ToLower()))
            {
                return i;
            }
        }
        return -1;
    }
}