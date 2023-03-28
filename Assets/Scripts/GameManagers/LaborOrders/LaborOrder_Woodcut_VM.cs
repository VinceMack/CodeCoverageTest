using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Woodcut_VM : LaborOrder_Base_VM
{
    public GameObject woodLog;

    // override of the execute method to preform the labor order
    public override IEnumerator execute(Pawn_VM pawn)
    {
        yield return new WaitForSeconds(timeToComplete);
        // remove tree game object at the location of the labor order
        // spawn woodLog game object at the location of the labor order
    }
}
