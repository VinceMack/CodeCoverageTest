using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockResource : Item
{
    public override void Awake()
    {
        isGatherable = true;
        itemName = "RockResource";
    }
}




