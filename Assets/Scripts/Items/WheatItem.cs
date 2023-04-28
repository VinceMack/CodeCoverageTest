using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatItem : Item
{
    public override void Awake()
    {
        isGatherable = true;
        itemName = "WheatResource";
    }
}




