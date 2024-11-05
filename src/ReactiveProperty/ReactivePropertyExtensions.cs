using R3;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace R3Utility;
public static class ReactivePropertyExtensions
{
    /// <summary>
    /// Convert INotifyPropertyChanged to BindableReactiveProperty.
    /// `propertySelector1` must be a Func specifying a simple property. For example, it extracts "Foo" from `x => x.Foo`.
    /// </summary>
    public static BindableReactiveProperty<TProperty> ToTwoWayBindableReactiveProperty<T, TProperty>(this T value,
    Expression<Func<T, TProperty>> propertySelector,
        bool pushCurrentValueOnSubscribe = true,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(propertySelector))] string? expr = null)
        where T : INotifyPropertyChanged
    {
        if (expr == null) throw new ArgumentNullException(expr);

        var propertyName = expr!.Substring(expr.LastIndexOf('.') + 1);
        var setter = AccessorCache<T>.LookupSet(propertySelector, propertyName);

        var rp = value.ObservePropertyChanged(propertySelector.Compile(), pushCurrentValueOnSubscribe, cancellationToken, expr)!.ToBindableReactiveProperty();
        rp.Subscribe(x => setter(value, x!));

        return rp!;
    }

    /// <summary>
    /// Convert INotifyPropertyChanged to BindableReactiveProperty.
    /// `propertySelector1` and `propertySelector2` must be a Func specifying a simple property. For example, it extracts "Foo" from `x => x.Foo`.
    /// </summary>
    public static BindableReactiveProperty<TProperty2> ToTwoWayBindableReactiveProperty<T, TProperty1, TProperty2>(this T value,
        Func<T, TProperty1?> propertySelector1,
        Func<TProperty1, TProperty2> propertySelector2,
        bool pushCurrentValueOnSubscribe = true,
        CancellationToken cancellationToken = default,
        [CallerArgumentExpression(nameof(propertySelector1))] string? propertySelector1Expr = null,
        [CallerArgumentExpression(nameof(propertySelector2))] string? propertySelector2Expr = null)
        where T : INotifyPropertyChanged
        where TProperty1 : INotifyPropertyChanged
    {
        if (propertySelector1Expr == null) throw new ArgumentNullException(propertySelector1Expr);
        if (propertySelector2Expr == null) throw new ArgumentNullException(propertySelector2Expr);

        var property1Name = propertySelector1Expr!.Substring(propertySelector1Expr.LastIndexOf('.') + 1);
        var property2Name = propertySelector2Expr!.Substring(propertySelector2Expr.LastIndexOf('.') + 1);
        var rp = value.ObservePropertyChanged(propertySelector1,
                                              propertySelector2,
                                              pushCurrentValueOnSubscribe,
                                              cancellationToken,
                                              propertySelector1Expr,
                                              propertySelector2Expr)!.ToBindableReactiveProperty();

        var propertyInfo1 = AccessorCache<T>.GetCachedPropertyInfo(property1Name);
        var propertyInfo2 = AccessorCache<TProperty1>.GetCachedPropertyInfo(property2Name);
        rp.Subscribe(x =>
        {
            if (propertyInfo1?.GetValue(value) is TProperty1 property1Value)
            {
                propertyInfo2?.SetValue(property1Value, x);
            }
        });

        return rp!;
    }

    /// <summary>
    /// Convert INotifyPropertyChanged to BindableReactiveProperty.
    /// `propertySelector1`, `propertySelector2`, and `propertySelector3` must be a Func specifying a simple property. For example, it extracts "Foo" from `x => x.Foo`.
    /// </summary>
    public static BindableReactiveProperty<TProperty3> ToTwoWayBindableReactiveProperty<T, TProperty1, TProperty2, TProperty3>(this T value,
        Func<T, TProperty1?> propertySelector1,
        Func<TProperty1, TProperty2?> propertySelector2,
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
        if (propertySelector1Expr == null) throw new ArgumentNullException(propertySelector1Expr);
        if (propertySelector2Expr == null) throw new ArgumentNullException(propertySelector2Expr);
        if (propertySelector3Expr == null) throw new ArgumentNullException(propertySelector3Expr);

        var property1Name = propertySelector1Expr!.Substring(propertySelector1Expr.LastIndexOf('.') + 1);
        var property2Name = propertySelector2Expr!.Substring(propertySelector2Expr.LastIndexOf('.') + 1);
        var property3Name = propertySelector3Expr!.Substring(propertySelector3Expr.LastIndexOf('.') + 1);

        var rp = value.ObservePropertyChanged(propertySelector1,
                                              propertySelector2,
                                              propertySelector3,
                                              pushCurrentValueOnSubscribe,
                                              cancellationToken,
                                              propertySelector1Expr,
                                              propertySelector2Expr,
                                              propertySelector3Expr)!.ToBindableReactiveProperty();

        var propertyInfo1 = typeof(T).GetProperty(property1Name);
        var propertyInfo2 = typeof(TProperty1).GetProperty(property2Name);
        var propertyInfo3 = typeof(TProperty2).GetProperty(property3Name);
        rp.Subscribe(x =>
        {
            if (propertyInfo1?.GetValue(value) is TProperty1 property1Value &&
            propertyInfo2?.GetValue(property1Value) is TProperty2 property2Value)
            {
                propertyInfo3?.SetValue(property2Value, x);
            }
        });
        return rp!;
    }
}
