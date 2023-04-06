[System.Serializable]
public class Item
{
    public string Name = "";
    public bool IsConsumable = false;
    public Rarity Rarity = Rarity.Common;
    public int Value = 1;
    public int Quantity = 0;
    public string BlockName = ""; //Note may want to change this to int

    public virtual void InvokePlacing(BaseNPC placer)
    {

    }

    public Item()
    {
        
    }

    public Item(string name, bool consumable, Rarity rarity, int value, int quantity, string blockName)
    {
        Name = name;
        IsConsumable = consumable;
        Rarity = rarity;
        Value = value;
        Quantity = quantity;
        BlockName = blockName;
    }

    public Item(Item duplicate)
    {
        Name = duplicate.Name;
        IsConsumable = duplicate.IsConsumable;
        Rarity = duplicate.Rarity;
        Value = duplicate.Value;
        Quantity = duplicate.Quantity;
        BlockName = duplicate.BlockName;
    }
}