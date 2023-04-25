using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorObject : Item
{

    void Awake()
    {
        isGatherable = true;
        isPlaceable = true;
        itemName = "ErrorObject";
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
