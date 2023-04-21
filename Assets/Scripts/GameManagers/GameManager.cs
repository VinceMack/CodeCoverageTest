using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int NUM_OF_PAWNS_TO_SPAWN = 20;
    private int NUM_OF_LABOR_ORDERS_TO_SPAWN = 100;
    private const int NUM_OF_LEVELS = 4;

    void Awake()
    {
        // set target frame rate to 60
        Application.targetFrameRate = 60;
        // set the screen to not sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // initialize the grid manager
        GridManager.InitializeGridManager();

        // generate random tile map levels
        for (int i = 1; i <= NUM_OF_LEVELS; i++)
        {
            GridManager.CreateLevel();
        }
		
		// Add objects to the map if GlobalInstance2 (TMPCombined) exists
        if (GameObject.Find("GlobalInstance2") != null)
        {
            NUM_OF_PAWNS_TO_SPAWN = 2;
            NUM_OF_LABOR_ORDERS_TO_SPAWN = 0;
            GridManager.PopulateWithTrees();
            GridManager.PopulateWithBushes();
            GridManager.PopulateWithWheat();
        }

        // initialize camera 
	    CameraManager.InitializeCamera();

        // initialize the labor order manager
        LaborOrderManager_VM.InitializeLaborOrderManager();

        // fill the labor order manager with random pawns and labor orders
		Pawn_VM.PawnList.Clear();
        LaborOrderManager_VM.FillWithRandomPawns(NUM_OF_PAWNS_TO_SPAWN);

        // fill the labor order manager with random labor orders
        LaborOrderManager_VM.FillWithRandomLaborOrders(NUM_OF_LABOR_ORDERS_TO_SPAWN);

        // initialize the labor order ui
        LaborOrderPanelManager.InitializeLaborOrderPanel();

    }

    void Start()
    {
		// create labor orders for objects if GlobalInstance2 (TMPCombined) exists
        if (GameObject.Find("GlobalInstance2") != null)
        {
            LaborOrderManager_VM.PopulateObjectLaborOrders();
        }
    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        InputManager.CheckForInput();
        // if there are available pawns and labor orders, assign pawns to labor orders
        if (LaborOrderManager_VM.GetAvailablePawnCount() > 0 && LaborOrderManager_VM.GetLaborOrderCount() > 0)
        {
            LaborOrderManager_VM.AssignPawnsToLaborOrders();
        }

        // if there are working pawns, start working pawns
        if (LaborOrderManager_VM.GetWorkingPawnCount() > 0)
        {
            LaborOrderManager_VM.StartAssignedPawns();
        }
    }

    void LateUpdate()
    {

    }
}
