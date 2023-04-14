using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Enum to represent different types of labor tasks
public enum LaborType { FireFight, Patient, Doctor, Sleep, Basic, Warden, Handle, Cook, Hunt, Construct, Grow, Mine, Farm, Woodcut, Smith, Tailor, Art, Craft, Haul, Clean, Research };

// LaborOrderManager_VM class to manage and assign labor tasks for pawns
public class LaborOrderManager_VM : MonoBehaviour
{
    [SerializeField]
    private static Queue<Pawn_VM> availablePawns;
    private static Queue<Pawn_VM> assignedPawns;
    private static Queue<LaborOrder_Base_VM>[] laborQueues;
    private static int laborOrderTotal = 0;

    private const int NUM_OF_PAWNS_TO_SPAWN = 1000;
    private const int NUM_OF_LABOR_ORDERS_TO_SPAWN = 10000;

    // Method to Get the total number of labor tasks in the queue
    public static int GetLaborOrderCount()
    {
        int total = 0;
        for (int i = 0; i < GetLaborTypesCount(); i++)
        {
            total += laborQueues[i].Count;
        }
        return total;
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
        return assignedPawns.Count;
    }

    // Method to return the number of available pawns
    public static int GetAvailablePawnCount()
    {
        return availablePawns.Count;
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
            Debug.Log("Error: type "+ typeString + " not found.");
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
                            pawn.SetCurrentLaborOrder(order);
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
    public static void FillWithRandomPawns(int count)
    {
        availablePawns.Clear();
        for (int i = 0; i < count; i++)
        {
            GameObject pawn_prefab = Resources.Load("prefabs/Pawn_VM") as GameObject;
            AddAvailablePawn(Instantiate(pawn_prefab, GameObject.Find("Pawns").transform).GetComponent<Pawn_VM>());
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
        while (GetWorkingPawnCount() > 0 && GetLaborOrderCount() > 0)
        {
            Pawn_VM pawn = assignedPawns.Dequeue();
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

}