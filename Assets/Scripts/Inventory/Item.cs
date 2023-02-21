[System.Serializable]
public class Item
{
    public string Name = "Cool Rock";
    public bool IsConsumable = false;
    public Rarity Rarity = Rarity.Common;
    public int Value = 10;
    public int Quantity = 1;
    public string BlockName = ""; //Note may want to change this to int

    public virtual void InvokePlacing(BaseNPC placer)
    {

    }
}