using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Enum to represent different types of labor tasks
public enum LaborType { Woodcut, Mine, Forage, Gather, Craft, Place, Deconstruct, Basic, Plantcut };

// LaborOrderManager_VM class to manage and assign labor tasks for pawns
public class LaborOrderManager_VM : MonoBehaviour
{
    [SerializeField]
    protected static Queue<Pawn_VM> availablePawns;
    private static Queue<Pawn_VM> assignedPawns;
    private static Queue<LaborOrder_Base_VM>[] laborQueues;
    private static int laborOrderTotal = 0;

    private const int NUM_OF_PAWNS_TO_SPAWN = 1000;
    private const int NUM_OF_LABOR_ORDERS_TO_SPAWN = 10000;

    // Method to remove all labor orders from the queue
    public static void ClearLaborOrders()
    {
        // return if laborQueues is null
        if (laborQueues == null)
        {
            return;
        }

        // clear all labor orders
        for (int i = 0; i < GetLaborTypesCount(); i++)
        {
            laborQueues[i].Clear();
        }
    }

    // Method to getNumOfLaborOrders
    public static int GetNumOfLaborOrders()
    {
        return laborOrderTotal;
    }

    // Method to Get the total number of labor tasks in the queue
    public static int GetLaborOrderCount()
    {
        // return if laborQueues is null
        if (laborQueues == null)
        {
            return 0;
        }

        int total = 0;
        for (int i = 0; i < GetLaborTypesCount(); i++)
        {
            total += laborQueues[i].Count;
        }
        return total;
    }

    // Method to add a place labor order to the queue
    public static void AddPlaceLaborOrder(GameObject itemToPlace)
    {
        // create a new place labor order
        LaborOrder_Place placeOrder = new LaborOrder_Place(itemToPlace);
        // add the labor order to the queue
        laborQueues[(int)LaborType.Place].Enqueue(placeOrder);
    }

