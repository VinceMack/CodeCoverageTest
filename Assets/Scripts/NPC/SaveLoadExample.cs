using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[RequireComponent(typeof(BaseNPC))]
public class SaveLoadExample : MonoBehaviour
{
    private NPCStats stats;

    private void Start() 
    {
        stats = GetComponent<BaseNPC>().stats;
    }
}
