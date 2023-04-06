using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualZone : Zone
{
    protected GameObject visualBox;

    protected string zoneName;

    public VisualZone(Vector2 tR, Vector2 bL, Colony colony, int type) : base(tR, bL, colony, type)
    {
        visualBox = new GameObject("Zone" + myColony.GetNextZoneNumber());
        zoneName = "Zone " + myColony.GetNextZoneNumber();
        visualBox.transform.position = new Vector3(middle.x, middle.y, 0);
        visualBox.transform.localScale = new Vector3(width, height, 1f);
        SpriteRenderer myRend = visualBox.AddComponent<SpriteRenderer>();
        myRend.sprite = myColony.GetZoneSprite();

        Color tmp = Color.yellow;
        tmp.a = 0.35f;
        myRend.color = tmp;

        myColony.AddZone(this);
    }

    public string GetZoneName()
    {
        return zoneName;
    }

    public void DeleteZone()
    {
        myColony.RemoveZone(this);
    }

    public GameObject GetVisualBox()
    {
        return visualBox;
    }
}
