using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    public List<Zone> zones = new List<Zone>();
    public List<string> colonyResources = new List<string>();
    private Dictionary<string, ResourceListElement> resourceListRef =  new Dictionary<string, ResourceListElement>();
    [SerializeField] private SpriteRenderer zoneSprite;
    [SerializeField] private string colonyName = "Test Colony";
    private int colonyResourceCheckTime = 1;

    /////////////////////////////////////
    // General Methods
    /////////////////////////////////////

    private void Awake() 
    {
        foreach(KeyValuePair<string, Resource> kvp in ResourceManager.resourceDictionary)
        {
            colonyResources.Add(kvp.Key);
        }
        StartCoroutine("EssentialResourceCoroutine");
    }

    public string GetColonyName()
    {
        return colonyName;
    }

    public IEnumerable EssentialResourceCoroutine()
    {
        foreach(string resourceName in colonyResources)
        {
            if(!ResourceManager.resourceDictionary[resourceName].EssentialConditionMet())
            {
                // Give some warning or end the colony
            }
        }
        yield return new WaitForSeconds(colonyResourceCheckTime);
    }

    public void RegisterResourceListElement(ResourceListElement element, string name)
    {
        if(!resourceListRef.ContainsKey(name))
        {
            resourceListRef.Add(name, element);
        }
    }

    public int GetResourceQuantity(string resourceName)
    {
        if(!ResourceManager.resourceDictionary.ContainsKey(resourceName))
        {
            return 0;
        }

        return ResourceManager.resourceDictionary[resourceName].GetResourceQuantity();
    }

    public void UpdateResourceListElement(string resourceName)
    {
        if(resourceListRef.ContainsKey(resourceName))
        {
            resourceListRef[resourceName].UpdateValue();
        }
    }

    public void UpdateResourceList()
    {
        foreach(string resourceName in colonyResources)
        {
            UpdateResourceListElement(resourceName);
        }
    }

    [ContextMenu("TestResourceCount")]
    public void Test()
    {
        foreach(string resourceName in colonyResources)
        {
            Debug.Log(resourceName + " : " + GetResourceQuantity(resourceName));
        }
    }

    /////////////////////////////////////
    // Zone Methods 
    /////////////////////////////////////

    public void AddZone(Zone newZone)
    {
        zones.Add(newZone);
    }

    public int GetNextZoneNumber()
    {
        return zones.Count+1;
    }

    public Sprite GetZoneSprite()
    {
        return zoneSprite.sprite;
    }

    public void RemoveZone(Zone zoneToRemove)
    {
        Destroy(zoneToRemove.GetVisualBox());
        if(zones.Contains(zoneToRemove))
        {
            zones.Remove(zoneToRemove);
        }
    }

    public List<Zone> GetZones()
    {
        return zones;
    }
}
