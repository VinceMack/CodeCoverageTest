using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : SaveableEntity
{
    public static List<Bush> BushList = new List<Bush>();   // List of all existing bushes. Used to increment berryCount on a timer.
    public int berryCount = 0;

    private const int MAX_BERRIES = 100;
    private const int FULL_SPRITE_THRESHOLD = 30;
    
    private SpriteRenderer spriteRenderer;
    public Sprite bush_full;
    public Sprite bush_empty;

    // Increment the berries of all bushes
    public static void incrementBerries(int incAmount)
    {
        foreach(Bush b in BushList)
        {
            b.berryCount += incAmount;

            if (b.berryCount > MAX_BERRIES)
            {
                b.berryCount = MAX_BERRIES;
            }

            if(b.berryCount >= FULL_SPRITE_THRESHOLD)
            {
                b.spriteRenderer.sprite = b.bush_full;
            }
            else
            {
                b.spriteRenderer.sprite = b.bush_empty;
            }
        }
    }

    // Destroys this bush object and removes from the list
    public void Destroy()
    {
        BushList.Remove(this);
        GlobalInstance.Instance.entityDictionary.DestroySaveableObject(this);
    }

    // Return the berryCount and set to zero
    public int Harvest()
    {
        int tmp = berryCount;
        berryCount = 0;
        return tmp;
    }

    // Start is called before the first frame update
    void Start()
    {
        BushList.Add(this);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
