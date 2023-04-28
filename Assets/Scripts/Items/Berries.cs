using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berries : Item
{

    public static readonly float foodValue = Pawn.MAX_HUNGER * 0.10f;

    void Awake()
    {
        isGatherable = true;
        isPlaceable = true;
        itemName = "Berries";
    }
}




