using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;

public class Pawn_VM : MonoBehaviour
{
    public List<LaborType>[] laborTypePriority { get; private set; }   // Priority list for different types of labor
    private LaborOrder_Base_VM currentLaborOrder;                           // Reference to the current labor order
    private bool isAssigned;                                                // Indicates if the pawn is currently assigned to a labor order
    private static int pawnCount = 0;                                       // Counter for the total number of pawns
    private const int NUM_OF_PRIORITY_LEVELS = 4;                           // Number of priority levels for labor types
    public List<Vector3> path { get; set; }   // List of positions for the pawn to follow
    private float pawnSpeed;                                                // Speed of the pawn movement
    private Vector3Int position;                                            // Current position of the pawn in the grid
    private string pawnName;                                                // Name of the pawn
    private Coroutine currentExecution;                                     // holds a reference to labor order execute() coroutine
    public Coroutine currentPathExecution { get; set; }                     // holds a reference to labor order execute() coroutine
    private AnimatorController anim;

    public static List<Pawn_VM> PawnList = new List<Pawn_VM>();             // a list of all living pawns
    public bool refuseLaborOrders = false;                                  // prevents this pawn from being assigned labor orders, redundant for now but may be useful later
    [SerializeField] public int hunger = 10000;                               // Hunger level of the pawn. Starves at 0
    public Dictionary<string, Item> items;

    private int pathIndex;

    private Vector3 _offset;
    private Color _color;
    private static List<Color> availableColors;
    private List<Color> predefinedColors = new List<Color>
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        new Color(1f, 0.5f, 0f), // Orange
        Color.white,
        Color.gray,
        Color.black
    };

    private string ColorToName(Color color)
    {
        if (color == Color.red) return "Red";
        if (color == Color.green) return "Green";
        if (color == Color.blue) return "Blue";
        if (color == Color.yellow) return "Yellow";
        if (color == Color.cyan) return "Cyan";
        if (color == Color.magenta) return "Magenta";
        if (color == new Color(1f, 0.5f, 0f)) return "Orange";
        if (color == Color.white) return "White";
        if (color == Color.gray) return "Gray";
        if (color == Color.black) return "Black";

        return "Unknown";
    }

    private void OnDrawGizmos()
    {
        if (path != null)
        {
            for (int i = pathIndex; i < path.Count - 1; i++)
            {
                Gizmos.color = _color;
                Gizmos.DrawLine(path[i] + _offset, path[i + 1] + _offset);
            }
        }
    }

    // Method to return the level of the pawn's current tile 
    public int GetTileLevel()
    {
        return GridManager.GetTile(position).level;
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
            if (laborTypePriority[i - 1] != null)
                if (laborTypePriority[i - 1].Contains(type))
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
            Debug.LogWarning(pawnName + " found a null tile at current location - GetPawnTileFromTilemap");
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
            Debug.LogWarning(pawnName + " found a null tile at current location - GetLaborOrderTileFromTilemap");
            return null;
        }
        return tile;
    }

    // Sets the current labor order for the pawn
    // True if successful, false if not
    public bool SetCurrentLaborOrder(LaborOrder_Base_VM LaborOrder_Base_VM)
    {
        if (isAssigned)
        {
            Debug.Log("Pawn is already assigned to a labor order");
            return false;
        }
        else
        {
            currentLaborOrder = LaborOrder_Base_VM;
            isAssigned = true;
            LaborOrderManager_VM.AddAssignedPawn(this);
            //Debug.Log("Added " + GetPawnName() + " to assigned pawns"); //TMP
            return true;
        }
    }

    // Coroutine to move the pawn along the path
    public IEnumerator TakePath()
    {
        anim.SetAnimParameter("walking", true);

        pathIndex = 0;

        while (pathIndex < path.Count)
        {
            Vector3Int cellPosition = GridManager.grid.WorldToCell(path[pathIndex]);
            Vector3 centeredWorldPosition = GridManager.grid.GetCellCenterWorld(cellPosition);

            // Determine if the pawn is moving left or right
            if (centeredWorldPosition.x > transform.position.x)
            {
                anim.SetAnimParameter("xMovement", Vector3.right.x); // Moving right
            }
            else if (centeredWorldPosition.x < transform.position.x)
            {
                anim.SetAnimParameter("xMovement", Vector3.left.x); // Moving left
            }

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

        anim.SetAnimParameter("walking", false); // Not moving
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
                    Debug.LogWarning("Cannot move labor type up priority level; already at the highest priority level");
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
                    Debug.LogWarning("Cannot move labor type down priority level; already at the lowest priority level");
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
            Debug.LogWarning("foundTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.AddAvailablePawn(this);
            yield break;
        }

        if (currentTile == null)
        {
            Debug.LogWarning("currentTile is null; adding pawn back to labor order manager; breaking out of coroutine");
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

        // while the current position is not the target position or any adjacent tile to the target position
        while (currentPosition != targetPosition &&
                currentPosition != targetPosition + Vector3Int.up &&
                currentPosition != targetPosition + Vector3Int.down &&
                currentPosition != targetPosition + Vector3Int.left &&
                currentPosition != targetPosition + Vector3Int.right &&
                currentPosition != targetPosition + Vector3Int.up + Vector3Int.left &&
                currentPosition != targetPosition + Vector3Int.up + Vector3Int.right &&
                currentPosition != targetPosition + Vector3Int.down + Vector3Int.left &&
                currentPosition != targetPosition + Vector3Int.down + Vector3Int.right)
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
                    if (stairs == null)
                    {
                        Debug.LogWarning("descending stairs tile is null at CompleteLaborOrder()");
                        break;
                    }
                    levelChangeStairsPosition = stairs.getLowerLevelStairs().position;
                }
                // Get stairs to upper level
                else
                {
                    stairs = GridManager.mapLevels[currentLevel].getAscendingStairs_VM(currentPosition);
                    if (stairs == null)
                    {
                        Debug.LogWarning("ascending stairs tile is null at CompleteLaborOrder()");
                        break;
                    }
                    levelChangeStairsPosition = stairs.getUpperLevelStairs().position;
                }

                Vector3Int stairsPosition = Vector3Int.FloorToInt(stairs.position);

                // Move to next level
                if (currentPosition == stairsPosition)
                {
                    transform.position = levelChangeStairsPosition;
                    currentPosition = Vector3Int.FloorToInt(levelChangeStairsPosition); // Update currentPosition
                    currentLevel = (int)levelChangeStairsPosition.x / GridManager.LEVEL_WIDTH;
                    continue;
                }
                // Set path to stairs at current level
                else
                {
                    path = PathfindingManager.GetPath(currentPosition, Vector3Int.FloorToInt(stairs.position), currentLevel, true);
                }
            }
            // Target level is the same as current level
            else
            {
                path = PathfindingManager.GetPath(currentPosition, targetPosition, currentLevel, false);
            }

            if (path.Count == 0)
            {
                Debug.Log($"{pawnName,-10} FAILED Labor Order #{currentLaborOrder.orderNumber,-5} UNREACHABLE: {target.ToString(),-80}");
                break;
            }

            currentPathExecution = StartCoroutine(TakePath());
            yield return currentPathExecution;
            currentPathExecution = null;
            currentPosition = Vector3Int.FloorToInt(transform.position);
        }

        if(path.Count > 0){
            currentExecution = StartCoroutine(currentLaborOrder.Execute(this));
            yield return currentExecution;
            Debug.Log($"{pawnName,-10} COMPLETED Labor Order #{currentLaborOrder.orderNumber,-5} TTC: {currentLaborOrder.timeToComplete,-10:F2} {"Order Type: " + currentLaborOrder.laborType,-25} {target.ToString(),-80}");
        }

        isAssigned = false;
        path.Clear();
        currentLaborOrder = null;
        LaborOrderManager_VM.AddAvailablePawn(this);
    }


