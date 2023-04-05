using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{
    public enum ZoneType { NormalFoodFarm = 1, PremiumFoodFarm = 2, ClothFarm = 3, NormalMedicineFarm = 4, PremiumMedicineFarm = 5 };

    private ZoneType myType;

    private Vector2 topRight;
    private Vector2 bottomLeft;
    private List<Vector2> corners;

    private float height;
    private float width;
    private Vector2 middle;

    private Colony myColony;

    public GameObject visualBox;

    private string zoneName;

    public Zone(Vector2 tR, Vector2 bL, Colony colony, int type)
    {
        myColony = colony;

        topRight = tR;
        bottomLeft = bL;
        List<Vector2> corners = new List<Vector2>();
        corners.Add(topRight);                  //Top right
        corners.Add(bottomLeft);                //Bottom left
        corners.Add(new Vector2(bL.x, tR.y));   //Top left
        corners.Add(new Vector2(tR.x, bL.y));   //Bottom right

        height = topRight.y - bottomLeft.y;
        width = topRight.x - bottomLeft.x;

        middle = new Vector2(bottomLeft.x + (width/2), bottomLeft.y + (height/2));

        visualBox = new GameObject("Zone" + myColony.GetNextZoneNumber());
        zoneName = "Zone " + myColony.GetNextZoneNumber();
        visualBox.transform.position = new Vector3(middle.x, middle.y, 0);
        visualBox.transform.localScale = new Vector3(width, height, 1f);
        SpriteRenderer myRend = visualBox.AddComponent<SpriteRenderer>();
        myRend.sprite = myColony.GetZoneSprite();

        if(type < 7 && type > 1)
        {
            myType = (ZoneType)(type - 1);
        }
        else
        {
            myType = ZoneType.NormalFoodFarm;
        }

        Color tmp = Color.yellow;
        tmp.a = 0.35f;
        myRend.color = tmp;

        myColony.AddZone(this);
    }

    public Zone()
    {
        topRight = new Vector2();
        bottomLeft = new Vector2();
        corners.Add(topRight);                  //Top right
        corners.Add(bottomLeft);                //Bottom left
        corners.Add(new Vector2());             //Top left
        corners.Add(new Vector2());             //Bottom right
    }

    public List<Vector2> GetTwoCorners()
    {
        return new List<Vector2>{topRight, bottomLeft};
    }

    public List<Vector2> GetCorners()
    {
        return corners;
    }

    public Vector2 GetCenter()
    {
        return middle;
    }

    public string GetZoneName()
    {
        return zoneName;
    }

    public ZoneType GetZoneType()
    {
        return myType;
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
