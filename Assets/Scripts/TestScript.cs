using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Tile testLocation;

    [ContextMenu("TestStaircasePlacing")]
    public void TestStaircasePlacement()
    {
        StaircaseItem item = new StaircaseItem();
        BaseNPC fakeNpc = new BaseNPC();
        fakeNpc.SetCurrentLocation(testLocation);
        item.InvokePlacing(fakeNpc);
    }

    [ContextMenu("TestInheritence")]
    public void TestInheritence()
    {
        NPCStats npc = GetComponent<NPCStats>();
        BaseStats baseS = GetComponent<NPCStats>();
        Debug.Log(npc.GetType());
        Debug.Log(baseS.GetType());

    }

    [ContextMenu("TestSingleton")]
    public void TestSingleton()
    {
        Debug.Log(GlobalInstance.Instance);
    }

    [ContextMenu("PrintObjectDictionary")]
    public void PrintObjectDictionary()
    {
        EntityDictionary entityDict = GlobalInstance.Instance.entityDictionary;
        foreach(KeyValuePair<string, GameObject> kvp in entityDict.entityDictionary)
        {
            Debug.Log(kvp.Value.name);
        }
    }  
}
