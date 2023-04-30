using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static readonly int NUM_OF_PAWNS_TO_SPAWN = 10;
    //private int NUM_OF_LABOR_ORDERS_TO_SPAWN = 10;
    private const int NUM_OF_LEVELS = 2;

    //[SerializeField] private InputManager inputManager;
    //[SerializeField] private LaborOrderPanelManager laborOrderPanelManager;

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

        // initialize camera 
        CameraManager.InitializeCamera();

        // initialize the labor order manager
        LaborOrderManager.InitializeLaborOrderManager();

        // fill the labor order manager with random pawns and labor orders
        Pawn.PawnList.Clear();
        LaborOrderManager.FillWithRandomPawns(NUM_OF_PAWNS_TO_SPAWN);
    }

    void Start()
    {

    }

    void FixedUpdate()
    {

    }

    void Update()
    {
        InputManager.CheckForInput();
        // if there are available pawns and labor orders, assign pawns to labor orders
        if (LaborOrderManager.GetAvailablePawnCount() > 0 && LaborOrderManager.GetLaborOrderCount() > 0)
        {
            LaborOrderManager.AssignPawnsToLaborOrders();
        }

        // if there are working pawns, start working pawns
        if (LaborOrderManager.GetWorkingPawnCount() > 0)
        {
            LaborOrderManager.StartAssignedPawns();
        }
    }

    void LateUpdate()
    {

    }
}




