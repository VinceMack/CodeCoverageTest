[System.Serializable]
public class Weapon : Equipment
{
    public Weapon(string name, string displayName, bool consumable, Rarity rarity, int value, int quantity, string blockName, Slot slot, float attackSpeed, int damage, float range)
         : base(name, displayName, consumable, rarity, value, quantity, blockName, slot)
    {
        AttackSpeed = attackSpeed;
        Damage = damage;
        Range = range;
    }

    public Weapon(Weapon duplicate) : base(duplicate)
    {
        AttackSpeed = duplicate.AttackSpeed;
        Damage = duplicate.Damage;
        Range = duplicate.Range;
    }

    public Weapon() : base()
    {
        
    }

    public float AttackSpeed = 5;
    public int Damage = 2;
    public float Range = 1;
}