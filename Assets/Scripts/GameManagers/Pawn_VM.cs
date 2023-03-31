using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Pawn_VM : MonoBehaviour
{
    [SerializeField]
    protected List<LaborType>[] LaborTypePriority;        // Priority list for different types of labor
    protected LaborOrder_Base_VM currentLaborOrder;       // Reference to the current labor order
    protected bool isAssigned;                            // Indicates if the pawn is currently assigned to a labor order
    protected static int pawnCount = 0;                   // Counter for the total number of pawns
    protected const int NUM_OF_PRIORITY_LEVELS = 4;       // Number of priority levels for labor types
    protected List<Vector3> path;                         // List of positions for the pawn to follow
    protected float pawnSpeed = 1f;                       // Speed of the pawn movement
    protected Vector3Int position;                        // Current position of the pawn in the grid

    protected string pawnName;                            // Name of the pawn

    // pawn constructor - NOT CALLED WHEN SPAWNED AS SCRIPTABLE OBJECT, WHICH WE DO - SO WE NEED TO INITIALIZE IN THE START METHOD
    public Pawn_VM()
    {
        // Initialize the priority list for different types of labor
        LaborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            LaborTypePriority[i] = new List<LaborType>();
        }

        // Initialize the path list
        path = new List<Vector3>();

        // Initialize the pawn name
        pawnName = "Pawn " + pawnCount;
        pawnCount++;
    }

    // Sets the path for the pawn
    public void SetPath(List<Vector3> path)
    {
        this.path = path;
    }

    // Get the path for the pawn
    public List<Vector3> GetPath()
    {
        return path;
    }

    // Prints the path for debugging purposes
    public void PrintPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(pawnName + " path[" + i + "]: " + path[i]);
        }
    }

    // Moves the given labor type to the specified priority level
    public void MoveLaborTypeToPriorityLevel(LaborType laborType, int priorityLevel)
    {
        for (int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++)
        {
            if (LaborTypePriority[i].Contains(laborType))
            {
                LaborTypePriority[i].Remove(laborType);
            }
        }

        LaborTypePriority[priorityLevel].Add(laborType);
        LaborTypePriority[priorityLevel].Sort();
    }

    // Sets the name of the pawn
    public void SetPawnName(string pawnName)
    {
        this.pawnName = pawnName;
    }

    // Gets the name of the pawn
    public string GetPawnName()
    {
        return pawnName;
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
        TileBase tile = GridManager.tileMap.GetTile(currentLaborOrder.GetLaborOrderLocation());
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
        if(isAssigned)
        {
            Debug.LogError("Pawn is already assigned to a labor order");
        }else
        {
            currentLaborOrder = LaborOrder_Base_VM;
            isAssigned = true;
        }
    }

    // Gets the current labor type priority list
    public List<LaborType>[] GetLaborTypePriority()
    {
        return LaborTypePriority;
    }

    // Coroutine to complete the current labor order
    protected IEnumerator CompleteCurrentLaborOrder()
    {
        TileBase foundTile = GetLaborOrderTileFromTilemap();
        TileBase currentTile = GetPawnTileFromTilemap();

        if (foundTile == null)
        {
            Debug.LogError("foundTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.AddPawn(this);
            yield break;
        }

        if (currentTile == null)
        {
            Debug.LogError("currentTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.AddPawn(this);
            yield break;
        }

        BaseTile_VM tarGet = (BaseTile_VM)foundTile;
        BaseTile_VM current = (BaseTile_VM)currentTile;

        Vector3Int x = Vector3Int.FloorToInt(tarGet.GetPosition());
        Vector3Int y = Vector3Int.FloorToInt(current.GetPosition());

        SetPath(PathfindingManager.GetPath(x, y));

        yield return StartCoroutine(TakePath());

        yield return StartCoroutine(currentLaborOrder.Execute(this));

        Debug.Log($"{pawnName,-10} COMPLETED Labor Order #{currentLaborOrder.GetOrderNumber(),-5} TTC: {currentLaborOrder.GetTimeToComplete(),-10:F2} {"Order Type: " + currentLaborOrder.GetLaborType(),-25} {tarGet.returnTileInformation(),-80}");

        LaborOrderManager_VM.AddPawn(this);
    }

    // Updates the pawn's position based on the path
    public void UpdateLocation()
    {
        if (path.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, path.Last(), pawnSpeed);
            if (transform.position == path.Last()) path.RemoveAt(path.Count - 1);
        }
    }

    // Updates the pawn's path with a new path
    public void UpdatePath(List<Vector3> newPath)
    {
        path.Clear();
        path = newPath;
    }

    // Coroutine to move the pawn along the path
    protected IEnumerator TakePath()
    {
        int pathIndex = 0;

        while (pathIndex < path.Count)
        {
            Vector3Int cellPosition = GridManager.grid.WorldToCell(path[pathIndex]);
            Vector3 centeredWorldPosition = GridManager.grid.GetCellCenterWorld(cellPosition);

            transform.position = Vector3.MoveTowards(transform.position, centeredWorldPosition, pawnSpeed);

            if (transform.position == centeredWorldPosition)
            {
                pathIndex++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return null;
            }
        }
    }

    // Initialization function for the labor type priority list
    public void InitializeLaborTypePriority()
    {
        LaborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];

        // Assign random priority levels to labor types
        foreach (LaborType laborType in System.Enum.GetValues(typeof(LaborType)))
        {
            int randomPriorityLevel = Random.Range(0, NUM_OF_PRIORITY_LEVELS);
            if (LaborTypePriority[randomPriorityLevel] == null)
            {
                LaborTypePriority[randomPriorityLevel] = new List<LaborType>();
            }
            LaborTypePriority[randomPriorityLevel].Add(laborType);
        }
    }


    // Initialization function
    void Start()
    {
        // Initialize the labor type priority list
        InitializeLaborTypePriority();

        // Set initial position to the center of the cell at the origin
        transform.position = GridManager.grid.GetCellCenterWorld(Vector3Int.zero);

        currentLaborOrder = new LaborOrder_Base_VM();

        // Set and increment the pawn name
        pawnName = "Pawn" + ++pawnCount;
        name = pawnName;

        isAssigned = false;
    }

    // Update function for handling logic
    void Update()
    {
        if (isAssigned)
        {
            StartCoroutine(CompleteCurrentLaborOrder());
            isAssigned = false;
        }
        else
        {
        }
    }

    // FixedUpdate function for handling physics updates
    void FixedUpdate()
    {
        if (isAssigned)
        {
            UpdateLocation();
        }
        else
        {
        }
    }
}
