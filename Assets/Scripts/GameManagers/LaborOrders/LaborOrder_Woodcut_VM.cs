using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Woodcut_VM : LaborOrder_Base_VM
{
    public static GameObject woodLog;
    private static float BASE_TTC = 3f;
    private GameObject targetTree;

    // constructor
    public LaborOrder_Woodcut_VM(GameObject targetTree)
    {
        laborType = LaborType.Woodcut;
        timeToComplete = BASE_TTC;
        if(woodLog == null) woodLog = GlobalInstance.Instance.prefabList.prefabDictionary["wood"].prefab;
        this.targetTree = targetTree;
        location = Vector3Int.FloorToInt(targetTree.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        if (targetTree != null)
        {
            // cutting down tree
            yield return new WaitForSeconds(timeToComplete);

            if (targetTree != null)
            {
                // delete tree
                Vector3 treePosition = targetTree.transform.position;
                Transform treeParent = targetTree.transform.parent;
                UnityEngine.Object.Destroy(targetTree);

                // create wood in tree's place
                BaseTile_VM tile = (BaseTile_VM)GridManager.tileMap.GetTile(Vector3Int.FloorToInt(treePosition));
                GameObject woodObject = GlobalInstance.Instance.entityDictionary.InstantiateEntity("wood", "", treePosition);
                woodObject.transform.SetParent(treeParent);
                tile.SetTileInformation(tile.type, false, woodObject, tile.resourceCount, tile.position);
            }
        }
        yield break;
    }
}
