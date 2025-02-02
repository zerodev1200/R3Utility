using System.Linq.Expressions;
using System.Reflection;

namespace R3Utility;
internal static class AccessorCache<T>
{
    private static readonly Dictionary<string, PropertyInfo?> PropertyCache = [];

    internal static PropertyInfo? GetCachedPropertyInfo(string propertyName)
    {
        if (!PropertyCache.TryGetValue(propertyName, out var propertyInfo))
        {
            propertyInfo = typeof(T).GetProperty(propertyName);
            PropertyCache[propertyName] = propertyInfo;
        }
        return propertyInfo;
    }
}
