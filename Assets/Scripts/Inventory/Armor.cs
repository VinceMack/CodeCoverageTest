[System.Serializable]
public class Armor : Equipment
{
    public Armor(string name, string displayName, bool consumable, Rarity rarity, int value, int quantity, string blockName, Slot slot, int defense, int warmth, int coverage)
         : base(name, displayName, consumable, rarity, value, quantity, blockName, slot)
    {
        Defense = defense;
        Warmth = warmth;
        Coverage = coverage;
    }

    public Armor(Armor duplicate) : base(duplicate)
    {
        Defense = duplicate.Defense;
        Warmth = duplicate.Warmth;
        Coverage = duplicate.Coverage;
    }

    public Armor() : base()
    {
        
    }

    public int Defense = 0;
    public int Warmth = 0;
    public int Coverage = 0;
}
