using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaircaseItem : Item
{
    public StaircaseItem(string name, string displayName, bool consumable, Rarity rarity, int value, int quantity, string blockName) : base(name, displayName, consumable, rarity, value, quantity, blockName)
    {
        
    }

    public StaircaseItem(StaircaseItem duplicate) : base(duplicate)
    {

    }

    public StaircaseItem() : base()
    {

    }

    public PrefabList prefabList;
    public EntityDictionary entityDictionary;
    public new string BlockName = "staircase";

    public override void InvokePlacing(BaseNPC placer)
    {
        prefabList = GlobalInstance.Instance.prefabList;
        entityDictionary = GlobalInstance.Instance.entityDictionary;

        GameObject staircaseOriginBlock = entityDictionary.InstantiateEntity(BlockName);
        Staircase staircaseOriginComp = staircaseOriginBlock.GetComponent<Staircase>();
        bool success = staircaseOriginComp.PlaceObject(placer.GetCurrentLocation());

        GameObject staircaseDestinationBlock = entityDictionary.InstantiateEntity(BlockName);
        Staircase staircaseDestinationComp = staircaseDestinationBlock.GetComponent<Staircase>();
        success = success && staircaseDestinationComp.PlaceObject(staircaseOriginComp.GetDestinationTile(), false);
    }
}
