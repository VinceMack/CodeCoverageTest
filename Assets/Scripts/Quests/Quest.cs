using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    private Item reward;
    private Item requirement;
    private Colony myColony;

    public Quest(Item reward, Item requirement, Colony colony)
    {
        this.reward = reward;
        this.requirement = requirement;
        myColony = colony;
    }

    public Item GetRewardItem()
    {
        return reward;
    }

    public Item GetRequirementItem()
    {
        return requirement;
    }

    public int GetStatus()
    {
        return myColony.GetNumberOfItemInGalaxy(requirement.Name);
    }

    public bool CheckCondition()
    {
        return(myColony.ColonyHasItem(requirement.Name, requirement.Quantity));
    }

    public bool CompleteQuest()
    {
        if(myColony.RemoveItemFromColony(requirement, requirement.Quantity))
        {
            myColony.AddItemToColony(reward);
            return true;
        }
        return false;
    }
}
