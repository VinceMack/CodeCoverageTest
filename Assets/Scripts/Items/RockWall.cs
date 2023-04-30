using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockWall : Item
{
    public override void Awake()
    {
        isPlaceable = true;
        itemName = "RockWall";
        isCollision = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}




