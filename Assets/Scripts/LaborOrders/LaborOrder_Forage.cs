using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborOrder_Forage : LaborOrder_Base_VM
{
    public enum ObjectType { Bush, Tree };
    private static float BASE_TTC = 1.5f;
    private GameObject targetBush;
    private ObjectType type;

    // constructor
    public LaborOrder_Forage(GameObject targetBush, ObjectType type)
    {
        laborType = LaborType.Forage;
        timeToComplete = BASE_TTC;
        this.targetBush = targetBush;
        location = Vector3Int.FloorToInt(targetBush.transform.position);
        this.type = type;
    }

    // override of the execute method to perform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        pawn.path.Clear();

        if (targetBush != null && targetBush.GetComponent<Item>().isForageable == true && targetBush.GetComponent<Item>().isItemized == false)
        {
            yield return new WaitForSeconds(timeToComplete);

            if(type == ObjectType.Bush){
                targetBush.GetComponent<Bush>().Harvest();

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile_VM> adjacentTiles = GridManager.GetAdjacentTiles(GridManager.GetTile(location));
                foreach (BaseTile_VM adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        GameObject resource = Resources.Load<GameObject>("prefabs/items/berries");
                        GameObject wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity);
                        wheatItem.transform.SetParent(GameObject.Find("GameManager").transform.Find("Objects"));
                        adjacentTile.SetTileInformation(adjacentTile.type, false, wheatItem, adjacentTile.resourceCount, adjacentTile.position);
                        break;
                    }
                }

            }

            if(type == ObjectType.Tree){
                targetBush.GetComponent<Tree>().Harvest();

                // spawn seeds in an adjacent tile that is not collision and does not have a resource
                List<BaseTile_VM> adjacentTiles = GridManager.GetAdjacentTiles(GridManager.GetTile(location));
                foreach (BaseTile_VM adjacentTile in adjacentTiles)
                {
                    if (!adjacentTile.isCollision && adjacentTile.resource == null)
                    {
                        GameObject resource = Resources.Load<GameObject>("prefabs/items/Wood");
                        GameObject wheatItem = UnityEngine.Object.Instantiate(resource, adjacentTile.position, Quaternion.identity);
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
