using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    private List<Zone> zones = new List<Zone>();
    [SerializeField] private SpriteRenderer zoneSprite;
    [SerializeField] private string colonyName = "Test Colony";

    /////////////////////////////////////
    // General Methods
    /////////////////////////////////////

    public string GetColonyName()
    {
        return colonyName;
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
        Destroy(zoneToRemove.visualBox);
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
