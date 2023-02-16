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
        { Slot.Head, new Armor() {
            Name = "Snoopy Cap",
            EquippedSlot = Slot.Head,
            Rarity = Rarity.Unique,
            Value = 500,
            Defense = 4
        }},
        { Slot.Chest, new Armor() {
            Name = "Oxygen Pack",
            EquippedSlot = Slot.Chest,
            Rarity = Rarity.Legendary,
            Value = 800,
            Defense = 3
        } },
        { Slot.Legs, new Armor() {
            Name = "Moon Boots",
            EquippedSlot = Slot.Legs,
            Rarity = Rarity.Uncommon,
            Value = 350,
            Defense = 8
        } },
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
        new Item
        {
            Name = "Special Item",
            Rarity = Rarity.Legendary,
            Value = 420
        }
    };
}
