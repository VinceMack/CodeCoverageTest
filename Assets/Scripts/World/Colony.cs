using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    public List<FarmZone> farmZones = new List<FarmZone>();
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

    public void AddFarmZone(FarmZone newZone)
    {
        farmZones.Add(newZone);
    }

    public int GetNextFarmZoneNumber()
    {
        return farmZones.Count+1;
    }

    public Sprite GetZoneSprite()
    {
        return zoneSprite.sprite;
    }

    public void RemoveFarmZone(FarmZone zoneToRemove)
    {
        Destroy(zoneToRemove.GetVisualBox());
        if(farmZones.Contains(zoneToRemove))
        {
            farmZones.Remove(zoneToRemove);
        }
    }

    public List<FarmZone> GetFarmZones()
    {
        return farmZones;
    }
}
