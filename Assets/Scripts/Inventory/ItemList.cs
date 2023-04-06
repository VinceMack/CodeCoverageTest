using System.Collections;
using System.Collections.Generic;

public static class ItemList
{
    public static Dictionary<string, Item> itemList = new Dictionary<string, Item>
    {
        {"berry",  new Item("Berries", true, Rarity.Common, 2, 0, "Dropped Berries")},
        {"bread",  new Item("Bread", true, Rarity.Common, 5, 0, "Dropped Bread")},
        {"cloth",  new Item("Cloth", false, Rarity.Uncommon, 3, 0, "Dropped Cloth")},
        {"honey", new Item("Honey", true, Rarity.Uncommon, 20, 0, "Dropped Honey")}
    };
}
