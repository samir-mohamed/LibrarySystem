namespace LibrarySystem.HelperExtensions;

public static class Utilities
{
    public static T Copy<T>(this object inObj) 
        where T : class, new()
    {
        var outObj = new T();

        foreach(var outProp in outObj.GetType().GetProperties())
        {
            if (inObj.GetType().GetProperties().Any(p => p.Name == outProp.Name))
                outObj.GetType().GetProperty(outProp.Name)?.SetValue(outObj, inObj.GetType().GetProperty(outProp.Name)?.GetValue(inObj));
        }

        return outObj;
    }
}
