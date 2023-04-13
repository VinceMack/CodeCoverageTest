using System.Collections;
using System.Collections.Generic;

public static class ResourceManager
{
    public static Dictionary<string, Resource> resourceDictionary = new Dictionary<string, Resource>
    {
        {"food",  new Resource("food", "Food", new List<string>{ "berry", "bread"}, true, new Condition(0, Condition.Operation.greaterThan), "resource_food")},
        {"medicine", new Resource("medicine", "Medicine", new List<string>{ "honey" }, false, "resource_medicine")},
        {"wood", new Resource("wood", "Wood", new List<string>{ "wood" }, false, "resource_wood")}
    };
}
