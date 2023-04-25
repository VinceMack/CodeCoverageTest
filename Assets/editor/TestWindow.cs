// TestWindow.cs
using UnityEditor;
using UnityEngine;

public class TestWindow : EditorWindow
{
    [MenuItem("Window/Test Functions")]
    public static void ShowWindow()
    {
        GetWindow<TestWindow>("Test Functions");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Labor Order Info", EditorStyles.boldLabel);

        EditorGUILayout.BeginVertical();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Total Labor Orders:", GUILayout.Width(130));
        EditorGUILayout.LabelField(LaborOrderManager_VM.GetLaborOrderCount().ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Working Pawns:", GUILayout.Width(130));
        EditorGUILayout.LabelField(LaborOrderManager_VM.GetWorkingPawnCount().ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Available Pawns:", GUILayout.Width(130));
        EditorGUILayout.LabelField(LaborOrderManager_VM.GetAvailablePawnCount().ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Populate Object Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrders();
        }

        if (GUILayout.Button("Populate Object Labor Orders (UPDATED)"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersUpdated();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Labor Orders by Type", EditorStyles.boldLabel);

        if (GUILayout.Button("Populate Mineable Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersMineable();
        }

        if (GUILayout.Button("Populate Plantcuttable Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersPlantcuttable();
        }

        if (GUILayout.Button("Populate Forageable Bushes Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersForageableBushes();
        }

        if (GUILayout.Button("Populate Forageable Trees Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersForageableTrees();
        }

        if (GUILayout.Button("Populate Gatherable Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersGatherable();
        }

        if (GUILayout.Button("Populate Deconstructable Labor Orders"))
        {
            LaborOrderManager_VM.PopulateObjectLaborOrdersDeconstructable();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Multiple Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Wheat Plants"))
        {
            GridManager.PopulateWithWheatPlants();
        }

        if (GUILayout.Button("Bushes"))
        {
            GridManager.PopulateWithBushes();
        }

        if (GUILayout.Button("Trees"))
        {
            GridManager.PopulateWithTrees();
        }

        if (GUILayout.Button("Rocks"))
        {
            GridManager.PopulateWithRocks();
        }

        EditorGUILayout.LabelField("Populate Object", EditorStyles.boldLabel);

        if (GUILayout.Button("Wheat Plant"))
        {
            GridManager.PopulateWithWheatPlant();
        }

        if (GUILayout.Button("Bush"))
        {
            GridManager.PopulateWithBush();
        }

        if (GUILayout.Button("Tree"))
        {
            GridManager.PopulateWithTree();
        }

        if (GUILayout.Button("Rock"))
        {
            GridManager.PopulateWithRock();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Generate Orders", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Place Order (ErrorObject)"))
        {
            GameObject testObj = Resources.Load("prefabs/items/ErrorObject") as GameObject;
            LaborOrderManager_VM.AddPlaceLaborOrder(testObj);
        }

        if (GUILayout.Button("Clear Labor Orders"))
        {
            LaborOrderManager_VM.ClearLaborOrders();
        }

        if (GUILayout.Button("Generate Deconstruct Order"))
        {
            LaborOrderManager_VM.AddDeconstructLaborOrder();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Chest Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Spawn Chest"))
        {
            GridManager.PopulateWithChest();
        }

        if (GUILayout.Button("Add ErrorObject to Random Chest"))
        {
            Chest_VM randomChest = GlobalStorage_VM.GetRandomChest();
            if (randomChest != null)
            {
                GameObject errorObject = Resources.Load("prefabs/items/ErrorObject") as GameObject;
                randomChest.AddItem(errorObject);
                Debug.Log("ErrorObject added to a random chest.");
            }
            else
            {
                Debug.Log("No chests available.");
            }
        }
    }
}
