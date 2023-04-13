using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int NUM_OF_PAWNS_TO_SPAWN = 7;
    private const int NUM_OF_LABOR_ORDERS_TO_SPAWN = 0;

    void Awake()
    {
        // set target frame rate to 60
        Application.targetFrameRate = 60;
        // set the screen to not sleep
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // initialize the grid manager
        GridManager.InitializeGridManager();

        // generate a random tile map
        GridManager.GenerateRandomTileMap();

        // Add objects to the map
        GridManager.PopulateWithTrees();
        GridManager.PopulateWithBushes();

        // initialize the labor order manager
        LaborOrderManager_VM.InitializeLaborOrderManager();

        // fill the labor order manager with random pawns and labor orders
        Pawn_VM.PawnList.Clear();
        LaborOrderManager_VM.FillWithRandomPawns(NUM_OF_PAWNS_TO_SPAWN);

        // fill the labor order manager with random labor orders
        LaborOrderManager_VM.FillWithRandomLaborOrders(NUM_OF_LABOR_ORDERS_TO_SPAWN);

        // initialize the labor order ui
        //LaborOrderPanelManager.InitializeLaborOrderPanel();
        //LaborOrderPanelManager.AddButtons();
    }

    void Start()
    {
        LaborOrderManager_VM.PopulateObjectLaborOrders();
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
