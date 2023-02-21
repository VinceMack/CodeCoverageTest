using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnableEntity", menuName = "ScriptableObjects/SpawnableEntity", order = 1)]
public class SpawnableEntity : ScriptableObject
{
    public string entityName;
    public string displayName;
    public GameObject prefab;
}
