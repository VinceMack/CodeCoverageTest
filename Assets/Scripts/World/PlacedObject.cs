using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : SaveableEntity
{
    [SerializeField] protected BaseTile homeTile;

    public virtual void PlaceObject(BaseTile location)
    {
        homeTile = location;
    }
}
