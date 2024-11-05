using System.Linq.Expressions;
using System.Reflection;

namespace R3Utility;
internal static class AccessorCache<T>
{
    private static readonly Dictionary<string, Delegate> SetCache = [];
    /// <summary>
    /// Lookups the set.
    /// </summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="propertySelector">The property selector.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    internal static Action<T, TProperty> LookupSet<TProperty>(Expression<Func<T, TProperty>> propertySelector, string propertyName)
    {
        Delegate? accessor;

        lock (SetCache)
        {
            if (!SetCache.TryGetValue(propertyName, out accessor))
            {
                accessor = CreateSetAccessor(propertySelector);
                SetCache.Add(propertyName, accessor);
            }
        }

        return (Action<T, TProperty>)accessor;
    }

    private static Delegate CreateSetAccessor<TProperty>(Expression<Func<T, TProperty>> propertySelector)
    {
        var propertyInfo = (PropertyInfo)((MemberExpression)propertySelector.Body).Member;
        var selfParameter = Expression.Parameter(typeof(T), "self");
        var valueParameter = Expression.Parameter(typeof(TProperty), "value");
        var body = Expression.Assign(Expression.Property(selfParameter, propertyInfo), valueParameter);
        var lambda = Expression.Lambda<Action<T, TProperty>>(body, selfParameter, valueParameter);
        return lambda.Compile();
    }


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
