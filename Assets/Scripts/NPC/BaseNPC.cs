using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : SaveableEntity
{
    public NPCStats stats = new NPCStats();

    public override void SaveData<T>(T stats)
    {
        base.SaveData<NPCStats>(this.stats);
    }

    public override T LoadData<T>()
    {
        this.stats = base.LoadData<NPCStats>();
        return base.LoadData<T>();
    }

    public override void SaveMyData()
    {
        SaveData<NPCStats>(stats);
    }
}
