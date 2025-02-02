
namespace R3Utility.ObservableCollections;

/// <summary>
/// Represents property and instance package.
/// </summary>
/// <typeparam name="T">Type of instance</typeparam>
/// <typeparam name="TValue">Type of property value</typeparam>
public class PropertyPack<T, TValue>
{
    #region Properies

    /// <summary>
    /// Gets instance which has property.
    /// </summary>
    public T Instance { get; }

    /// <summary>
    /// Gets target propertyName
    /// </summary>
    public string PropertyName { get; }

    /// <summary>
    /// Gets target property value.
    /// </summary>
    public TValue Value { get; }

    #endregion Properies

    #region Constructor

    /// <summary>
    /// Create instance.
    /// </summary>
    /// <param name="type">Target instance</param>
    /// <param name="propertyName">Target property info</param>
    /// <param name="value">Property value</param>
    internal PropertyPack(T type, string propertyName, TValue value)
    {
        if (type == null) throw new ArgumentNullException(nameof(type));

        Instance = type;
        PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        Value = value;
    }

    #endregion Constructor
}

/// <summary>
/// Provides PropertyPack static members.
/// </summary>
internal static class PropertyPack
{
    /// <summary>
    /// Create instance.
    /// </summary>
    /// <param name="type">Target instance</param>
    /// <param name="propertyName">Target property info</param>
    /// <param name="value">Property value</param>
    /// <returns>Created instance</returns>
    public static PropertyPack<T, TValue> Create<T, TValue>(T type, string propertyName, TValue value) =>
        new(type, propertyName, value);
}
