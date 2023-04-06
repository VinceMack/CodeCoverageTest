using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmZone : VisualZone
{
    private Resource farmResource;

    public FarmZone(Vector2 tR, Vector2 bL, Colony colony, int type, Resource farmResource) : base(tR, bL, colony, type)
    {
        myColony.AddFarmZone(this);

        this.farmResource = farmResource;
    }

    public override void DeleteZone()
    {
        myColony.RemoveFarmZone(this);
    }
}
