using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaborOrderPanelManager : MonoBehaviour
{

    [SerializeField, HideInInspector]
    public GameObject buttonContainer;

    [SerializeField]
    GameObject button_prefab;

    public GameObject[,] buttonList;
    private int buttonTotal;
    private bool completed = false;

    void Start()
    {

        // Adjust grid layout of buttons
        buttonContainer = GameObject.Find("Buttons");
        GridLayoutGroup grid = buttonContainer.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = LaborOrderManager_VM.getNumberOfLaborTypes();

        buttonList = new GameObject[LaborOrderManager_VM.getPawnCount(), LaborOrderManager_VM.getNumberOfLaborTypes()];

    }

    private void addButtons()
    {
    
        for (int i = 0; i < LaborOrderManager_VM.getPawnCount(); i++)
        {
            GameObject pawn = GameObject.Find("Pawn" + (i+1));
            for (int j = 0; j < LaborOrderManager_VM.getNumberOfLaborTypes(); j++)
            {
                buttonList[i, j] = Instantiate(button_prefab, transform.Find("Buttons"));
                buttonList[i, j].name = "Pawn" + i + "_Button" + j;
                buttonList[i, j].GetComponent<PawnButton>().pawn = pawn;
            }
        }

    }


    void Update()
    {
        // Ensures that all pawns have been created before adding buttons
        if (GameObject.Find("Pawn" + LaborOrderManager_VM.getPawnCount()) != null && !completed) 
        {
            addButtons();
            completed = true;
        }
    }
    
}
