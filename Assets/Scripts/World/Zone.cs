using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{
    public enum ZoneType { NormalFoodFarm = 1, PremiumFoodFarm = 2, ClothFarm = 3, NormalMedicineFarm = 4, PremiumMedicineFarm = 5, Destroy = 6, Create = 7 };

    protected ZoneType myType;

    protected Vector2 topRight;
    protected Vector2 bottomLeft;
    protected List<Vector2> corners;

    protected float height;
    protected float width;
    protected Vector2 middle;

    protected Colony myColony;

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

        if(type < 7 && type > 1)
        {
            myType = (ZoneType)(type - 1);
        }
        else
        {
            myType = ZoneType.NormalFoodFarm;
        }
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

    public ZoneType GetZoneType()
    {
        return myType;
    }
}
