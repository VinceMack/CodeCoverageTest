using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // list reference to all items
    public static List<Item> items = new List<Item>();

    [SerializeField] public string itemName = "baseItem";
    [SerializeField] public BaseTile location;
    [SerializeField] public bool isGatherable = false;
    [SerializeField] public bool isPlaceable = false;
    [SerializeField] public bool isDeconstructable = false;
    [SerializeField] public bool isWoodcuttable = false;
    [SerializeField] public bool isMineable = false;
    [SerializeField] public bool isForageable = false;
    [SerializeField] public bool isCraftable = false;
    [SerializeField] public bool isPlantcuttable = false;
    [SerializeField] public bool isItemized = false;
    [SerializeField] public bool isCollision = false;
    [SerializeField] public List<Item> requiredForCrafting;

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

    public virtual void Awake()
    {
        foreach (Item item in Resources.LoadAll("prefabs/items", typeof(Item)))
        {
            items.Add(item.GetComponent<Item>());
        }
    }
}




