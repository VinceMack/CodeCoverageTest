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
        EditorGUILayout.LabelField(LaborOrderManager.GetLaborOrderCount().ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Working Pawns:", GUILayout.Width(130));
        EditorGUILayout.LabelField(LaborOrderManager.GetWorkingPawnCount().ToString());
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Available Pawns:", GUILayout.Width(130));
        EditorGUILayout.LabelField(LaborOrderManager.GetAvailablePawnCount().ToString());
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Clear Stairs"))
        {
            GridManager.ClearStairs();
        }


        if (GUILayout.Button("Populate Object Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrders();
        }

        if (GUILayout.Button("Populate Object Labor Orders (UPDATED)"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersUpdated();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Labor Orders by Type", EditorStyles.boldLabel);

        if (GUILayout.Button("Populate Mineable Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersMineable();
        }

        if (GUILayout.Button("Populate Plantcuttable Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersPlantcuttable();
        }

        if (GUILayout.Button("Populate Forageable Bushes Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersForageableBushes();
        }

        if (GUILayout.Button("Populate Forageable Trees Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersForageableTrees();
        }

        if (GUILayout.Button("Populate Gatherable Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersGatherable();
        }

        if (GUILayout.Button("Populate Deconstructable Labor Orders"))
        {
            LaborOrderManager.PopulateObjectLaborOrdersDeconstructable();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Populate Multiple Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Wheat Plants"))
        {
            GridManager.PopulateWithWheatPlants();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Bushes"))
        {
            GridManager.PopulateWithBushes();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Trees"))
        {
            GridManager.PopulateWithTrees();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Rocks"))
        {
            GridManager.PopulateWithRocks();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Rocks (Perlin)"))
        {
            GridManager.PopulateWithRocksPerlin();
            GridManager.ClearStairs();
        }

        EditorGUILayout.LabelField("Populate Object", EditorStyles.boldLabel);

        if (GUILayout.Button("Wheat Plant"))
        {
            GridManager.PopulateWithWheatPlant();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Bush"))
        {
            GridManager.PopulateWithBush();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Tree"))
        {
            GridManager.PopulateWithTree();
            GridManager.ClearStairs();
        }

        if (GUILayout.Button("Rock"))
        {
            GridManager.PopulateWithRock();
            GridManager.ClearStairs();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Generate Orders", EditorStyles.boldLabel);

        if (GUILayout.Button("Generate Place Order (ErrorObject)"))
        {
            Item testObj = Resources.Load<GameObject>("prefabs/items/ErrorObject").GetComponent<Item>();
            LaborOrderManager.AddPlaceLaborOrder(testObj);
        }

        if (GUILayout.Button("Clear Labor Orders"))
        {
            LaborOrderManager.ClearLaborOrders();
        }

        if (GUILayout.Button("Generate Deconstruct Order"))
        {
            LaborOrderManager.AddDeconstructLaborOrder();
        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Chest Actions", EditorStyles.boldLabel);

        if (GUILayout.Button("Spawn Chest"))
        {
            GridManager.PopulateWithChest();
        }

        if (GUILayout.Button("Add ErrorObject to Random Chest"))
        {
            Chest randomChest = GlobalStorage.GetRandomChest();
            if (randomChest != null)
            {
                Item errorObject = Resources.Load<GameObject>("prefabs/items/Berries").GetComponent<Item>();
                randomChest.AddItem(errorObject.itemName);
                Debug.Log("ErrorObject added to a random chest.");
            }
            else
            {
                Debug.Log("No chests available.");
            }
        }
    }
}