/*
    // Decrement the hunger for all living pawns
    public static void DecrementHunger(int decAmount)
    {
        for (int i = PawnList.Count - 1; i >= 0; i--)
        {
            PawnList[i].hunger -= decAmount;
            if (PawnList[i].hunger <= 0)
            {
                PawnList[i].hunger = 0;
                PawnList[i].Die("has starved to death.");

            }
            else if (PawnList[i].hunger < PawnHunger.HUNGER_RESPONSE_THRESHOLD)
            {
                PawnHunger.EatFromInventory(PawnList[i]);
            }
        }
    }
*/

    // cancels the current labor order
    public void CancelCurrentLaborOrder()
    {
        path.Clear();
        if (currentExecution != null)
            StopCoroutine(currentExecution);
        if (currentPathExecution != null)
            StopCoroutine(currentPathExecution);
    }

    // kills the pawn. Cancels labor order and removes them from appropriate lists.
    public void Die() { Die("has died."); }
    public void Die(string cause)
    {
        CancelCurrentLaborOrder();
        anim.SetAnimParameter("dead", true);
        PawnList.Remove(this);
        LaborOrderManager_VM.RemoveSpecificPawn(this);
        CancelCurrentLaborOrder();
        refuseLaborOrders = true;
        Debug.Log(pawnName + " " + cause);
        //base.Die();
        //GlobalInstance.Instance.entityDictionary.DestroySaveableObject(this);
        // maybe check if PawnList is empty to initiate the Game Lose here
    }

    private Vector3 GetRandomOffset()
    {
        float xOffset = UnityEngine.Random.Range(0.1f, 0.5f);
        float yOffset = UnityEngine.Random.Range(0.1f, 0.5f);
        return new Vector3(xOffset, yOffset, 0);
    }

    // Initialization function for the pawn
    void Awake()
    {
        _offset = GetRandomOffset();

        if (availableColors == null)
        {
            availableColors = new List<Color>(predefinedColors);
        }

        if (availableColors.Count > 0)
        {
            _color = availableColors[0];
            availableColors.RemoveAt(0);
        }
        else
        {
            Debug.LogWarning("No more available colors. Assigning the default color.");
            _color = Color.white;
        }

        name = ColorToName(_color);

        // animator
        anim = GetComponent<AnimatorController>();

        // Initialize the labor type priority list and sublists
        laborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            laborTypePriority[i] = new List<LaborType>();
        }
        

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
        //transform.position = GridManager.grid.GetCellCenterWorld(Vector3Int.zero);

        // set current labor order to null
        currentLaborOrder = null;

        // Set the path to an empty list
        path = new List<Vector3>();

        // Set and increment the pawn name
        pawnName = "Pawn" + ++pawnCount;
        //name = pawnName;

        // Set the pawn speed
        pawnSpeed = 2.0f;

        // Set the pawn to not be assigned
        isAssigned = false;

        // add this pawn to the pawn list
        hunger = 100;
        if (!PawnList.Contains(this)) PawnList.Add(this);

        // Initialize item dictionary
        items = new Dictionary<string, Item>();

        currentPathExecution = null;
        currentExecution = null;

    }

}
