using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LaborOrderPanelManager : MonoBehaviour
{
    public static GameObject buttonContainer;
    public static GameObject button_prefab;
    public static GameObject[,] buttonList;
    private static int buttonTotal;

    public static void InitializeLaborOrderPanel()
    {
        // Adjust grid layout of buttons
        buttonContainer = GameObject.Find("Buttons");
        button_prefab = Resources.Load("prefabs/LaborOrderPawnButton") as GameObject;
        GridLayoutGroup grid = buttonContainer.GetComponent<GridLayoutGroup>();
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = LaborOrderManager_VM.GetLaborTypesCount();

        buttonList = new GameObject[LaborOrderManager_VM.GetPawnCount(), LaborOrderManager_VM.GetLaborTypesCount()];

    }

    public static void AddButtons()
    {
    
        for (int i = 0; i < LaborOrderManager_VM.GetPawnCount(); i++)
        {
            GameObject pawn = GameObject.Find("Pawn" + (12 + i));
            for (int j = 0; j < LaborOrderManager_VM.GetLaborTypesCount(); j++)
            {
                buttonList[i, j] = Instantiate(button_prefab, GameObject.Find("Buttons").transform);
                buttonList[i, j].name = "Pawn" + i + "_Button" + j;
                buttonList[i, j].GetComponent<PawnButton>().pawn = pawn;
            }
        }

    }

}
