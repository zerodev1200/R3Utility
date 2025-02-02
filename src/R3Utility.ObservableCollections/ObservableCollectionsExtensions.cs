using ObservableCollections;
using R3;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace R3Utility.ObservableCollections;
public static class ObservableCollectionsExtensions
{
    /// <summary>
    /// Observes a specific property of each element in a collection and provides a sequence of property values.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <typeparam name="TProperty">The type of the property being observed.</typeparam>
    /// <param name="source">The collection whose elements' properties are being observed.</param>
    /// <param name="propertySelector">An expression to select the property to observe from each element. must be a Func specifying a simple property. For example, it extracts "Foo" from `x => x.Foo`.</param>
    /// <param name="pushCurrentValueOnSubscribe">Specifies whether the current property value should be pushed immediately upon subscription. Defaults to <c>true</c>.</param>
    /// <param name="cancellationToken">A token to observe while waiting for task completion. Defaults to <c>CancellationToken.None</c>.</param>
    /// <param name="expr">The expression text for the property selector, provided automatically by the compiler. This parameter is optional.</param>
    /// <returns>An observable sequence of <see cref="PropertyPack{T, TProperty}"/> objects containing the observed property values.</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static Observable<PropertyPack<T, TProperty>> ObserveElementProperty<T, TProperty>(
        this IObservableCollection<T> source,
        Func<T, TProperty> propertySelector,
        bool pushCurrentValueOnSubscribe = true,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(propertySelector))] string? expr = null)
        where T : INotifyPropertyChanged
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (expr == null) throw new ArgumentNullException(expr);

        var propertyName = expr.Substring(expr.LastIndexOf('.') + 1);

        return CollectionUtilities.ObserveElementCore<T, PropertyPack<T, TProperty>>
        (
            source,
            (x, observer) => x.ObservePropertyChanged(propertySelector, pushCurrentValueOnSubscribe, cancellationToken, expr)
                              .Subscribe(y =>
                              {
                                  var pair = PropertyPack.Create(x, propertyName, y);
                                  observer.OnNext(pair);
                              })
        );
    }

    /// <summary>
    /// Observes specified properties of each element in a collection and provides a sequence of the observed property values.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <typeparam name="TProperty1">The type of the first property being observed.</typeparam>
    /// <typeparam name="TProperty2">The type of the second property being observed.</typeparam>
    /// <param name="source">The collection whose elements' properties are observed.</param>
    /// <param name="propertySelector1">A selector for the first property to observe, specified as a simple property accessor (e.g., <c>x => x.Foo</c>).</param>
    /// <param name="propertySelector2">A selector for the second property to observe, specified as a simple property accessor.</param>
    /// <param name="pushCurrentValueOnSubscribe">Indicates whether the current property value should be emitted immediately upon subscription. Defaults to <c>true</c>.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the observation. Defaults to <c>CancellationToken.None</c>.</param>
    /// <param name="propertySelector1Expr">The source expression for <paramref name="propertySelector1"/>, provided automatically by the compiler. Optional.</param>
    /// <param name="propertySelector2Expr">The source expression for <paramref name="propertySelector2"/>, provided automatically by the compiler. Optional.</param>
    /// <returns>An observable sequence of <see cref="PropertyPack{T, TProperty2}"/> objects representing the observed property values.</returns>
    /// <exception cref="ArgumentNullException" />
    public static Observable<PropertyPack<T, TProperty2>> ObserveElementProperty<T, TProperty1, TProperty2>(
        this IObservableCollection<T> source,
        Func<T, TProperty1?> propertySelector1,
        Func<TProperty1, TProperty2> propertySelector2,
        bool pushCurrentValueOnSubscribe = true,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(propertySelector1))] string? propertySelector1Expr = null,
        [CallerArgumentExpression(nameof(propertySelector2))] string? propertySelector2Expr = null)
        where T : INotifyPropertyChanged
        where TProperty1 : INotifyPropertyChanged
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (propertySelector1Expr == null) throw new ArgumentNullException(propertySelector1Expr);
        if (propertySelector2Expr == null) throw new ArgumentNullException(propertySelector2Expr);

        var property1Name = propertySelector1Expr.Substring(propertySelector1Expr.LastIndexOf('.') + 1);
        var property2Name = propertySelector2Expr.Substring(propertySelector2Expr.LastIndexOf('.') + 1);

        return CollectionUtilities.ObserveElementCore<T, PropertyPack<T, TProperty2>>
        (
            source,
            (x, observer) => x.ObservePropertyChanged(
                                    propertySelector1, propertySelector2,
                                    pushCurrentValueOnSubscribe,
                                    cancellationToken,
                                    propertySelector1Expr, propertySelector2Expr)
                              .Subscribe(y =>
                              {
                                  var pair = PropertyPack.Create(x, $"{property1Name}.{property2Name}", y);
                                  observer.OnNext(pair);
                              })
        );
    }

    /// <summary>
    /// Observes specified properties of each element in a collection and provides a sequence of the observed property values.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <typeparam name="TProperty1">The type of the first property being observed.</typeparam>
    /// <typeparam name="TProperty2">The type of the second property being observed.</typeparam>
    /// <typeparam name="TProperty3">The type of the third property being observed.</typeparam>
    /// <param name="source">The collection whose elements' properties are observed.</param>
    /// <param name="propertySelector1">A selector for the first property to observe, specified as a simple property accessor (e.g., <c>x => x.Foo</c>).</param>
    /// <param name="propertySelector2">A selector for the second property to observe, specified as a simple property accessor.</param>
    /// <param name="propertySelector3">A selector for the third property to observe, specified as a simple property accessor.</param>
    /// <param name="pushCurrentValueOnSubscribe">Indicates whether the current property value should be emitted immediately upon subscription. Defaults to <c>true</c>.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the observation. Defaults to <c>CancellationToken.None</c>.</param>
    /// <param name="propertySelector1Expr">The source expression for <paramref name="propertySelector1"/>, provided automatically by the compiler. Optional.</param>
    /// <param name="propertySelector2Expr">The source expression for <paramref name="propertySelector2"/>, provided automatically by the compiler. Optional.</param>
    /// <param name="propertySelector3Expr">The source expression for <paramref name="propertySelector3"/>, provided automatically by the compiler. Optional.</param>
    /// <returns>An observable sequence of <see cref="PropertyPack{T, TProperty3}"/> objects representing the observed property values.</returns>
    /// <exception cref="ArgumentNullException" />

    public static Observable<PropertyPack<T, TProperty3>> ObserveElementProperty<T, TProperty1, TProperty2, TProperty3>(
        this IObservableCollection<T> source,
        Func<T, TProperty1?> propertySelector1,
        Func<TProperty1, TProperty2> propertySelector2,
        Func<TProperty2, TProperty3> propertySelector3,
        bool pushCurrentValueOnSubscribe = true,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(propertySelector1))] string? propertySelector1Expr = null,
        [CallerArgumentExpression(nameof(propertySelector2))] string? propertySelector2Expr = null,
        [CallerArgumentExpression(nameof(propertySelector3))] string? propertySelector3Expr = null)
        where T : INotifyPropertyChanged
        where TProperty1 : INotifyPropertyChanged
        where TProperty2 : INotifyPropertyChanged
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (propertySelector1Expr == null) throw new ArgumentNullException(propertySelector1Expr);
        if (propertySelector2Expr == null) throw new ArgumentNullException(propertySelector2Expr);
        if (propertySelector3Expr == null) throw new ArgumentNullException(propertySelector3Expr);

        var property1Name = propertySelector1Expr.Substring(propertySelector1Expr.LastIndexOf('.') + 1);
        var property2Name = propertySelector2Expr.Substring(propertySelector2Expr.LastIndexOf('.') + 1);
        var property3Name = propertySelector3Expr.Substring(propertySelector3Expr.LastIndexOf('.') + 1);

        return CollectionUtilities.ObserveElementCore<T, PropertyPack<T, TProperty3>>
        (
            source,
            (x, observer) => x.ObservePropertyChanged(
                                    propertySelector1, propertySelector2, propertySelector3,
                                    pushCurrentValueOnSubscribe,
                                    cancellationToken,
                                    propertySelector1Expr, propertySelector2Expr, propertySelector3Expr)
                              .Subscribe(y =>
                              {
                                  var pair = PropertyPack.Create(x, $"{property1Name}.{property2Name}.{property3Name}", y);
                                  observer.OnNext(pair);
                              })
        );
    }
}
