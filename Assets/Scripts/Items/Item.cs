using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // list reference to all items
    public static List<GameObject> items = new List<GameObject>();

    [SerializeField] public string itemName;
    [SerializeField] public BaseTile_VM location;
    [SerializeField] public bool isGatherable = false;
    [SerializeField] public bool isPlaceable = false;
    [SerializeField] public bool isDeconstructable = false;
    [SerializeField] public bool isWoodcuttable = false;
    [SerializeField] public bool isMineable = false;
    [SerializeField] public bool isForageable = false;
    [SerializeField] public bool isCraftable = false;
    [SerializeField] public bool isPlantcuttable = false;

    public bool isItemized = false;

    public void Itemize()
    {
        isItemized = true;
        isGatherable = true;
        isPlaceable = true;

        isForageable = false;
        isPlantcuttable = false;
    }

    public void Unitemize()
    {
        isItemized = false;
        isGatherable = false;
        isPlaceable = false;
    }

    public void Deconstruct()
    {
        Itemize();
    }

    void Awake()
    {
        foreach (GameObject item in Resources.LoadAll("prefabs/items", typeof(GameObject)))
        {
            items.Add(item.GetComponent<GameObject>());
        }
    }
}
