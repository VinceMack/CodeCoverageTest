using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Static methods relating to pawn hunger
 *
*/

public static class PawnHunger
{
    /*

    public static readonly string[] FoodItemNames = new string[] { "Berries" };
    public static readonly int HUNGER_RESPONSE_THRESHOLD = 20;       // Pawns will try to eat if below this value

    // Eat items from pawn items until full or no more food is available
    // True if successful, false if no food found
    public static bool EatFromInventory(Pawn_VM pawn)
    {
        bool foodFound = false;

        foreach (string foodname in FoodItemNames)
        {
            if (pawn.items.ContainsKey(foodname) && pawn.items[foodname] != null)
            {
                while (pawn.hunger < 100 && pawn.items[foodname].Quantity > 0)
                {
                    // food item found, eat
                    pawn.hunger += pawn.items[foodname].FoodValue;
                    pawn.items[foodname].Quantity--;

                    Debug.Log(pawn.name + " ate " + pawn.items[foodname].DisplayName);

                    // if quantity is 0, remove from dictionary
                    if (pawn.items[foodname].Quantity == 0)
                        pawn.items.Remove(foodname);

                    foodFound = true;
                }
            }
        }
        return foodFound;
    }

    */
}
