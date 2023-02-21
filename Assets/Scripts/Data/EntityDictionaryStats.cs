using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class EntityDictionaryStats : IStats
{
    public Dictionary<string, string> entitiesInScene = new Dictionary<string, string>();
}
