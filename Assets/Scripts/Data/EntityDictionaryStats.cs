using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityDictionaryStats : IStats
{
    public Dictionary<string, string> entitiesInScene = new Dictionary<string, string>();
}
