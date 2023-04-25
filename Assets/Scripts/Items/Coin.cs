using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    void Awake()
    {
        isGatherable = true;
        isPlaceable = true;
        itemName = "Coin";
    }
}
