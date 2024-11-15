# R3Utility

A utility library for [Cysharp/R3](https://github.com/Cysharp/R3) that provides enhanced reactive programming capabilities, focusing on validation and property binding.

## Features

### Two-Way Binding Extensions
- Convert INotifyPropertyChanged properties to BindableReactiveProperty
- Automatic value propagation in both directions
- Support for deep property path binding (up to 3 levels)

### Reactive Validation
- Combine multiple boolean observables with various logical operations
- Support for creating executable command sources based on validation states

## API Reference

### ReactivePropertyExtensions

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `ToTwoWayBindableReactiveProperty<T, TProperty>` | `T value, Expression<Func<T, TProperty>> propertySelector` | `BindableReactiveProperty<TProperty>` | Converts a single property to two-way bindable reactive property |
| `ToTwoWayBindableReactiveProperty<T, TProperty1, TProperty2>` | `T value, Func<T, TProperty1?> propertySelector1, Func<TProperty1, TProperty2> propertySelector2` | `BindableReactiveProperty<TProperty2>` | Converts a nested property (2 levels) to two-way bindable reactive property |
| `ToTwoWayBindableReactiveProperty<T, TProperty1, TProperty2, TProperty3>` | `T value, Func<T, TProperty1?> propertySelector1, Func<TProperty1, TProperty2?> propertySelector2, Func<TProperty2, TProperty3> propertySelector3` | `BindableReactiveProperty<TProperty3>` | Converts a deeply nested property (3 levels) to two-way bindable reactive property |

### ReactiveValidationHelper

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `CombineLatestValuesAreAllFalse` | `IEnumerable<Observable<bool>>` | `Observable<bool>` | Combines multiple observables and returns true only when all source values are false |
| `CombineLatestValuesAreAllTrue` | `IEnumerable<Observable<bool>>` | `Observable<bool>` | Combines multiple observables and returns true only when all source values are true |
| `CombineLatestValuesAreAnyFalse` | `IEnumerable<Observable<bool>>` | `Observable<bool>` | Combines multiple observables and returns true when any source value is false |
| `CombineLatestValuesAreAnyTrue` | `IEnumerable<Observable<bool>>` | `Observable<bool>` | Combines multiple observables and returns true when any source value is true |
| `CreateCanExecuteSource` | `IBindableReactiveProperty[]` | `Observable<bool>` | Creates an observable that monitors HasErrors property of multiple BindableReactiveProperty instances |

## Usage Examples

### Two-Way Binding

```csharp
// Create a BindableReactiveProperty<T> that is two-way bound
BindableReactiveProperty<string> name = item.ToTwoWayBindableReactiveProperty(x => x.Name);

// This will also update the Name property of the item object
name.Value = "X"; //item.Name = "X"

// This will also update the Value property of the BindableReactiveProperty
item.Name = "Y"; //name.Value = "Y"


// Deep property path binding
BindableReactiveProperty<string> nestedProperty = viewModel.ToTwoWayBindableReactiveProperty(
    x => x.User,
    x => x.Profile,
    x => x.DisplayName
);
```

### Validation Helpers

```csharp
// Combine multiple validation states
Observable<bool> isValid = ReactiveValidationHelper.CombineLatestValuesAreAllTrue(
      emailValidation,
      passwordValidation,
      termsAccepted
);

// Create executable command source
Observable<bool> canExecute = ReactiveValidationHelper.CreateCanExecuteSource(
      emailProperty,
      passwordProperty
);
command = canExecute.ToReactiveCommand();
```



## Installation

```
dotnet add package R3Utility
```

## License
R3Utility is distributed under a free and open-source license. Feel free to use it in your projects!


## Contributions
We welcome contributions to the R3Utility project! If you have any suggestions, bug reports, or feature requests, please feel free to open an issue or submit a pull request.
We're excited to make R3Utility better together.
