using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area
{
    protected Vector2 topRight;
    protected Vector2 bottomLeft;
    protected List<Vector2> corners;

    protected float height;
    protected float width;
    protected Vector2 middle;

    public Area(Vector2 tR, Vector2 bL)
    {
        topRight = tR;
        bottomLeft = bL;
        corners = new List<Vector2>();
        corners.Add(topRight);                  //Top right
        corners.Add(bottomLeft);                //Bottom left
        corners.Add(new Vector2(bL.x, tR.y));   //Top left
        corners.Add(new Vector2(tR.x, bL.y));   //Bottom right

        height = topRight.y - bottomLeft.y;
        width = topRight.x - bottomLeft.x;

        middle = new Vector2(bottomLeft.x + (width/2), bottomLeft.y + (height/2));
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
}
