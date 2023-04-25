using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berries : Item
{
    void Awake()
    {
        isGatherable = true;
        isPlaceable = true;
        itemName = "Berries";
    }
}
