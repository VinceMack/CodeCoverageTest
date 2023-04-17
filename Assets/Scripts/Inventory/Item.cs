[System.Serializable]
public class Item
{
    public string Name = "";
    public string DisplayName = "";
    public bool IsConsumable = false;
    public Rarity Rarity = Rarity.Common;
    public int Value = 1;
    public int Quantity = 0;
    public string BlockName = ""; //Note may want to change this to int
    public int FoodValue = 0;

    public virtual void InvokePlacing(BaseNPC placer)
    {

    }

    public Item()
    {
        
    }

    public Item(string name, string displayName, bool consumable, Rarity rarity, int value, int quantity, string blockName, int foodvalue = 0)
    {
        Name = name;
        DisplayName = displayName;
        IsConsumable = consumable;
        Rarity = rarity;
        Value = value;
        Quantity = quantity;
        BlockName = blockName;
        FoodValue = foodvalue;
    }

    public Item(Item duplicate)
    {
        Name = duplicate.Name;
        DisplayName = duplicate.DisplayName;
        IsConsumable = duplicate.IsConsumable;
        Rarity = duplicate.Rarity;
        Value = duplicate.Value;
        Quantity = duplicate.Quantity;
        BlockName = duplicate.BlockName;
        FoodValue = duplicate.FoodValue;
    }
}