using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheatItem : Item
{
    void Awake()
    {
        isGatherable = true;
        itemName = "WheatResource";
    }
}
