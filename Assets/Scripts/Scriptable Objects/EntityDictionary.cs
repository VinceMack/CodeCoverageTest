using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityDictionary", menuName = "ScriptableObjects/EntityDictionary", order = 1)]
public class EntityDictionary : ScriptableObject
{
    // Dictionary where key - guid, value - associated gameobject
    public Dictionary<string, GameObject> entityDictionary = new Dictionary<string, GameObject>();
    public PrefabList prefabList;

    public GameObject InstanitateEntity(string prefabName, string guid = "", Vector3 position = new Vector3(), Vector3 rotation = new Vector3())
    {
        GameObject toClone = prefabList.prefabDictionary[prefabName].prefab;

        if(toClone == null)
        {
            return null;
        }

        GameObject clone = Instantiate(toClone, position, Quaternion.Euler(rotation.x, rotation.y, rotation.z));
        SaveableEntity saveEntity = clone.GetComponent<SaveableEntity>();
        if(saveEntity != null)
        {
            saveEntity.GenerateId(guid);
            entityDictionary.Add(saveEntity.Id, clone);
            saveEntity.SetPrefabName(prefabName);
        }
        return clone;
    }

    public void BuildSceneFromStats(EntityDictionaryStats stats)
    {
        foreach(KeyValuePair<string, string> kvp in stats.entitiesInScene)
        {
            InstanitateEntity(kvp.Value, kvp.Key);   
        }
    }
}
