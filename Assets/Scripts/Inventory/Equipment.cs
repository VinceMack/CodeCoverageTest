[System.Serializable]
public class Equipment : Item
{
    public Equipment(string name, string displayName, bool consumable, Rarity rarity, int value, int quantity, string blockName, Slot slot) : base(name, displayName, consumable, rarity, value, quantity, blockName)
    {
        EquippedSlot = slot;
    }

    public Equipment(Equipment duplicate) : base(duplicate)
    {
        EquippedSlot = duplicate.EquippedSlot;
    }

    public Equipment() : base()
    {
        
    }

    public Slot EquippedSlot;
}