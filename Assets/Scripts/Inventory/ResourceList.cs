using System.Collections;
using System.Collections.Generic;

public static class ResourceList
{
    public static Dictionary<string, Resource> itemList = new Dictionary<string, Resource>
    {
        {"food",  new Resource("food", "Food", new List<string>{ "berry", "bread"}, true)},
        {"medicine", new Resource("medicine", "Medicine", new List<string>{ "honey" }, false)}
    };
}
