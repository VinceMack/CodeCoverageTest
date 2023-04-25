using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : Item
{
    // list of plantResources 
    public static List<Bush> plantResources = new List<Bush>();

    [SerializeField] public int resourceCountRef;

    public static readonly int FULL_RESOURCE_THRESHOLD = 25;
    public static readonly int MAX_RESOURCE = 25;
    public int resourceCount;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        isPlaceable = true;
        isDeconstructable = true;
        isForageable = false;
        resourceCount = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

        resourceCountRef = resourceCount;

        itemName = "Bush";

        // Add this plant to the list of plantResources
        plantResources.Add(this);
    }

    void OnDestroy()
    {
        // Remove this plant from the list of plantResources
        plantResources.Remove(this);
    }

    // Static method to increment berries for all plantResources
    public static void IncrementAllResources(int incAmount)
    {
        foreach (Bush plant in plantResources)
        {
            if(plant.isItemized){
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
                plant.spriteRenderer.sprite = Resources.Load<Sprite>("sprites/bush_full");
                plant.isForageable = true;
            }
            else
            {
                plant.spriteRenderer.sprite = Resources.Load<Sprite>("sprites/bush_empty");
                plant.isForageable = false;
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
        int temp = resourceCount;
        resourceCount = 0;
        if (resourceCount >= FULL_RESOURCE_THRESHOLD)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("sprites/bush_full");
            isForageable = true;
        }
        else
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("sprites/bush_empty");
            isForageable = false;
        }
        resourceCountRef = resourceCount;
        return temp;
    }
}
