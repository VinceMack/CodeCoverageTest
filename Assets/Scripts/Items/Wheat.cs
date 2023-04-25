using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheat : Item
{
    // list of plantResources 
    public static List<Wheat> plantResources = new List<Wheat>();

    [SerializeField] public int resourceCountRef;

    public static readonly int FULL_RESOURCE_THRESHOLD = 25;
    public static readonly int MAX_RESOURCE = 25;
    public int resourceCount;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        isPlantcuttable = false;
        isDeconstructable = true;
        resourceCount = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

        resourceCountRef = resourceCount;

        // Add this plant to the list of plantResources
        plantResources.Add(this);

        itemName = "Wheat";
    }

    void OnDestroy()
    {
        // Remove this plant from the list of plantResources
        plantResources.Remove(this);
    }

    // Static method to increment berries for all plantResources
    public static void IncrementAllResources(int incAmount)
    {
        foreach (Wheat plant in plantResources)
        {
            if(plant.isItemized == true){
                continue;
            }

            // null check spriteRenderer
            if (plant.spriteRenderer == null)
            {
                Debug.Log("spriteRenderer is null");
                return;
            }

            if (plant.resourceCount < MAX_RESOURCE)
            {
                plant.resourceCount = plant.resourceCount + incAmount;

                if (plant.resourceCount > MAX_RESOURCE)
                {
                    plant.resourceCount = MAX_RESOURCE;
                }
            }

            if (plant.resourceCount >= FULL_RESOURCE_THRESHOLD)
            {
                plant.spriteRenderer.sprite = Resources.Load<Sprite>("sprites/wheat");
                plant.isPlantcuttable = true;
            }
            else
            {
                plant.spriteRenderer.sprite = Resources.Load<Sprite>("sprites/seeds");
                plant.isPlantcuttable = false;
            }

            plant.resourceCountRef = plant.resourceCount;
        }
    }

    public new void Deconstruct()
    {
        plantResources.Remove(this);
        Itemize();
    }

    public int Harvest()
    {
        if(isItemized == true){
            return -1;
        }

        int temp = resourceCount;
        resourceCount = 0;
        if (resourceCount >= FULL_RESOURCE_THRESHOLD)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("sprites/wheat");
            isPlantcuttable = true;
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("sprites/seeds");
            isPlantcuttable = false;
        }
        resourceCountRef = resourceCount;
        return temp;
    }
}
