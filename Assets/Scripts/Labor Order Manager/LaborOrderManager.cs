using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public enum LaborType { FireFight, Patient, Doctor, Sleep, Basic, Warden, Handle, Cook, Hunt, Construct, Grow, Mine, Farm, Woodcut, Smith, Tailor, Art, Craft, Haul, Clean, Research };

public class LaborOrderManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pawn_prefab;
    [SerializeField]
    private static Queue<Pawn> availablePawns;
    private static Queue<LaborOrder>[] laborQueues;
    private static int laborOrderTotal;
    private static int numOfLaborTypes;

    private const int NUM_OF_PAWNS_TO_SPAWN = 10;

    // returns the total number of labor orders that are currently queued
    public static int getNumOfLaborOrders()
    {
        int total = 0;
        for(int i = 0; i < numOfLaborTypes; i++){
            total += laborQueues[i].Count;
        }
        return total;
    }

    // returns the total number of labor types that exist
    public static int getNumberOfLaborTypes()
    {
        return numOfLaborTypes;
    }

    // returns the total number of labor orders that has ever been queued (used for unique order numbers)
    public static int getLaborOrderTotal()
    {
        return laborOrderTotal;
    }

    // adds a pawn to the queue of available pawns which will be assigned to a labor order
    public static void addPawn(Pawn pawn)
    {
        // add pawn to the queue
        availablePawns.Enqueue(pawn);
    }

    // returns a pawn from the queue of available pawns
    public static Pawn getAvailablePawn()
    {
        // return pawn from the queue
        return availablePawns.Dequeue();
    }

    // adds a labor order to the appropriate queue of labor orders
    public static void addLaborOrder(LaborOrder laborOrder)
    {
        // check if the labor order is already in the queue
        if (!laborQueues[(int)laborOrder.getLaborType()].Contains(laborOrder))
        {
            // add labor order to the queue
            laborQueues[(int)laborOrder.getLaborType()].Enqueue(laborOrder);
            laborOrderTotal++;
        }
    }

    // get the number of available pawns
    public static int getPawnCount(){
        return availablePawns.Count;
    }

    // dequeue a pawn and assign it a labor order if possible, do nothing otherwise
    private void assignPawn()
    {
        try{
            // print out the list of available pawns
            //Debug.Log("Available Pawns: " + availablePawns.Count);
            //foreach(Pawn pawn in availablePawns){
            //    Debug.Log(pawn);
            //}

            // if there are pawns and labor orders available
                // get the labor type priority of the pawn
                // iterate through the labor type priority of the pawn
                    // iterate through the labor types at each level of the labor type priorities of the pawn
                        // if the queue of the labor type is not empty
                            // dequeue the labor order from the queue and assign it to the pawn
            // the pawn will add itself back to the queue once it is done with its current labor order

            if(availablePawns.Count > 0 && getNumOfLaborOrders() > 0){

                Pawn pawn = getAvailablePawn(); // dequeue a pawn
                List<LaborType>[] laborTypePriority = pawn.getLaborTypePriority();

                for(int i = 0; i < laborTypePriority.Length; i++){                                                                          
                    if(laborTypePriority[i] != null) {
                        for(int j = 0; j < laborTypePriority[i].Count; j++){
                            if(laborQueues[(int)laborTypePriority[i][j]] != null && laborQueues[(int)laborTypePriority[i][j]].Count > 0){
                                pawn.setCurrentLaborOrder(laborQueues[(int)laborTypePriority[i][j]].Dequeue());
                                return;
                            }
                        }
                    }
                }

                // if there were no labor orders which matched
                Debug.Log("NO MATCHING ORDERS.");
                addPawn(pawn);
            }
        }catch(Exception e){
            Debug.Log(e);
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        laborOrderTotal = 0;
        numOfLaborTypes = Enum.GetNames(typeof(LaborType)).Length;

        // initialize and populate pawn queue (Instantiate them as the children of this object)
        availablePawns = new Queue<Pawn>();
        for (int i = 0; i < NUM_OF_PAWNS_TO_SPAWN; i++) {
            addPawn(Instantiate(pawn_prefab, transform).GetComponent<Pawn>());
        }

        // initialize the array of labor order queues
        laborQueues = new Queue<LaborOrder>[numOfLaborTypes];

        // iterate through the array of labor order queues and initialize each queue
        for(int i = 0; i < numOfLaborTypes; i++){
            laborQueues[i] = new Queue<LaborOrder>();
        }

        // generate 100 labor orders with random labor types and random ttc's (0.5 seconds to 1 second) and add them to the appropriate queue in the array of labor order queues
        for(int i = 0; i < 100; i++){
            addLaborOrder(new LaborOrder((LaborType)UnityEngine.Random.Range(0, numOfLaborTypes), UnityEngine.Random.Range(0.5f, 1.0f)));
        }
    }

    void Start(){
        // print the total number of pawns and the total number of labor orders 
        Debug.Log("Number of Pawns: " + getPawnCount() + "\tNumber of Labor Orders: " + getNumOfLaborOrders());
    }

    // Update is called once per frame
    void Update()
    {
        assignPawn();
    }
}