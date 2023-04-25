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

    public void CreateChests()
    {
        for(int i = (int)bottomLeft.x; i < (int)topRight.x; i++)
        {
            for(int j = (int)bottomLeft.y; j < (int)topRight.y; j++)
            {
                GameObject itemToPlace = Resources.Load<GameObject>("prefabs/items/Chest") as GameObject;
                LaborOrderManager_VM.AddPlaceLaborOrder(itemToPlace, new Vector2(i, j));

                // // AUTO CREATION OF CHESTS
                // BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(new Vector3Int(i, j, 0));
                // if (tile != null && tile.type == TileType.GRASS && tile.resource == null)
                // {
                    
                //     GameObject chestPrefab = Resources.Load<GameObject>("prefabs/items/Chest");
                //     GameObject chestInstance = UnityEngine.Object.Instantiate(chestPrefab, tile.position, Quaternion.identity);
                //     chestInstance.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                //     tile.SetTileInformation(tile.type, true, chestInstance, tile.resourceCount, tile.position);
                // }
            }
        }
    }

    public void DestroyObjects(Item[] gameObjectsInScene)
    {        
        // Check if the objects array is null
        if (gameObjectsInScene == null)
        {
            Debug.LogWarning("No GameObjects found in the scene.");
            return;
        }
        Debug.Log(topRight);
        Debug.Log(bottomLeft);

        foreach (Item itemComponent in gameObjectsInScene)
        {
            if (itemComponent.isDeconstructable)
            {
                Debug.Log(itemComponent.location.GetXPosition());
                Debug.Log(itemComponent.location.GetYPosition());
                if(itemComponent.location.GetXPosition() <= topRight.x && itemComponent.location.GetXPosition() > bottomLeft.x)
                {
                    if(itemComponent.location.GetYPosition() <= topRight.y && itemComponent.location.GetYPosition() > bottomLeft.y)
                    {
                        LaborOrderManager_VM.AddSpecificDeconstructLaborOrder(itemComponent.gameObject);
                    }
                }
            }
        }
    }
}
