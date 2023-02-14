using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectDictionary", menuName = "ScriptableObjects/ObjectDictionary", order = 1)]
public class EntityDictionary : ScriptableObject
{
    // Dictionary where key - guid, value - associated gameobject
    public Dictionary<string, GameObject> objectDictionary;
    public PrefabList prefabList;

    public void InstanitateEntity(string prefabName)
    {

    }
}
