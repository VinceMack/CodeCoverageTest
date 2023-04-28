using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodWall : Item
{

    public override void Awake()
    {
        isPlaceable = true;
        itemName = "WoodWall";
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




