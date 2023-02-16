using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : SaveableEntity
{
    [SerializeField] protected Tile homeTile;

    public virtual void PlaceObject(Tile location)
    {
        homeTile = location;
        transform.SetParent(location.transform);
        transform.localPosition = new Vector3();
    }
}
