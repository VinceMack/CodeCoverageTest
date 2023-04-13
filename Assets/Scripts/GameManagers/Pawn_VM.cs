using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class Pawn_VM : MonoBehaviour
{
    public List<LaborType>[] laborTypePriority      { get; private set; }   // Priority list for different types of labor
    private LaborOrder_Base_VM currentLaborOrder;                           // Reference to the current labor order
    private bool isAssigned;                                                // Indicates if the pawn is currently assigned to a labor order
    private static int pawnCount = 0;                                       // Counter for the total number of pawns
    private const int NUM_OF_PRIORITY_LEVELS = 4;                           // Number of priority levels for labor types
    public List<Vector3> path                       { get; private set; }   // List of positions for the pawn to follow
    private float pawnSpeed;                                                // Speed of the pawn movement
    private Vector3Int position;                                            // Current position of the pawn in the grid
    private string pawnName;                                                // Name of the pawn

    // pawn constructor
    public Pawn_VM()
    {
        // Initialize the priority list for different types of labor
        laborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            laborTypePriority[i] = new List<LaborType>();
        }

        // Initialize the path list
        path = new List<Vector3>();

        // Initialize the pawn name
        pawnName = "Pawn " + pawnCount;
        pawnCount++;
    }

    // Method to return the number of priority levels
    public int GetPriorityLevelsCount()
    {
        return NUM_OF_PRIORITY_LEVELS;
    }

    // Method to return the currentLaborOrder
    public LaborOrder_Base_VM GetCurrentLaborOrder()
    {
        return currentLaborOrder;
    }

    // Method to return the size of the LaborType enum
    public int GetLaborTypesCount()
    {
        return Enum.GetNames(typeof(LaborType)).Length;
    }

    // Method to return the name of the pawn
    public string GetPawnName()
    {
        return pawnName;
    }

    // Method to return the priority level according to labor type
    public int GetPriorityLevelOfLabor(LaborType type)
    {
        for (int i = 1; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            if (laborTypePriority[i-1] != null)
                if (laborTypePriority[i-1].Contains(type))
                    return i;
        }
        return NUM_OF_PRIORITY_LEVELS;
    }

    // Gets the tile the pawn is currently on from the tilemap
    public TileBase GetPawnTileFromTilemap()
    {
        TileBase tile = GridManager.tileMap.GetTile(GridManager.tileMap.WorldToCell(transform.position));
        if (tile == null)
        {
            Debug.LogError(pawnName + " found a null tile at current location - GetPawnTileFromTilemap");
            return null;
        }
        return tile;
    }

    // Gets the tile of the current labor order from the tilemap
    public TileBase GetLaborOrderTileFromTilemap()
    {
        TileBase tile = GridManager.tileMap.GetTile(currentLaborOrder.location);
        if (tile == null)
        {
            Debug.LogError(pawnName + " found a null tile at current location - GetLaborOrderTileFromTilemap");
            return null;
        }
        return tile;
    }

    // Sets the current labor order for the pawn
    public void SetCurrentLaborOrder(LaborOrder_Base_VM LaborOrder_Base_VM)
    {
        if (isAssigned)
        {
            Debug.LogError("Pawn is already assigned to a labor order");
        }
        else
        {
            currentLaborOrder = LaborOrder_Base_VM;
            isAssigned = true;
            LaborOrderManager_VM.AddAssignedPawn(this);
        }
    }

    // Coroutine to move the pawn along the path
    protected IEnumerator TakePath()
    {
        int pathIndex = 0;

        while (pathIndex < path.Count)
        {
            Vector3Int cellPosition = GridManager.grid.WorldToCell(path[pathIndex]);
            Vector3 centeredWorldPosition = GridManager.grid.GetCellCenterWorld(cellPosition);

            transform.position = Vector3.MoveTowards(transform.position, centeredWorldPosition, pawnSpeed * Time.deltaTime);

            if (transform.position == centeredWorldPosition)
            {
                pathIndex++;
            }
            else
            {
                yield return null;
            }
        }
    }

    // Method to move a given laborType to the priority level above the current one
    public void MoveLaborTypeUpPriorityLevel(LaborType laborType)
    {
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            if (laborTypePriority[i].Contains(laborType))
            {
                if (i == 0)
                {
                    Debug.LogError("Cannot move labor type up priority level; already at the highest priority level");
                    return;
                }
                else
                {
                    laborTypePriority[i].Remove(laborType);
                    laborTypePriority[i - 1].Add(laborType);
                    laborTypePriority[i - 1].Sort();
                    return;
                }
            }
        }
    }

    // Method to move a given laborType to the priority level below the current one
    public void MoveLaborTypeDownPriorityLevel(LaborType laborType)
    {
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            if (laborTypePriority[i].Contains(laborType))
            {
                if (i == NUM_OF_PRIORITY_LEVELS - 1)
                {
                    Debug.LogError("Cannot move labor type down priority level; already at the lowest priority level");
                    return;
                }
                else
                {
                    laborTypePriority[i].Remove(laborType);
                    laborTypePriority[i + 1].Add(laborType);
                    laborTypePriority[i + 1].Sort();
                    return;
                }
            }
        }
    }

    // Coroutine to complete the current labor order
    public IEnumerator CompleteLaborOrder()
    {
        TileBase foundTile = GetLaborOrderTileFromTilemap();
        TileBase currentTile = GetPawnTileFromTilemap();
        

        if (foundTile == null)
        {
            Debug.LogError("foundTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.AddAvailablePawn(this);
            yield break;
        }

        if (currentTile == null)
        {
            Debug.LogError("currentTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.AddAvailablePawn(this);
            yield break;
        }

        BaseTile_VM target = (BaseTile_VM)foundTile;
        BaseTile_VM current = (BaseTile_VM)currentTile;

        Vector3Int targetPosition = Vector3Int.FloorToInt(target.position);
        Vector3Int currentPosition = Vector3Int.FloorToInt(current.position);

        // Get the level the target tile is on
        int targetLevel = targetPosition.x / GridManager.LEVEL_WIDTH;
        // Get the level the pawn is on
        int currentLevel = currentPosition.x / GridManager.LEVEL_WIDTH;

        while (currentPosition != targetPosition)
        {
            // If target location is on a different level, set path to stairs
            if (currentLevel != targetLevel)
            {
                StairsTile_VM stairs;
                Vector3 levelChangeStairsPosition;

                // Get stairs to lower level
                if (currentLevel < targetLevel)
                {
                    stairs = GridManager.mapLevels[currentLevel].getDescendingStairs_VM(currentPosition);
                    if(stairs == null)
                    {
                        Debug.LogError("descending stairs tile is null at CompleteLaborOrder()");
                        break;
		            }
                    levelChangeStairsPosition = stairs.getLowerLevelStairs().position;
                }
                // Get stairs to upper level
                else
                {
                    stairs = GridManager.mapLevels[currentLevel].getAscendingStairs_VM(currentPosition);
                    if(stairs == null)
                    {
                        Debug.LogError("ascending stairs tile is null at CompleteLaborOrder()");
                        break;
		            }
                    levelChangeStairsPosition = stairs.getUpperLevelStairs().position;
                }

                Vector3Int stairsPosition = Vector3Int.FloorToInt(stairs.position);

                // Move to next level
                if (currentPosition == stairsPosition)
                {
                    transform.position = levelChangeStairsPosition;
                    currentLevel = (int)levelChangeStairsPosition.x / GridManager.LEVEL_WIDTH;
                    continue;
                }
                // Set path to stairs at current level
                else
                {
                    path = PathfindingManager.GetPath(currentPosition, Vector3Int.FloorToInt(stairs.position), currentLevel);
		        }
            }
            // Target level is the same as current level
            else
            {
                path = PathfindingManager.GetPath(currentPosition, targetPosition, currentLevel);
	        }

            yield return StartCoroutine(TakePath());
            currentPosition = Vector3Int.FloorToInt(transform.position);
        }

        // check if returned path is empty 
        if (path.Count == 0 && currentPosition != targetPosition)
        {
            Debug.Log($"{pawnName,-10} FAILED Labor Order #{currentLaborOrder.orderNumber,-5} UNREACHABLE: {target.ToString(),-80}");
        }else{
            yield return StartCoroutine(currentLaborOrder.Execute(this));
            Debug.Log($"{pawnName,-10} COMPLETED Labor Order #{currentLaborOrder.orderNumber,-5} TTC: {currentLaborOrder.timeToComplete,-10:F2} {"Order Type: " + currentLaborOrder.laborType,-25} {target.ToString(),-80}");
        }

        LaborOrderManager_VM.AddAvailablePawn(this);
        isAssigned = false;
    }

    // Initialization function for the pawn
    void Awake()
    {
        // Initialize the labor type priority list
        laborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];

        // Assign random priority levels to labor types
        foreach (LaborType laborType in System.Enum.GetValues(typeof(LaborType)))
        {
            int randomPriorityLevel = UnityEngine.Random.Range(0, NUM_OF_PRIORITY_LEVELS);
            if (laborTypePriority[randomPriorityLevel] == null)
            {
                laborTypePriority[randomPriorityLevel] = new List<LaborType>();
            }
            laborTypePriority[randomPriorityLevel].Add(laborType);
        }

        // Set initial position to the center of the cell at the origin
        transform.position = GridManager.grid.GetCellCenterWorld(Vector3Int.zero);

        // set current labor order to null
        currentLaborOrder = null;

        // Set the path to an empty list
        path = new List<Vector3>();

        // Set and increment the pawn name
        pawnName = "Pawn" + ++pawnCount;
        name = pawnName;

        // Set the pawn speed
        pawnSpeed = 2.0f;

        // Set the pawn to not be assigned
        isAssigned = false;
    }
}
