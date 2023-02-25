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

    public override void SaveMyData(int saveSlot)
    {
        SaveData<NPCStats>(stats, saveSlot);
    }

    public override void LoadMyData(int saveSlot)
    {
        stats = LoadData<NPCStats>(saveSlot);
        // Do whatever we want with stats
        // I.e. change transform.position, update any components, etc.
    }
}
