using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : SaveableEntity
{
    public List<Item> storageList;
    public List<Pawn> workersList;

    public List<Item> filter;

    public LaborOrder zoneAssignedOrder;

    public uint maxPopulationSize;
    public uint maxStorageSize;

    public void Storage()
    {
        storageList = new List<Item>();
        workersList = new List<Pawn>();
        filter = new List<Item>();
        zoneAssignedOrder = null;
        maxPopulationSize = 1;
        maxStorageSize = 1;
    }

    /*
        Constructor for storage if you know what type of zone storage area should be
    */
    public void Storage(Pawn.PawnType type, int maxPopSize, int maxStrgSize)
    {
        storageList = new List<Item>();
        workersList = new List<Pawn>();
        filter = new List<Item>();
        zoneAssignedOrder = null;
        maxPopulationSize = maxPopSize;
        maxStorageSize = maxStrgSize;
    }

    public void Storage(Storage storage)
    {
        storageList = storage.storageList;
        workersList = storage.workersList;
        filter = storage.filter;
        zoneAssignedOrder = storage.zoneAssignedOrder;
        maxPopulationSize = storage.maxPopulationSize;
        maxStorageSize = storage.maxStorageSize;
    }

    //TODO: Once we get a better understanding of tasks, then we should be able to flesh this part of the system out
    public void Update()
    {

        /*
            switch(zoneAssignedOrder)
            {
                case lumber:
                    doLumber();
                case mines:
                    doMining();
                case etc:
                    doEtc();
                default:
                    doDefault();
            }
        */
    }

    public void doTask(Pawn.PawnType taskType)
    {

    }

    public void addWorker(Pawn pawn)
    {
        if (workersList.Count + 1 >= maxPopulationSize || workersList.Contains(pawn))
        {
            return -1;
        }

        pawn.type = Pawn.PawnType.storage;
        pawn.currentOrder = zoneAssignedOrder;
        workersList.Add(pawn);

        return 1;
    }

    public int removeWorker(Pawn pawn)
    {
        int pawnIndex = workersList.FindIndex(pawn);

        if (pawnIndex == -1)
        {
            return -1;
        }

        workersList.RemoveAt(workersList.FindIndex(pawn));

        return pawnIndex;
    }

    //TODO: If change in zone requires change in item filter / pawn jobs, add logic here
    public int changeZoneTask(LaborOrder order, Filter filter)
    {
        zoneAssignedOrder = order;
        this.filter = filter;

    }

    public int changeFilter(Filter filter)
    {
        this.filter = filter;
    }

    //TODO: Implement logic for filters
    public int addItem(Item item)
    {
        if (storageList.Count + 1 >= maxStorageSize)
        {
            return -1;
        } else if (!filter.Contains(item))
        {
            return 0;
        }

        int index = storageList.FindIndex(item);
        
        if (index == -1)
        {
            storageList.Add(item);
            return 1;
        }

        storageList[index].Quantity += item.Quantity;
        return 1;
    }

    //TODO: Add logic for creating item object to add to list rather than just incrementing item quantity
    public int addItem(string itemName, int quanity)
    {
        int index = -1;
        for (int i = 0; i < storageList.Count; ++i)
        {
            if (storageList[i].Name.ToLower().Equals(itemName.ToLower()))
            {
                index = i;
                break;
            }
        }

        if (index == -1)
        {
            return -1;
        }

        storageList[index].Quantity += quanity;
        return 1;
    }

    public int removeItem(Item item)
    {
        if (!storageList.Contains(item))
        {
            return -1;
        }

        storageList.Remove(item);
        return 1;
    }

    public int removeItem(string itemName, int quantity)
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

        if (!existsInList || storageList[index].Quantity < quantity || index = -1)
        {
            return -1;
        }

        storageList[index].Quantity -= quantity;
        return 1;
    }

    //TODO: Use saveable entity functions
}