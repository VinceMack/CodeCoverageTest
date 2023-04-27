using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private string rewardName;
    private int rewardQuantity;
    private string requirementName;
    private int requirementQuantity;
    private Colony myColony;

    public Quest(string rewardName, int rewardQuantity, string requirementName, int requiredItemQuantity, Colony colony)
    {
        this.rewardName = rewardName;
        this.rewardQuantity = rewardQuantity;
        this.requirementName = requirementName;
        this.requirementQuantity = requiredItemQuantity;
        myColony = colony;
    }

    public string GetRewardItemName()
    {
        return rewardName;
    }

    public int GetRewardItemCount()
    {
        return rewardQuantity;
    }

    public string GetRequirementItemName()
    {
        return requirementName;
    }

    public int GetRequirementItemCount()
    {
        return requirementQuantity;
    }

    public int GetStatus()
    {
        return myColony.GetNumberOfItemInGalaxy(requirementName);
    }

    public bool CheckCondition()
    {
        return(myColony.ColonyHasItem(requirementName, requirementQuantity));
    }

    public bool CompleteQuest()
    {
        // if(myColony.RemoveItemFromColony(requirementName, requirementQuantity))
        // {
        //     myColony.AddItemToColony(rewardName, rewardQuantity);
        //     return true;
        // }
        // return false;
        return true;
    }
}



