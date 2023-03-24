using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseNPC
{
	[SerializeField]
    private List<LaborType>[] LaborTypePriority;    // required for LaborOrderManager labor order assignment logic
    private LaborOrder currentLaborOrder;           // set by LaborOrderManager
    private bool isAssigned;         // set to true when the pawn is assigned to a labor order, set to false when the pawn completes the labor order, controls Update() logic
    private bool isWorking;
    //private Mutex assignmentLock = new Mutex();

    private string pawnName;

    private const int NUM_OF_PRIORITY_LEVELS = 4;

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
        if(!isWorking)
        {
            currentLaborOrder = laborOrder;
            isAssigned = true;
        }
    }

    // get the current labor type priorities of the pawn
    public List<LaborType>[] getLaborTypePriority() {
        return LaborTypePriority;
    }

    // coroutine to complete labor order by waiting for the time to complete
    private IEnumerator completeCurrentLaborOrder() {
        isAssigned = false;
        isWorking = true;

        // wait the time to complete the labor order
        yield return StartCoroutine(currentLaborOrder.execute());


        // debug print the pawn name and the labor type and the labor number and time to complete
        //Debug.Log($"{pawnName,-10} completed {currentLaborOrder.getLaborType(),-10} {currentLaborOrder.getOrderNumber(),-10} in {currentLaborOrder.getTimeToComplete(),-5:F2} seconds");


        // stop the coroutine
        isWorking = false;
        StopCoroutine(completeCurrentLaborOrder());
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

        // initialize the default pawn name to null
        pawnName = "Pawn" + GetInstanceID();
        name = pawnName;

        // initialize the default isWorking to false
        isAssigned = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAssigned && !isWorking)
        {
            StartCoroutine(completeCurrentLaborOrder());
        }
        else
        {
            // no order to complete, do nothing
        }
    }
}
