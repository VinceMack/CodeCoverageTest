using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone
{
    private Vector2 topRight;
    private Vector2 bottomLeft;
    private List<Vector2> corners;

    private float height;
    private float width;
    private Vector2 middle;

    private Colony myColony;

    public GameObject visualBox;

    public Zone(Vector2 tR, Vector2 bL, Colony colony)
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
        visualBox.transform.position = new Vector3(middle.x, middle.y, 0);
        visualBox.transform.localScale = new Vector3(width, height, 1f);
        SpriteRenderer myRend = visualBox.AddComponent<SpriteRenderer>();
        myRend.sprite = myColony.GetZoneSprite();

        Color tmp = Color.yellow;
        tmp.a = 0.35f;
        myRend.color = tmp;
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
}
