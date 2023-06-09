using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LaborOrderPanelManager : MonoBehaviour
{

    // Containers for pawns and buttons
    public GameObject buttonContainer;
    public GameObject pawnContainer;
    public GameObject pawnNameContainer;
    public GameObject LaborNameContainer;

    // Prefabs for display.
    public GameObject button_prefab;
    public GameObject pawnText_prefab;
    public GameObject LaborText_prefab;

    // For formatting panel.
    public static GameObject content;
    private static float spacing;

    // Panel text objects.
    public GameObject[] laborTypeNames;

    [ContextMenu("TestInit")]
    public void InitializeLaborOrderPanel()
    {

        buttonContainer = GameObject.Find("Buttons");
        pawnNameContainer = GameObject.Find("PawnNames");
        LaborNameContainer = GameObject.Find("LaborTypeNames");
        pawnContainer = GameObject.Find("/GameManager/Pawns");

        button_prefab = Resources.Load("prefabs/ui/pawnButton_LaborOrder") as GameObject;
        pawnText_prefab = Resources.Load("prefabs/ui/pawnText_LaborOrder") as GameObject;
        LaborText_prefab = Resources.Load("prefabs/ui/laborText_LaborOrder") as GameObject;

        content = GameObject.Find("LaborOrderContent");

        AdjustGridLayout();

        laborTypeNames = new GameObject[LaborOrderManager.GetLaborTypesCount()];

        // Initialize labor order panel
        for (int i = 0; i < LaborOrderManager.GetLaborTypesCount(); i++)
        {
            // Create text object for name
            GameObject newTextObject = Instantiate(LaborText_prefab, LaborNameContainer.transform);
            newTextObject.name = LaborOrderManager.GetLaborTypeName(i);

            // Change new text name
            TMP_Text newText = newTextObject.GetComponent<TMP_Text>();
            newText.text = LaborOrderManager.GetLaborTypeName(i);
            laborTypeNames[i] = newTextObject;
        }

        List<GameObject> pawnList = new List<GameObject>();

        foreach (Transform pawn in pawnContainer.transform)
        {
            pawnList.Add(pawn.gameObject);
        }

        for (int i = 0; i < pawnList.Count; i++)
        {
            AddPawnButtons(pawnList[i]);
        }

    }

    // Add buttons for the addition of a pawn.
    public void AddPawnButtons(GameObject pawn)
    {

        // Add pawn text.
        Pawn pawnComponent = pawn.GetComponent<Pawn>();
        GameObject newTextObject = Instantiate(pawnText_prefab, pawnNameContainer.transform);
        newTextObject.name = pawnComponent.GetPawnName() + " (Text)";

        TMP_Text newText = newTextObject.GetComponent<TMP_Text>();
        newText.text = pawnComponent.GetPawnName();

        // Add pawn buttons.
        for (int i = 0; i < LaborOrderManager.GetLaborTypesCount(); i++)
        {
            // Create button and adjust component.
            GameObject newButton = Instantiate(button_prefab, buttonContainer.transform);
            newButton.name = pawnComponent.GetPawnName() + ": " + LaborOrderManager.GetLaborTypeName(i);
            PawnButton buttonComponent = newButton.GetComponent<PawnButton>();

            buttonComponent.pawn = pawnComponent;
            buttonComponent.labor = LaborOrderManager.GetLaborType(LaborOrderManager.GetLaborTypeName(i));
            buttonComponent.InitializePawnButton();

        }

        // Resize content.
        var rectTransform = content.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + spacing);

    }

    // Remove buttons for the removal of a pawn.
    public void RemovePawnButtons(GameObject pawn)
    {
        // Remove pawn name.
        Pawn pawnComponent = pawn.GetComponent<Pawn>();
        GameObject pawnNameObj = GameObject.Find(pawnComponent.GetPawnName() + " (Text)");
        Destroy(pawnNameObj);

        // Remove pawn buttons.
        for (int i = 0; i < LaborOrderManager.GetLaborTypesCount(); i++)
        {
            GameObject buttonObj = GameObject.Find(pawnComponent.GetPawnName() + ": " + LaborOrderManager.GetLaborTypeName(i));
            Destroy(buttonObj);
        }

        // Resize content.
        var rectTransform = content.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - spacing);

    }

    // Make adjustments for layout.
    public void AdjustGridLayout()
    {

        // Adjust button layout.
        GridLayoutGroup buttonGrid = buttonContainer.GetComponent<GridLayoutGroup>();
        buttonGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        buttonGrid.constraintCount = LaborOrderManager.GetLaborTypesCount();

        // Adjust pawn name layout.
        GridLayoutGroup pawnNameGrid = pawnNameContainer.GetComponent<GridLayoutGroup>();
        pawnNameGrid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        pawnNameGrid.constraintCount = 1;

        // Adjust labor name layout.
        GridLayoutGroup laborNameGrid = LaborNameContainer.GetComponent<GridLayoutGroup>();
        laborNameGrid.constraint = GridLayoutGroup.Constraint.FixedRowCount;
        laborNameGrid.constraintCount = LaborOrderManager.GetLaborTypesCount();

        var rectTransform = content.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 0);

        spacing = pawnNameGrid.spacing.y + pawnNameGrid.cellSize.y;

    }

}




