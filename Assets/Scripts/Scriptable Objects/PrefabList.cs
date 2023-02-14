using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PrefabList", menuName = "ScriptableObjects/PrefabList", order = 1)]
public class PrefabList : ScriptableObject
{
    // List where 
    public List<SpawnableEntity> prefabList = new List<SpawnableEntity>(); 
    public Dictionary<string, SpawnableEntity> prefabDictionary = new Dictionary<string, SpawnableEntity>();

    public void InitializePrefabDictionary()
    {
        foreach(SpawnableEntity entity in prefabList)
        {
            if(prefabDictionary.ContainsKey(entity.entityName))
            {
                Debug.LogError("ERROR: Do not create multiple SpawnableEntitys with the same entityName.");
                break;
            }
            prefabDictionary.Add(entity.entityName, entity);
        }
    }
}
