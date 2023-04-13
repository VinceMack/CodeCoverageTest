using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaborOrder_Forage : LaborOrder_Base_VM
{
    private static float BASE_TTC = 1.5f;
    private GameObject targetBush;
    private bool storeInChest;              // true will follow up with a labor order to store the berries in a chest, false keeps in inventory. // todo
    private readonly int BerryFoodValue = 5;

    // constructor
    public LaborOrder_Forage(GameObject targetBush, bool storeInChest = true)
    {
        laborType = LaborType.Forage;
        timeToComplete = BASE_TTC;
        this.storeInChest = storeInChest;
        this.targetBush = targetBush;
        location = Vector3Int.FloorToInt(targetBush.transform.position);
    }

    // override of the execute method to preform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        if (targetBush != null)
        {
            // forage for berries
            yield return new WaitForSeconds(timeToComplete);

            if (targetBush != null)
            {
                int quantity = targetBush.GetComponent<Bush>().Harvest();
                Item berryItem;
                // add berries to item dictionary
                if (pawn.items.TryGetValue("Berries", out berryItem))
                {
                    berryItem.Quantity += quantity;
                }
                else
                {
                    berryItem = new Item("Berries", "Berries", true, Rarity.Common, 1, quantity, "", BerryFoodValue);
                    pawn.items.Add("Berries", berryItem);
                }
            }
        }
        yield break;
    }
}
