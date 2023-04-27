using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berries : Item
{
    int foodValue = 10;

    void Awake()
    {
        isGatherable = true;
        isPlaceable = true;
        itemName = "Berries";
    }
}