    // Method to add a destroy labor order to the queue
    public static void AddDeconstructLaborOrder()
    {
        // iterate through all objects and find the first tile with an item that is deconstructable
        GameObject[] objects = FindObjectsOfType<GameObject>();
        
        // Check if the objects array is null
        if (objects == null)
        {
            Debug.LogWarning("No GameObjects found in the scene.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            Item itemComponent = obj.GetComponent<Item>();
            if (itemComponent != null && itemComponent.isDeconstructable)
            {
                // create a new deconstruct labor order
                LaborOrder_Deconstruct deconstructOrder = new LaborOrder_Deconstruct(obj);
                // add the labor order to the queue
                laborQueues[(int)LaborType.Deconstruct].Enqueue(deconstructOrder);
                return;
            }
        }
    }

    // Method to Get the total number of labor types
    public static int GetLaborTypesCount()
    {
        return Enum.GetNames(typeof(LaborType)).Length;
    }

    // Method to Get the number of available pawns
    public static int GetPawnCount()
    {
        return availablePawns.Count;
    }

    // Method to return the number of available pawns
    public static int GetWorkingPawnCount()
    {
        // null check
        if (assignedPawns == null)
        {
            return 0;
        }

        return assignedPawns.Count;
    }

    // Method to return the number of available pawns
    public static int GetAvailablePawnCount()
    {
        // null check
        if (availablePawns == null)
        {
            return 0;
        }
        
        return availablePawns.Count;
    }

    // Method to prematurely find and remove a specific pawn from the availablePawns and assignedPawns queues
    // used to clean up after dying
    public static void RemoveSpecificPawn(Pawn_VM pawn)
    {
        // removes the pawn from the queue
        Queue<Pawn_VM> newQueue = new Queue<Pawn_VM>();
        while (availablePawns.Count != 0)
        {
            Pawn_VM queuedPawn = availablePawns.Dequeue();
            if (queuedPawn != pawn)
            {
                newQueue.Enqueue(queuedPawn);
            }
        }
        availablePawns = newQueue;

        newQueue.Clear();
        while (assignedPawns.Count != 0)
        {
            Pawn_VM queuedPawn = assignedPawns.Dequeue();
            if (queuedPawn != pawn)
            {
                newQueue.Enqueue(queuedPawn);
            }
        }
        assignedPawns = newQueue;
    }

    // Method to return the name of labor type by index
    public static string GetLaborTypeName(int i)
    {
        return Enum.GetNames(typeof(LaborType))[i];
    }

    // Method to return the labor type enum by string name
    public static LaborType GetLaborType(string typeString)
    {
        LaborType type;
        if (!Enum.TryParse<LaborType>(typeString, out type))
            Debug.Log("Error: type " + typeString + " not found.");
        return type;
    }

    // Method to add a pawn to the queue of available pawns
    public static void AddAvailablePawn(Pawn_VM pawn)
    {
        // add pawn to the queue
        availablePawns.Enqueue(pawn);
    }

    // Method to add a pawn to the queue of assigned pawns
    public static void AddAssignedPawn(Pawn_VM pawn)
    {
        // add pawn to the queue
        assignedPawns.Enqueue(pawn);
    }

    // Method to add a labor task to the appropriate queue
    public static void AddLaborOrder(LaborOrder_Base_VM LaborOrder_Base_VM)
    {
        // check if the labor order is already in the queue
        if (!laborQueues[(int)LaborOrder_Base_VM.laborType].Contains(LaborOrder_Base_VM))
        {
            // add labor order to the queue
            laborQueues[(int)LaborOrder_Base_VM.laborType].Enqueue(LaborOrder_Base_VM);
            laborOrderTotal++;
        }
    }

    // Method to initialize the labor order manager
    public static void InitializeLaborOrderManager()
    {
        // Initialize the pawn queue
        availablePawns = new Queue<Pawn_VM>();

        // Initialize the laborQueues array
        laborQueues = new Queue<LaborOrder_Base_VM>[GetLaborTypesCount()];

        // Initialize the working pawn queue
        assignedPawns = new Queue<Pawn_VM>();

        // Initialize the array of labor order queues
        for (int i = 0; i < GetLaborTypesCount(); i++)
        {
            laborQueues[i] = new Queue<LaborOrder_Base_VM>();
        }
    }

    // Method to assign a labor task to a pawn if possible (based on priority)
    public static void AssignPawnsToLaborOrders()
    {
        while (availablePawns.Count > 0 && GetLaborOrderCount() > 0)
        {
            Pawn_VM pawn = GetAvailablePawn();
            //Debug.Log("Finding order for " + pawn.GetPawnName()); //TMP
            List<LaborType>[] laborTypePriority = pawn.laborTypePriority;
            bool found = false;

            for (int i = 0; i < laborTypePriority.Length; i++)
            {
                if (laborTypePriority[i] != null)
                {
                    for (int j = 0; j < laborTypePriority[i].Count; j++)
                    {
                        if (laborQueues[(int)laborTypePriority[i][j]] != null && laborQueues[(int)laborTypePriority[i][j]].Count > 0)
                        {
                            LaborOrder_Base_VM order = laborQueues[(int)laborTypePriority[i][j]].Dequeue();
                            //Debug.Log("Assigning " + order.laborType.ToString() + " to " + pawn.GetPawnName()); //TMP
                            if (!pawn.SetCurrentLaborOrder(order)) AddLaborOrder(order);
                            found = true;
                            break;
                        }
                    }
                }

                if (found)
                {
                    break;
                }
            }

            // if there were no labor orders which matched the pawn's priorities
            if (!found)
            {
                Debug.Log("NO MATCHING ORDERS.");
                AddAvailablePawn(pawn);
            }
        }
    }

    // Method to initialize and populate pawn queue (Instantiate them as the children of this object)
    //      changed Instantiate to InstantiateEntity to be consistent with save system.
    //      requires GlobalInstance2 (TMPCombined) to be in the scene and PrefabList initialized
    public static void FillWithRandomPawns(int count)
    {
        availablePawns.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject pawn_prefab = Resources.Load<GameObject>("prefabs/npc/Pawn_VM");

            // Instantiate the pawn and store the reference in a variable
            GameObject pawn_instance = UnityEngine.Object.Instantiate(pawn_prefab, GridManager.tileMap.GetCellCenterWorld(Vector3Int.FloorToInt(new Vector3(GridManager.LEVEL_WIDTH / 2, GridManager.LEVEL_HEIGHT / 2, 0))), Quaternion.identity);

            // Set the parent of the instantiated pawn, not the prefab itself
            pawn_instance.transform.SetParent(GameObject.Find("Pawns").transform);
            AddAvailablePawn(pawn_instance.GetComponent<Pawn_VM>());
        }
    }


