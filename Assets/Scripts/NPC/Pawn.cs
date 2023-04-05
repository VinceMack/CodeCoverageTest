using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pawn : BaseNPC
{
    [SerializeField]
    private List<LaborType>[] LaborTypePriority;            // required for LaborOrderManager labor order assignment logic
    private LaborOrder currentLaborOrder;                   // set by LaborOrderManager
    private bool isAssigned;                                // set to true when the pawn is assigned to a labor order
    private string pawnName;
    private const int NUM_OF_PRIORITY_LEVELS = 4;
    private Coroutine currentExecution;                     // holds a reference to labor order execute() coroutine

    public static List<Pawn> PawnList = new List<Pawn>();   // a list of all living pawns
    public bool refuseLaborOrders = false;                  // prevents this pawn from being assigned labor orders, redundant for now but may be useful later
    public int hunger = 100;

    // Decrement the hunger for all living pawns
    public static void decrementHunger(int decAmount)
    {
        for (int i = Pawn.PawnList.Count - 1; i >= 0; i--)
        {
            Pawn.PawnList[i].hunger -= decAmount;
            if (Pawn.PawnList[i].hunger <= 0)
            {
                Pawn.PawnList[i].hunger = 0;
                Pawn.PawnList[i].Die("has starved to death.");
            }
        }
    }


    public void moveLaborTypeToPriorityLevel(LaborType laborType, int priorityLevel) { // take a labor type and move it to the X priority level
        
        // iterate through the labor type priorities and remove the labor type from the list
        for(int i = 0; i < NUM_OF_PRIORITY_LEVELS; i++) {
            if(LaborTypePriority[i].Contains(laborType)) {
                LaborTypePriority[i].Remove(laborType);
            }
        }

        // add the labor type to the priority level
        LaborTypePriority[priorityLevel].Add(laborType);
        LaborTypePriority[priorityLevel].Sort();
    }

    // set the pawnName of the pawn
    public void setPawnName(string pawnName) {
        this.pawnName = pawnName;
        name = pawnName;
    }

	public string getPawnName() {
		return pawnName;
	}

    // set the current labor order of the pawn and indicate that the pawn is assigned to a labor order
    public void setCurrentLaborOrder(LaborOrder laborOrder) {
        currentLaborOrder = laborOrder;
        isAssigned = true;
    }

    // get the current labor type priorities of the pawn
    public List<LaborType>[] getLaborTypePriority() {
        return LaborTypePriority;
    }
    
    // coroutine to complete labor order by waiting for the time to complete
    private IEnumerator completeCurrentLaborOrder() {
        isAssigned = false;

        // wait the time to complete the labor order
        currentExecution = StartCoroutine(currentLaborOrder.execute());
        yield return currentExecution;


        // debug print the pawn name and the labor type and the labor number and time to complete
        //Debug.Log($"{pawnName,-10} completed {currentLaborOrder.getLaborType(),-10} {currentLaborOrder.getOrderNumber(),-10} in {currentLaborOrder.getTimeToComplete(),-5:F2} seconds");

        // add the pawn back to the queue of available pawns
        LaborOrderManager.addPawn(this); // needs updated
    }

    // cancels the current labor order
    public void cancelCurrentLaborOrder()
    {
        if(currentExecution != null)
            StopCoroutine(currentExecution);
    }

    // kills the pawn. Cancels labor order and removes them from appropriate lists.
    public override void Die() { Die("has died."); }
    public void Die(string cause)
    {
        PawnList.Remove(this);
        LaborOrderManager.removeSpecificPawn(this);
        cancelCurrentLaborOrder();
        refuseLaborOrders = true;
        Debug.Log(getPawnName() + " " + cause);
        //base.Die();
        GlobalInstance.Instance.entityDictionary.DestroySaveableObject(this);
        // maybe check if PawnList is empty to initiate the Game Lose here
    }

    // Start is called before the first frame update
    void Awake()
    {   
        // initialize the array of lists
        LaborTypePriority = new List<LaborType>[NUM_OF_PRIORITY_LEVELS];

        // iterate through the labor types and add them to the list at random priority levels
        foreach(LaborType laborType in System.Enum.GetValues(typeof(LaborType))) {
            int randomPriorityLevel = Random.Range(0, NUM_OF_PRIORITY_LEVELS);
            if(LaborTypePriority[randomPriorityLevel] == null) {
                LaborTypePriority[randomPriorityLevel] = new List<LaborType>();
            }
            LaborTypePriority[randomPriorityLevel].Add(laborType);
        }

        // initialize the default current labor order
        currentLaborOrder = /*new LaborOrder();*/ null;

        // initialize pawn name
        pawnName = "Pawn" + GetInstanceID();
        name = pawnName;

        // initialize the default isAssigned to false
        isAssigned = false;

        // add this pawn to the pawn list
        PawnList.Add(this);
        LaborOrderManager.addPawn(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (isAssigned)
        {
            StartCoroutine(completeCurrentLaborOrder());
        }
        else
        {
            // no order to complete, do nothing
        }
    }
}
