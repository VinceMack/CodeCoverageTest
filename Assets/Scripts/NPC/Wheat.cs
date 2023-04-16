using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Wheat : SaveableEntity
{
    public static List<Wheat> WheatList = new List<Wheat>();   // List of all existing wheat. Used to increment wheat growth on a timer.
    public int growth = 0;

    private const int MAX_GROWTH = 100;
    private const int SPROUT_THRESHOLD = 30;

    private SpriteRenderer spriteRenderer;
    public Sprite wheat_sprite_seed;
    public Sprite wheat_sprite_sprout;
    public Sprite wheat_sprite_harvestable;

    // Increment the berries of all bushes
    public static void growWheat(int incAmount)
    {
        foreach(Wheat w in WheatList)
        {
            // create labor order if became fully grown
            if(w.growth < MAX_GROWTH && w.growth + incAmount >= MAX_GROWTH)
            {
                LaborOrderManager_VM.AddLaborOrder(new LaborOrder_Forage(w.gameObject, false));
            }

            w.growth += incAmount;

            if (w.growth > MAX_GROWTH)
            {
                w.growth = MAX_GROWTH;
            }

            w.UpdateSprite();
        }
    }

    public void UpdateSprite()
    {
        // control sprite based on growth
        if (growth < SPROUT_THRESHOLD)
        {
            spriteRenderer.sprite = wheat_sprite_seed;
        }
        else if (growth < MAX_GROWTH)
        {
            spriteRenderer.sprite = wheat_sprite_sprout;
        }
        else
        {
            spriteRenderer.sprite = wheat_sprite_harvestable;
        }
    }

    // Destroys this object and removes from the list
    public void Destroy()
    {
        WheatList.Remove(this);
        GlobalInstance.Instance.entityDictionary.DestroySaveableObject(this);
    }

    // Destroy this object if fully grown. true if successful, false if not
    public bool Harvest()
    {
        if(growth >= MAX_GROWTH)
        {
            Destroy();
            return true;
        }
        else
        {
            return false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        WheatList.Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSprite();
    }
}
