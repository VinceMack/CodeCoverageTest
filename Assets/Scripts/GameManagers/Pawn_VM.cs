using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class Pawn_VM : MonoBehaviour
{
    [SerializeField]
    private List<LaborType>[] LaborTypePriority;        // Priority list for different types of labor
    private LaborOrder_Base_VM currentLaborOrder;       // Reference to the current labor order
    private bool isAssigned;                            // Indicates if the pawn is currently assigned to a labor order
    private static int pawnCount = 0;                   // Counter for the total number of pawns
    private const int NUM_OF_PRIORITY_LEVELS = 4;       // Number of priority levels for labor types
    private List<Vector3> path;                         // List of positions for the pawn to follow
    private float pawnSpeed = 1f;                       // Speed of the pawn movement
    private Vector3Int position;                        // Current position of the pawn in the grid

    private string pawnName;                            // Name of the pawn

    // Sets the path for the pawn
    public void setPath(List<Vector3> path)
    {
        this.path = path;
    }

    // Prints the path for debugging purposes
    public void printPath()
    {
        for (int i = 0; i < path.Count; i++)
        {
            Debug.Log(pawnName + " path[" + i + "]: " + path[i]);
        }
    }

    // Moves the given labor type to the specified priority level
    public void moveLaborTypeToPriorityLevel(LaborType laborType, int priorityLevel)
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
    public void setPawnName(string pawnName)
    {
        this.pawnName = pawnName;
    }

    // Gets the name of the pawn
    public string getPawnName()
    {
        return pawnName;
    }

    // Gets the tile the pawn is currently on from the tilemap
    public TileBase getPawnTileFromTilemap()
    {
        TileBase tile = GridManager.tileMap.GetTile(GridManager.tileMap.WorldToCell(transform.position));
        if (tile == null)
        {
            Debug.LogError(pawnName + " found a null tile at current location - getPawnTileFromTilemap");
            return null;
        }
        return tile;
    }

    // Gets the tile of the current labor order from the tilemap
    public TileBase getLaborOrderTileFromTilemap()
    {
        TileBase tile = GridManager.tileMap.GetTile(currentLaborOrder.getLaborOrderLocation());
        if (tile == null)
        {
            Debug.LogError(pawnName + " found a null tile at current location - getLaborOrderTileFromTilemap");
            return null;
        }
        return tile;
    }

    // Sets the current labor order for the pawn
    public void setCurrentLaborOrder(LaborOrder_Base_VM LaborOrder_Base_VM)
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
    public List<LaborType>[] getLaborTypePriority()
    {
        return LaborTypePriority;
    }

    // Coroutine to complete the current labor order
    private IEnumerator completeCurrentLaborOrder()
    {
        TileBase foundTile = getLaborOrderTileFromTilemap();
        TileBase currentTile = getPawnTileFromTilemap();

        if (foundTile == null)
        {
            Debug.LogError("foundTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.addPawn(this);
            yield break;
        }

        if (currentTile == null)
        {
            Debug.LogError("currentTile is null; adding pawn back to labor order manager; breaking out of coroutine");
            LaborOrderManager_VM.addPawn(this);
            yield break;
        }

        BaseTile_VM target = (BaseTile_VM)foundTile;
        BaseTile_VM current = (BaseTile_VM)currentTile;

        Vector3Int x = Vector3Int.FloorToInt(target.getPosition());
        Vector3Int y = Vector3Int.FloorToInt(current.getPosition());

        setPath(PathfindingManager.getPath(x, y));

        yield return StartCoroutine(takePath());

        yield return StartCoroutine(currentLaborOrder.execute(this));

        Debug.Log($"{pawnName,-10} COMPLETED Labor Order #{currentLaborOrder.getOrderNumber(),-5} TTC: {currentLaborOrder.getTimeToComplete(),-10:F2} {"Order Type: " + currentLaborOrder.getLaborType(),-50} {target.returnTileInformation(),-80}");

        LaborOrderManager_VM.addPawn(this);
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
    private IEnumerator takePath()
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

    // Initialization function
    void Start()
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
            StartCoroutine(completeCurrentLaborOrder());
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
