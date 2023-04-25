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
        foreach (SpawnableEntity entity in prefabList)
        {
            if(prefabDictionary.ContainsKey(entity.entityName))
            {
                Debug.LogWarning("ERROR: Do not create multiple 'SpawnableEntity's with the same entityName." + entity.entityName);
                break;
            }
            //if(!entity.prefab.GetComponent<SaveableEntity>())
            //{
            //    Debug.LogWarning("WARNING: Prefab in Prefab List detected to not have a Saveable Entity component attached.");
            //}
            prefabDictionary.Add(entity.entityName, entity);
        }
    }
}
