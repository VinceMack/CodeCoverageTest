using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    private List<Zone> zones = new List<Zone>();

    [SerializeField] private SpriteRenderer zoneSprite;

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
}
