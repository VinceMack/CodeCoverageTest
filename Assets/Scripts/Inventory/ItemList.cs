using System.Collections;
using System.Collections.Generic;

public static class ItemList
{
    public static Dictionary<string, Item> itemList = new Dictionary<string, Item>
    {
        {"berry",  new Item("berry", "Berries", true, Rarity.Common, 2, 0, "Dropped Berries")},
        {"bread",  new Item("bread", "Bread", true, Rarity.Common, 5, 0, "Dropped Bread")},
        {"cloth",  new Item("cloth", "Cloth", false, Rarity.Uncommon, 3, 0, "Dropped Cloth")},
        {"honey", new Item("honey", "Honey", true, Rarity.Uncommon, 20, 0, "Dropped Honey")},
        {"wood", new Item("wood", "Wood", true, Rarity.Common, 1, 0, "Dropped Wood")}
    };
}
