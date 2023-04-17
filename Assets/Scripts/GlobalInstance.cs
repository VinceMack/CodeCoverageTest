using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: Do not abuse this class. It is a singleton, can have some issues with creation timing, and should not be
// used to store large amounts of data. (It currently is being used just to reference data elsewhere [pointer-like])

public class GlobalInstance : MonoBehaviour
{
    // "Global" instance
    public static GlobalInstance Instance;

    // References
    public PrefabList prefabList;
    public EntityDictionary entityDictionary;
    public GameObject sceneCamera;
    public GlobalStorage gs;

    private void Awake()
    {
        // If Instance is not null (any time after the first time)
        // AND
        // If Instance is not 'this' (after the first time)
        if (Instance != null && Instance != this)
        {
            // ...then destroy the game object this script component is attached to.
            Destroy(gameObject);
        }
        else
        {
            // Tell Unity not to destory the GameObject this
            //  is attached to between scenes.
            DontDestroyOnLoad(gameObject);
            // Save an internal reference to the first instance of this class
            Instance = this;
        }
        Instance.prefabList.InitializePrefabDictionary();
    }

    [ContextMenu("PrintEntityDictionary")]
    public void PrintEntityDictionary()
    {
        foreach(KeyValuePair<string, GameObject> kvp in entityDictionary.entityDictionary)
        {
            Debug.Log("GUID: '" + kvp.Key + "' of object: " + kvp.Value);
        }
    }
}
