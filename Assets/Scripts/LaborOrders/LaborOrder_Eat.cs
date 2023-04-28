using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LaborOrder_Eat : LaborOrder_Base
{
    // constructor
    public LaborOrder_Eat(Item target)
    {
        laborType = LaborType.Eat;
        timeToComplete = 1f;
        location = Vector3Int.FloorToInt(target.transform.position);
    }

    public override IEnumerator Execute(Pawn pawn)
    {
        BaseTile targetTile = GridManager.GetTile(location);
        BaseTile currentTile = (BaseTile)pawn.GetPawnTileFromTilemap();
        Chest chestContainingFood = (Chest)targetTile.resource;

        pawn.path.Clear();

        // eat berries until full or out of berries
        while (pawn.hunger < Pawn.MAX_HUNGER && chestContainingFood.ContainsItem("Berries"))
        {
            yield return new WaitForSeconds(0.1f);
            chestContainingFood.RemoveItem("Berries");
            pawn.hunger = (int)pawn.hunger + (int)Berries.foodValue;
        }

        yield break;
    }
}

