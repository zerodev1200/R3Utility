using R3;

namespace R3Utility;

public static class ReactiveValidationHelper
{
    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true only when all source values are false.
    /// </summary>
    /// <param name="sources">Collection of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all source values are false</returns>
    public static Observable<bool> CombineLatestValuesAreAllFalse(this IEnumerable<Observable<bool>> sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.All(x => !x));
    }

    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true only when all source values are false.
    /// </summary>
    /// <param name="sources">Array of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all source values are false</returns>
    public static Observable<bool> CombineLatestValuesAreAllFalse(params Observable<bool>[] sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.All(x => !x));
    }

    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true only when all source values are true.
    /// </summary>
    /// <param name="sources">Collection of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all source values are true</returns>
    public static Observable<bool> CombineLatestValuesAreAllTrue(this IEnumerable<Observable<bool>> sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.All(x => x));
    }

    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true only when all source values are true.
    /// </summary>
    /// <param name="sources">Array of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all source values are true</returns>
    public static Observable<bool> CombineLatestValuesAreAllTrue(params Observable<bool>[] sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.All(x => x));
    }

    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true when any source value is false.
    /// </summary>
    /// <param name="sources">Collection of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when any source value is false</returns>
    public static Observable<bool> CombineLatestValuesAreAnyFalse(this IEnumerable<Observable<bool>> sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.Any(x => !x));
    }

    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true when any source value is false.
    /// </summary>
    /// <param name="sources">Array of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when any source value is false</returns>
    public static Observable<bool> CombineLatestValuesAreAnyFalse(params Observable<bool>[] sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.Any(x => !x));
    }


    /// <summary>
    /// Combines multiple Observable&lt;bool&gt; streams and returns a new Observable that emits true when any source value is true.
    /// </summary>
    /// <param name="sources">Collection of Observable&lt;bool&gt; to combine</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when any source value is true</returns>
    public static Observable<bool> CombineLatestValuesAreAnyTrue(this IEnumerable<Observable<bool>> sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.Any(x => x));
    }

    /// <summary>
    /// Combines multiple Observable streams and returns a new Observable that emits true when any source value is true.
    /// </summary>
    /// <param name="sources">Array of Observable to combine</param>
    /// <returns>An Observable that emits true when any source value is true</returns>
    public static Observable<bool> CombineLatestValuesAreAnyTrue(params Observable<bool>[] sources)
    {
        return Observable.CombineLatest(sources).Select(xs => xs.Any(x => x));
    }

    /// <summary>
    /// Creates an Observable&lt;bool&gt; that monitors HasErrors property of multiple BindableReactiveProperty instances.
    /// Returns true only when HasErrors is false for all properties.
    /// </summary>
    /// <param name="properties">Array of BindableReactiveProperty instances to monitor</param>
    /// <param name="forceInitialNotification">If true, forces the initial notification for each property.</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all properties have no errors, false otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when properties array is null or empty</exception>
    public static Observable<bool> CreateCanExecuteSource(params IBindableReactiveProperty[] properties)
    {
        return CreateCanExecuteSource(true, properties);
    }

    /// <summary>
    /// Creates an Observable&lt;bool&gt; that monitors HasErrors property of multiple BindableReactiveProperty instances.
    /// Returns true only when HasErrors is false for all properties.
    /// </summary>
    /// <param name="properties">Array of BindableReactiveProperty instances to monitor</param>
    /// <param name="forceValidationOnStart">If true, forces the initial notification for each property.</param>
    /// <returns>An Observable&lt;bool&gt; that emits true when all properties have no errors, false otherwise</returns>
    /// <exception cref="ArgumentNullException">Thrown when properties array is null or empty</exception>
    public static Observable<bool> CreateCanExecuteSource(bool forceValidationOnStart = true, params IBindableReactiveProperty[] properties)
    {
        if (properties == null || properties.Length == 0)
        {
            throw new ArgumentNullException(nameof(properties));
        }
        // Create observables for HasErrors of each property
        var errorObservables = properties
                                .Select(property =>
                                {
                                    if (!property.IsValidationEnabled)
                                    {
                                        throw new ArgumentException($"Property must have EnableValidation() called.", nameof(properties));
                                    }
                                    if (forceValidationOnStart)
                                    {
                                        property.OnNext(property.Value);
                                    }
                                    return Observable.EveryValueChanged(property, x => x.HasErrors);
                                });
        return errorObservables.CombineLatestValuesAreAllFalse();
    }
}
