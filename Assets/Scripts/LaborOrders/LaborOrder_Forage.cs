using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborOrder_Forage : LaborOrder_Base
{
    public enum ObjectType { Bush, Tree };
    private static float BASE_TTC = 1.5f;
    private Item targetBush;
    private ObjectType type;

    // constructor
    public LaborOrder_Forage(Item targetBush, ObjectType type)
    {
        laborType = LaborType.Forage;
        timeToComplete = BASE_TTC;
        this.targetBush = targetBush;
        location = Vector3Int.FloorToInt(targetBush.transform.position);
        this.type = type;
    }

    // override of the execute method to perform the labor order
    public override IEnumerator Execute(Pawn pawn)
    {
        pawn.path.Clear();

        if (targetBush != null && targetBush.GetComponent<Item>().isForageable == true && targetBush.GetComponent<Item>().isItemized == false)
        {
            yield return new WaitForSeconds(timeToComplete);

            if(type == ObjectType.Bush){
                targetBush.GetComponent<Bush>().Harvest();

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile> adjacentTiles = GridManager.GetAdjacentTiles(GridManager.GetTile(location));
                foreach (BaseTile adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        Item resource = Resources.Load<Item>("prefabs/items/berries").GetComponent<Item>();
                        Item wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity).GetComponent<Item>();
                        wheatItem.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                        adjacentTile.SetTileInformation(adjacentTile.type, false, wheatItem, adjacentTile.resourceCount, adjacentTile.position);
                        break;
                    }
                }

            }

            if(type == ObjectType.Tree){
                targetBush.GetComponent<Tree>().Harvest();

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile> adjacentTiles = GridManager.GetAdjacentTiles(GridManager.GetTile(location));
                foreach (BaseTile adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        Item resource = Resources.Load<Item>("prefabs/items/Wood");
                        Item wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity);
                        wheatItem.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                        adjacentTile.SetTileInformation(adjacentTile.type, false, wheatItem, adjacentTile.resourceCount, adjacentTile.position);
                        break;
                    }
                }
            }

        }
        yield break;
    }
}



