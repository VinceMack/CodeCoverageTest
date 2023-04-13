using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalSelection : MonoBehaviour
{
    [SerializeField] private Colony myColony;

    bool dragSelect = false;

    Vector3 p1;
    Vector3 p2;

    //the corners of our 2d selection box
    Vector2[] corners;

    float height;
    float width;
    Vector2 middle;

    // Update is called once per frame
    void Update()
    {
        if((int)UIManager.myMode > 1)
        {
            //1. when left mouse button clicked (but not released)
            if (Input.GetMouseButtonDown(0))
            {
                p1 = Input.mousePosition;
            }

            //2. while left mouse button held
            if (Input.GetMouseButton(0))
            {
                if((p1 - Input.mousePosition).magnitude > 40)
                {
                    dragSelect = true;
                }
            }

            //3. when mouse button comes up
            if (Input.GetMouseButtonUp(0))
            {
                if(dragSelect != false) 
                {
                    p2 = Input.mousePosition;
                    corners = getBoundingBox(p1, p2);

                    Vector3 topRight = Camera.main.ScreenPointToRay(corners[1]).origin;
                    Vector3 bottomLeft = Camera.main.ScreenPointToRay(corners[2]).origin;

                    topRight = new Vector3((float)Math.Ceiling(topRight.x), (float)Math.Ceiling(topRight.y), topRight.z);
                    bottomLeft = new Vector3((float)Math.Floor(bottomLeft.x), (float)Math.Floor(bottomLeft.y), bottomLeft.z);

                    // Convert the world space mouse position to a grid position
                    Vector3Int topRightGridPosition = GridManager.tileMap.WorldToCell(topRight);
                    Vector3Int bottomLeftGridPosition = GridManager.tileMap.WorldToCell(bottomLeft);

                    // Get the tile at the grid position
                    BaseTile_VM topRightTile = (BaseTile_VM)GridManager.tileMap.GetTile(topRightGridPosition);
                    BaseTile_VM bottomLeftTile = (BaseTile_VM)GridManager.tileMap.GetTile(bottomLeftGridPosition);

                    if(topRightTile != null && bottomLeftTile != null)
                    {
                        if((int)UIManager.myMode < 7)
                        {
                            Zone newZone = new Zone(topRight, bottomLeft, myColony, (int)UIManager.myMode);
                        }
                        else
                        {
                            Area newZone = new Area(topRight, bottomLeft);
                        }
                    }
                }

                dragSelect = false;
            }
        }
    }

    private void OnGUI()
    {
        if(dragSelect == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //create a bounding box (4 corners in order) from the start and end mouse position
    Vector2[] getBoundingBox(Vector2 p1,Vector2 p2)
    {
        // Min and Max to get 2 corners of rectangle regardless of drag direction.
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);

        // 0 = top left; 1 = top right; 2 = bottom left; 3 = bottom right;
        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };

        return corners;
    }

    //Check non-collider neighbors
    //Check pathfind
    //


    //Treat orders in 'batches'
    // foreach order:
    //      Check non-collider neighbor
    //      If non-collider, Check pathfinding
    // If non reachable, cancel order
    // If some reachable, set conditionals, when conditional finsihed, enqueue next non reachable
}