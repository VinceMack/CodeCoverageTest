using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 
 * Forage/Harvest berries or wheat. Wheat must be fully grown to harvest.
 * These could be split into two different order types but they are the same for now
 * 
*/

public class LaborOrder_Forage : LaborOrder_Base_VM
{
    public enum ObjectType { Bush, Wheat };
    private static float BASE_TTC = 1.5f;
    private GameObject targetObject;
    private bool storeInChest;              // true will follow up with a labor order to store the items in a chest, false keeps in inventory. // todo
    private readonly int BerryFoodValue = 1;
    private ObjectType type;

    // constructor
    public LaborOrder_Forage(GameObject targetObject, bool storeInChest = true)
    {
        laborType = LaborType.Forage;
        timeToComplete = BASE_TTC;
        this.storeInChest = storeInChest;
        this.targetObject = targetObject;
        location = Vector3Int.FloorToInt(targetObject.transform.position);
        
        // check if bush or wheat
        if(targetObject.GetComponent<Bush>())
        {
            type = ObjectType.Bush;
        }
        else if(targetObject.GetComponent<Wheat>())
        {
            type = ObjectType.Wheat;
        }
        else
        {
            Debug.LogWarning("Target object does not contain component Bush or Wheat!");
        }
    }

    // override of the execute method to perform the labor order
    public override IEnumerator Execute(Pawn_VM pawn)
    {
        if (targetObject != null)
        {
            // forage/harvest
            yield return new WaitForSeconds(timeToComplete);

            if (targetObject != null)
            {
                if(type == ObjectType.Bush)             // Forage for berries
                {
                    int quantity = targetObject.GetComponent<Bush>().Harvest();
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
                else if(type == ObjectType.Wheat)      // Harvest Wheat
                {
                    if(targetObject.GetComponent<Wheat>().Harvest())
                    {
                        Item wheatItem;
                        if (pawn.items.TryGetValue("Wheat", out wheatItem))
                        {
                            wheatItem.Quantity += 1;
                        }
                        else
                        {
                            wheatItem = new Item("Wheat", "Wheat", false, Rarity.Common, 1, 1, "");
                            pawn.items.Add("Wheat", wheatItem);
                        }
                    }
                    else
                    {
                        Debug.Log("Wheat is not ready to be harvested!");
                    }
                }
            }
        }
        yield break;
    }
}
