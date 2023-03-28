using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// Enum to represent different types of labor tasks
//public enum LaborType { FireFight, Patient, Doctor, Sleep, Basic, Warden, Handle, Cook, Hunt, Construct, Grow, Mine, Farm, Woodcut, Smith, Tailor, Art, Craft, Haul, Clean, Research };

// LaborOrderManager class to manage and assign labor tasks for pawns
public class LaborOrderManager : MonoBehaviour
{
    [SerializeField]
    // Variables for managing pawns, labor tasks, and labor types
    protected GameObject pawn_prefab;
    protected static Queue<Pawn> availablePawns;
    protected static Queue<LaborOrder>[] laborQueues;
    protected static int laborOrderTotal;
    protected static int numOfLaborTypes;

    protected static bool initializeWithPawnsAndOrders = false;
    protected const int NUM_OF_PAWNS_TO_SPAWN = 10;

    // Method to get the total number of labor tasks in the queue
    public static int getNumOfLaborOrders()
    {
        int total = 0;
        for (int i = 0; i < numOfLaborTypes; i++)
        {
            total += laborQueues[i].Count;
        }
        return total;
    }

    // Method to get the total number of labor types
    public static int getNumberOfLaborTypes()
    {
        return numOfLaborTypes;
    }

    // Method to get the total number of labor tasks ever queued
    public static int getLaborOrderTotal()
    {
        return laborOrderTotal;
    }

    // Method to add a pawn to the queue of available pawns
    public static void addPawn(Pawn pawn)
    {
        // add pawn to the queue
        availablePawns.Enqueue(pawn);
    }

    // Method to get an available pawn from the queue
    public static Pawn getAvailablePawn()
    {
        // return pawn from the queue
        Pawn pawn = availablePawns.Dequeue();
        return pawn;
    }

    // Method to add a labor task to the appropriate queue
    public static void addLaborOrder(LaborOrder LaborOrder)
    {
        // check if the labor order is already in the queue
        if (!laborQueues[(int)LaborOrder.getLaborType()].Contains(LaborOrder))
        {
            // add labor order to the queue
            laborQueues[(int)LaborOrder.getLaborType()].Enqueue(LaborOrder);
            laborOrderTotal++;
        }
    }

    // Method to get the number of available pawns
    public static int getPawnCount()
    {
        return availablePawns.Count;
    }

    // Method to assign a labor task to a pawn if possible (based on priority)
    protected void assignPawns()
    {
        while(availablePawns.Count > 0 && getNumOfLaborOrders() > 0){
            
            Pawn pawn = getAvailablePawn();
            List<LaborType>[] laborTypePriority = pawn.getLaborTypePriority();
            bool found = false;

            for (int i = 0; i < laborTypePriority.Length; i++)
            {
                if (laborTypePriority[i] != null)
                {
                    for (int j = 0; j < laborTypePriority[i].Count; j++)
                    {
                        if (laborQueues[(int)laborTypePriority[i][j]] != null && laborQueues[(int)laborTypePriority[i][j]].Count > 0)
                        {
                            LaborOrder order = laborQueues[(int)laborTypePriority[i][j]].Dequeue();
                            pawn.setCurrentLaborOrder(order);
                            order.assignToPawn(pawn);
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
                addPawn(pawn);
            }
        }
    }

    // Awake method to initialize variables and set up the labor task queues
    void Awake()
    {
        laborOrderTotal = 0;
        numOfLaborTypes = Enum.GetNames(typeof(LaborType)).Length;

        // initialize and populate pawn queue (Instantiate them as the children of this object)
        availablePawns = new Queue<Pawn>();
        if (initializeWithPawnsAndOrders)
        {
            for (int i = 0; i < NUM_OF_PAWNS_TO_SPAWN; i++)
            {
                addPawn(Instantiate(pawn_prefab, transform.Find("Pawns")).GetComponent<Pawn>());
            }
        }

        // initialize the array of labor order queues
        laborQueues = new Queue<LaborOrder>[numOfLaborTypes];

        // iterate through the array of labor order queues and initialize each queue
        for (int i = 0; i < numOfLaborTypes; i++)
        {
            laborQueues[i] = new Queue<LaborOrder>();
        }

    }

    // Start method to generate and queue random labor tasks
    void Start()
    {
        // generate 100 labor orders with random labor types and random ttc's and add them to the appropriate queue in the array of labor order queues
        if (initializeWithPawnsAndOrders)
        {
            for (int i = 0; i < 100; i++)
            {
                addLaborOrder(new LaborOrder(true));
            }
        }
    }

    // Update method to assign labor tasks to available pawns
    void Update()
    {
        if (availablePawns.Count > 0 && getNumOfLaborOrders() > 0)
        {
            assignPawns();
        }else{
            // print the number of pawns in the queue and the number of labor tasks in the queue
            //Debug.Log("Pawns: " + getPawnCount() + " Labor Orders: " + getNumOfLaborOrders());
        }
    }
}