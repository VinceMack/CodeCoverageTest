using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseNPC
{
	// list all possible pawn careers/labor order types here
	public GameObject pawnObject;
	enum PawnType { storage, forage, generic };
	public string pawnName;
	public string type = "generic";
	public LaborOrder currentOrder = null;
	public bool isTypeExclusive; // forces this pawn to ONLY respond to tasks for their career.
	
	public Pawn(string type, string name, bool isTypeExclusive)
	{
		this.type = type;
		this.pawnName = name;
		this.name = "Pawn";
		this.isTypeExclusive = isTypeExclusive;
		this.currentOrder = null;
	}

	public void rename(string newName)
    {
		this.pawnName = newName;
    }

	public void setExclusive(bool exclusive)
    {
		this.isTypeExclusive = exclusive;
    }
	

	public void startLaborOrder()
    {
		// based on currentOrder assigned by global event queue,
		// pathfind to the location of labor order
		pawnObject.transform.position = currentOrder.targetObject.transform.position; // just teleport for now

		// perform the labor order
		Destroy(currentOrder.targetObject);  // immediately destroys the object for now
		currentOrder = null;
		// when complete, move self from working pawn list to free pawn list

	}
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
