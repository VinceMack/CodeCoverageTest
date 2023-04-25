using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockResource : Item
{
    void Awake()
    {
        isGatherable = true;
        itemName = "RockResource";
    }
}
