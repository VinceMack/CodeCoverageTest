using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Woodcut : LaborOrder
{
    GameObject woodPrefab;
    GameObject targetTree;

    // constructor
    public LaborOrder_Woodcut(GameObject targetTree) : base()
    {
        laborType = LaborType.Woodcut;
        timeToComplete = 3f;
        orderNumber = LaborOrderManager.getNumOfLaborOrders();

        woodPrefab = GlobalInstance.Instance.prefabList.prefabDictionary["wood"].prefab;
        this.targetTree = targetTree;
    }

    public override IEnumerator execute()
    {
        if(assignedPawn != null && targetTree != null)
        {
            //assignedPawn.transform.parent.gameObject;

            // go to tree
            assignedPawn.transform.position = targetTree.transform.position;

            // cut down tree
            yield return new WaitForSeconds(getTimeToComplete());

            if(targetTree != null)
            {
                // delete tree
                Vector3 treePosition = targetTree.transform.position;
                Transform treeParent = targetTree.transform.parent;
                UnityEngine.Object.Destroy(targetTree);

                // create wood in tree's place
                GameObject woodObject = GlobalInstance.Instance.entityDictionary.InstantiateEntity("wood", "", treePosition);
                woodObject.transform.SetParent(treeParent);
            }
        }
        // add the pawn back to the queue of available pawns
        //LaborOrderManager.addPawn(assignedPawn); // needs updated

        yield break;
    }
}