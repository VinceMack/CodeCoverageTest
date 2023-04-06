using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NPCStats : BaseStats
{
    public int currentHealth;
    public int maxHealth;

    public Dictionary<Slot, Equipment> Equipment = new Dictionary<Slot, Equipment>()
    {
        { Slot.Head, new Armor() },
        { Slot.Chest, new Armor() },
        { Slot.Legs, new Armor() },
        { Slot.Feet, null },
        { Slot.Hands, null },
        { Slot.Neck, null },
        { Slot.Ammo, null },
        { Slot.WeaponLeft, null },
        { Slot.Shield, null },
        { Slot.TwoHandedWeapon, null },
        { Slot.Wrist, null }
    };

    public List<Item> Inventory = new List<Item>()
    {
        new Item()
    };
}
