using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : Area
{
    public enum ZoneType { NormalFoodFarm = 1, PremiumFoodFarm = 2, ClothFarm = 3, NormalMedicineFarm = 4, PremiumMedicineFarm = 5 };

    protected ZoneType myType;
    protected Colony myColony;
    protected GameObject visualBox;
    protected string zoneName;

    public Zone(Vector2 tR, Vector2 bL, Colony colony, int type) : base(tR, bL)
    {
        myColony = colony;

        if(type < 7 && type > 1)
        {
            myType = (ZoneType)(type - 1);
        }
        else
        {
            myType = ZoneType.NormalFoodFarm;
        }

        visualBox = new GameObject("Zone" + myColony.GetNextZoneNumber());
        zoneName = "Zone " + myColony.GetNextZoneNumber();
        visualBox.transform.position = new Vector3(middle.x, middle.y, 0);
        visualBox.transform.localScale = new Vector3(width, height, 1f);
        visualBox.transform.SetParent(colony.gameObject.transform);
        SpriteRenderer myRend = visualBox.AddComponent<SpriteRenderer>();
        myRend.sprite = myColony.GetZoneSprite();

        Color tmp = Color.yellow;
        tmp.a = 0.35f;
        myRend.color = tmp;

        colony.AddZone(this);
    }

    public ZoneType GetZoneType()
    {
        return myType;
    }

    public string GetZoneName()
    {
        return zoneName;
    }

    public virtual void DeleteZone()
    {
        myColony.RemoveZone(this);
    }

    public GameObject GetVisualBox()
    {
        return visualBox;
    }
}
