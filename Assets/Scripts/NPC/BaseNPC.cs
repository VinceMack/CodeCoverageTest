using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : SaveableEntity
{
    public NPCStats stats = new NPCStats();

    public NPCStats GetNPCStats()
    {
        return stats;
    }

    public override void SaveMyData()
    {
        SaveData<NPCStats>(stats);
    }

    public override void LoadMyData()
    {
        stats = LoadData<NPCStats>();
        // Do whatever we want with stats
        // I.e. change transform.position, update any components, etc.
    }
}
