[System.Serializable]
public class Equipment : Item
{
    public Equipment(string name, bool consumable, Rarity rarity, int value, int quantity, string blockName, Slot slot) : base(name, consumable, rarity, value, quantity, blockName)
    {
        EquippedSlot = slot;
    }

    public Equipment(Equipment duplicate) : base(duplicate)
    {
        EquippedSlot = duplicate.EquippedSlot;
    }

    public Slot EquippedSlot;
}