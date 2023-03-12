using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : SaveableEntity
{
    [SerializeField] protected GameObjectTile homeTile;

    public virtual void PlaceObject(GameObjectTile location)
    {
        homeTile = location;
        transform.SetParent(location.transform);
        transform.localPosition = new Vector3();
    }
}