    // Method to fill the labor order queues with random labor tasks
    public static void FillWithRandomLaborOrders(int count)
    {
        for (int i = 0; i < count; i++)
        {
            AddLaborOrder(new LaborOrder_Base_VM(true));
        }
    }

    // method to go through the queue of working pawns and start the coroutine to complete their labor order
    public static void StartAssignedPawns()
    {
        while (GetWorkingPawnCount() > 0/* && GetLaborOrderCount() > 0*/)
        {
            Pawn_VM pawn = assignedPawns.Dequeue();
            //Debug.Log("Starting order for " + pawn.GetPawnName() + ". " + GetWorkingPawnCount() + " remaining assigned pawns. " + GetLaborOrderCount() + " remaining orders."); //TMP
            pawn.StartCoroutine(pawn.CompleteLaborOrder());
        }
    }

    // Method to Get an available pawn from the queue
    private static Pawn_VM GetAvailablePawn()
    {
        // return pawn from the queue
        Pawn_VM pawn = availablePawns.Dequeue();
        return pawn;
    }

    // Finds all objects that can be associated with a labor order and adds them to the manager
    //  For testing purposes
    public static void PopulateObjectLaborOrders()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        
        // Check if the objects array is null
        if (objects == null)
        {
            Debug.LogWarning("No GameObjects found in the scene.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            if (obj.name == "Tree(Clone)")
            {
                if(obj.GetComponent<Tree>().isForageable == true){
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Tree));
                }

                if(obj.GetComponent<Tree>().isDeconstructable == true){
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Deconstruct(obj));
                }

            }
            
            if (obj.name == "Bush(Clone)") // && obj.GetComponent<Bush>().berryCount > 0
            {
                if(obj.GetComponent<Bush>().isForageable){
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Bush));
                }
                
            }
            
            if (obj.name == "Wheat(Clone)")
            {
                if(obj.GetComponent<Wheat>().isPlantcuttable){
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Plantcut(obj));
                }
                
            }
            
            if (obj.name == "Rock(Clone)")
            {
                if(obj.GetComponent<Rock>().isMineable == true){
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Mine(obj));
                }
                
            }
            else
            {
                Item item = obj.GetComponent<Berries>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }
                item = obj.GetComponent<RockResource>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }
                item = obj.GetComponent<WheatItem>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }
                item = obj.GetComponent<Coin>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }
                item = obj.GetComponent<Wood>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }
                item = obj.GetComponent<Wheat>();
                if(item != null && item.isGatherable)
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                    continue;
                }

            }
        }
    }

    // Finds all objects that can be associated with a labor order and adds them to the manager
    //  For testing purposes
    public static void PopulateObjectLaborOrdersUpdated()
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();
        
        // Check if the objects array is null
        if (objects == null)
        {
            Debug.LogWarning("No GameObjects found in the scene.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            Item item = obj.GetComponent<Item>();

            // check if the item is mineable
            if (item != null && item.isMineable)
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Mine(obj));
                continue;
            }

            // check if the item is plantcuttable
            if (item != null && item.isPlantcuttable)
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Plantcut(obj));
                continue;
            }

            // check if the item is forageable
            if (item != null && item.isForageable)
            {
                // check name to see if it's a bush
                if (obj.name == "Bush(Clone)")
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Bush));
                    continue;
                }

                // check name to see if it's a tree
                if (obj.name == "Tree(Clone)")
                {
                    LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Tree));
                    continue;
                }

            }

            // check if the item is gatherable
            if (item != null && item.isGatherable)
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(obj));
                continue;
            }

            // check if the item is deconstructable
            if (item != null && item.isDeconstructable)
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Deconstruct(obj));
                continue;
            }

        }
    }

    public static void PopulateObjectLaborOrdersMineable()
    {
        PopulateObjectLaborOrdersOfType(item => item.isMineable, obj => new LaborOrder_Mine(obj));
    }

    public static void PopulateObjectLaborOrdersPlantcuttable()
    {
        PopulateObjectLaborOrdersOfType(item => item.isPlantcuttable, obj => new LaborOrder_Plantcut(obj));
    }

    public static void PopulateObjectLaborOrdersForageableBushes()
    {
        PopulateObjectLaborOrdersOfType(item => item.isForageable && item.name == "Bush(Clone)", obj => new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Bush));
    }

    public static void PopulateObjectLaborOrdersForageableTrees()
    {
        PopulateObjectLaborOrdersOfType(item => item.isForageable && item.name == "Tree(Clone)", obj => new LaborOrder_Forage(obj, LaborOrder_Forage.ObjectType.Tree));
    }

    public static void PopulateObjectLaborOrdersGatherable()
    {
        PopulateObjectLaborOrdersOfType(item => item.isGatherable, obj => new LaborOrder_Gather(obj));
    }

    public static void PopulateObjectLaborOrdersDeconstructable()
    {
        PopulateObjectLaborOrdersOfType(item => item.isDeconstructable, obj => new LaborOrder_Deconstruct(obj));
    }

    private static void PopulateObjectLaborOrdersOfType(Func<Item, bool> itemCondition, Func<GameObject, LaborOrder_Base_VM> createLaborOrder)
    {
        GameObject[] objects = FindObjectsOfType<GameObject>();

        // Check if the objects array is null
        if (objects == null)
        {
            Debug.LogWarning("No GameObjects found in the scene.");
            return;
        }

        foreach (GameObject obj in objects)
        {
            Item item = obj.GetComponent<Item>();

            if (item != null && itemCondition(item))
            {
                LaborOrderManager_VM.AddLaborOrder(createLaborOrder(obj));
            }
        }
    }

    // populate the object labor order at the location of the tile
    public static void PopulateObjectLaborOrderTile(BaseTile_VM tile)
    {
        // check if the tile is null
        if (tile == null)
        {
            Debug.LogWarning("Tile is null.");
            return;
        }

        // check if the tile has an object
        if (tile.resource == null)
        {
            Debug.LogWarning("Tile does not have an object.");
            return;
        }

        // check if the object is an item
        Item item = tile.resource.GetComponent<Item>();
        if (item == null)
        {
            Debug.LogWarning("Tile object is not an item.");
            return;
        }

        // check if the item is mineable
        if (item.isMineable)
        {
            LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Mine(tile.resource));
            return;
        }

        // check if the item is plantcuttable
        if (item.isPlantcuttable)
        {
            LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Plantcut(tile.resource));
            return;
        }

        // check if the item is forageable
        if (item.isForageable)
        {
            // check name to see if it's a bush
            if (tile.resource.name == "Bush(Clone)")
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(tile.resource, LaborOrder_Forage.ObjectType.Bush));
                return;
            }

            // check name to see if it's a tree
            if (tile.resource.name == "Tree(Clone)")
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(tile.resource, LaborOrder_Forage.ObjectType.Tree));
                return;
            }

        }

        // check if the item is gatherable
        if (item.isGatherable)
        {
            LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Gather(tile.resource));
            return;
        }

        // check if the item is deconstructable
        if (item.isDeconstructable)
        {
            LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Deconstruct(tile.resource));
            return;
        }

    }

    private void Awake()
    {
        availablePawns = new Queue<Pawn_VM>();
        assignedPawns = new Queue<Pawn_VM>();
    }

}