using System.Collections;
using System.Collections.Generic;

public static class ResrouceManager
{
    private static GlobalStorage gs;

    public static Dictionary<string, Resource> resourceDictionary = new Dictionary<string, Resource>
    {
        {"food",  new Resource("food", "Food", new List<string>{ "berry", "bread"}, true, gs)},//, new Condition(0, Condition.Operation.greaterThan)), gs},
        {"medicine", new Resource("medicine", "Medicine", new List<string>{ "honey" }, false, gs)}
    };

    public static int GetResourceCount(string resourceName)
    {
        return 0;
    }
}
