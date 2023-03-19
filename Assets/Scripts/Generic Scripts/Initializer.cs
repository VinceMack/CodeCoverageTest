using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private PrefabList prefabList;

    // Start is called before the first frame update
    void Awake()
    {
        if(prefabList != null && prefabList.prefabDictionary.Count == 0)
        {
            Debug.Log("Initializing PrefabList");
            prefabList.InitializePrefabDictionary();
        }
    }
}
