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
    // Variables for managing pawns, labor tasks, and labor types
    protected GameObject pawn_prefab;
    protected static Queue<Pawn_VM> availablePawns;
    protected static Queue<LaborOrder_Base_VM>[] laborQueues;
    protected static int laborOrderTotal;

    protected const int NUM_OF_PAWNS_TO_SPAWN = 10;

    // Method to Get the total number of labor tasks in the queue
    public static int GetNumOfLaborOrders()
    {
        int total = 0;
        for (int i = 0; i < GetNumberOfLaborTypes(); i++)
        {
            total += laborQueues[i].Count;
        }
        return total;
    }

    // Method to Get the total number of labor types
    public static int GetNumberOfLaborTypes()
    {
        return Enum.GetNames(typeof(LaborType)).Length;
    }

    // Method to Get the total number of labor tasks ever queued
    public static int GetLaborOrderTotal()
    {
        return laborOrderTotal;
    }

    // Method to add a pawn to the queue of available pawns
    public static void AddPawn(Pawn_VM pawn)
    {
        // add pawn to the queue
        availablePawns.Enqueue(pawn);
    }

    // Method to Get an available pawn from the queue
    public static Pawn_VM GetAvailablePawn()
    {
        // return pawn from the queue
        Pawn_VM pawn = availablePawns.Dequeue();
        return pawn;
    }

    // Method to add a labor task to the appropriate queue
    public static void AddLaborOrder(LaborOrder_Base_VM LaborOrder_Base_VM)
    {
        // check if the labor order is already in the queue
        if (!laborQueues[(int)LaborOrder_Base_VM.GetLaborType()].Contains(LaborOrder_Base_VM))
        {
            // add labor order to the queue
            laborQueues[(int)LaborOrder_Base_VM.GetLaborType()].Enqueue(LaborOrder_Base_VM);
            laborOrderTotal++;
        }
    }

    // Method to Get the number of available pawns
    public static int GetPawnCount()
    {
        return availablePawns.Count;
    }

    // Method to assign a labor task to a pawn if possible (based on priority)
    protected void AssignPawns()
    {
        while(availablePawns.Count > 0 && GetNumOfLaborOrders() > 0){
            
            Pawn_VM pawn = GetAvailablePawn();
            List<LaborType>[] laborTypePriority = pawn.GetLaborTypePriority();
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
                AddPawn(pawn);
            }
        }
    }

    // Method to initialize and populate pawn queue (Instantiate them as the children of this object)
    public void InitializePawns()
    {
        availablePawns = new Queue<Pawn_VM>();
        for (int i = 0; i < NUM_OF_PAWNS_TO_SPAWN; i++)
        {
            AddPawn(Instantiate(pawn_prefab, transform.Find("Pawns")).GetComponent<Pawn_VM>());
        }
    }

    // Method to initialize the array of labor order queues
    public void InitializeLaborQueues()
    {
        laborQueues = new Queue<LaborOrder_Base_VM>[GetNumberOfLaborTypes()];

        // iterate through the array of labor order queues and initialize each queue
        for (int i = 0; i < GetNumberOfLaborTypes(); i++)
        {
            laborQueues[i] = new Queue<LaborOrder_Base_VM>();
        }
    }

    // Awake method to initialize variables and Set up the labor task queues
    void Awake()
    {
        laborOrderTotal = 0;

        // initialize and populate pawn queue (Instantiate them as the children of this object)
        InitializePawns();

        // initialize the array of labor order queues & iterate through the array of labor order queues and initialize each queue
        InitializeLaborQueues();
    }

    // Start method to generate and queue random labor tasks
    void Start()
    {
        // generate 100 labor orders with random labor types and random ttc's and add them to the appropriate queue in the array of labor order queues
        for (int i = 0; i < 100; i++)
        {
            AddLaborOrder(new LaborOrder_Base_VM(true));
        }
    }

    // Update method to assign labor tasks to available pawns
    void Update()
    {
        if (availablePawns.Count > 0 && GetNumOfLaborOrders() > 0)
        {
            AssignPawns();
        }else{
            // print the number of pawns in the queue and the number of labor tasks in the queue
            //Debug.Log("Pawns: " + GetPawnCount() + " Labor Orders: " + GetNumOfLaborOrders());
        }
    }
}