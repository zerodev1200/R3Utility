# R3Utility
[![Nuget downloads](https://img.shields.io/nuget/v/R3Utility.svg)](https://www.nuget.org/packages/R3Utility)
[![NuGet](https://img.shields.io/nuget/dt/R3Utility.svg)](https://www.nuget.org/packages/R3Utility)  
A utility library for [Cysharp/R3](https://github.com/Cysharp/R3) that provides enhanced reactive programming capabilities, focusing on validation, property binding, and UI event observation.

## 🚨 Breaking Changes (v0.3.0)

Starting from version 0.3.0, `ObservableCollectionsExtensions` has been moved to a separate package: [R3Utility.ObservableCollections](https://www.nuget.org/packages/R3Utility.ObservableCollections).
If you were using `ObserveElementProperty`, please install the new package and update your imports accordingly.

```
dotnet add package R3Utility.ObservableCollections
```
```
using R3Utility.ObservableCollections;
```

## Features

### Two-Way Binding Extensions
- Convert INotifyPropertyChanged properties to BindableReactiveProperty
- Automatic value propagation in both directions
- Support for deep property path binding (up to 3 levels)

### Reactive Validation
- Combine multiple boolean observables with various logical operations
- Support for creating executable command sources based on validation states

### Observable Element Property Monitoring  (R3Utility.ObservableCollections)
- Observe changes in specific properties of elements within a collection.
- Customizable option, `pushCurrentValueOnSubscribe` to control initial value emission.
- Support for deep property path binding (up to 3 levels)

### WinForms UI Event Observables (R3Utility.WinForms)

Convert common UI events into observables
Supports TextBox, Button, CheckBox, ComboBox, Control drag-and-drop events, mouse events, focus events, and DataGridView events
All methods accept an optional CancellationToken parameter

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
| `CreateCanExecuteSource` | `IBindableReactiveProperty[]` | `Observable<bool>` | Creates an observable that monitors HasErrors property of multiple BindableReactiveProperty instances. An overload with `bool forceValidationOnStart = false` allows skipping initial validation. |

### ObservableCollectionsExtensions(R3Utility.ObservableCollections)

| Method | Parameters | Return Type | Description |
|--------|------------|-------------|-------------|
| `ObserveElementProperty<T, TProperty>` | `IObservableCollection<T> source, Func<T, TProperty> propertySelector, bool pushCurrentValueOnSubscribe = true, CancellationToken cancellationToken = default` | `Observable<PropertyPack<T, TProperty>>` | Observes a specific property of each element in a collection and emits its values. |
| `ObserveElementProperty<T, TProperty1, TProperty2>` | `IObservableCollection<T> source, Func<T, TProperty1?> propertySelector1, Func<TProperty1, TProperty2> propertySelector2, bool pushCurrentValueOnSubscribe = true, CancellationToken cancellationToken = default` | `Observable<PropertyPack<T, TProperty2>>` | Observes a nested property (2 levels) in a collection and emits its values. |
| `ObserveElementProperty<T, TProperty1, TProperty2, TProperty3>` | `IObservableCollection<T> source, Func<T, TProperty1?> propertySelector1, Func<TProperty1, TProperty2> propertySelector2, Func<TProperty2, TProperty3> propertySelector3, bool pushCurrentValueOnSubscribe = true, CancellationToken cancellationToken = default` | `Observable<PropertyPack<T, TProperty3>>` | Observes a deeply nested property (3 levels) in a collection and emits its values. |


### WinForms UIComponentExtensions (R3Utility.WinForms)

| Method | Parameters | Return Type |
|--------|------------|-------------|
| `TextChangedAsObservable` | `TextBoxBase` | `Observable<EventArgs>` |
| `ClickAsObservable` | `Button` | `Observable<EventArgs>` |
| `CheckedChangedAsObservable` | `CheckBox` | `Observable<EventArgs>` |
| `SelectedIndexChangedAsObservable` | `ComboBox` | `Observable<EventArgs>` |
| `SelectionChangeCommittedAsObservable` | `ComboBox` | `Observable<EventArgs>` |
| `DragEnterAsObservable` | `Control` | `Observable<DragEventArgs>` |
| `DragOverAsObservable` | `Control` | `Observable<DragEventArgs>` |
| `DragLeaveAsObservable` | `Control` | `Observable<EventArgs>` |
| `DragDropAsObservable` | `Control` | `Observable<DragEventArgs>` |
| `MouseDownAsObservable` | `Control` | `Observable<MouseEventArgs>` |
| `MouseMoveAsObservable` | `Control` | `Observable<MouseEventArgs>` |
| `MouseUpAsObservable` | `Control` | `Observable<MouseEventArgs>` |
| `EnterAsObservable` | `Control` | `Observable<EventArgs>` |
| `LeaveAsObservable` | `Control` | `Observable<EventArgs>` |
| `GotFocusAsObservable` | `Control` | `Observable<EventArgs>` |
| `LostFocusAsObservable` | `Control` | `Observable<EventArgs>` |
| `CellValueChangedAsObservable` | `DataGridView` | `Observable<DataGridViewCellEventArgs>` |
| `CellClickAsObservable` | `DataGridView` | `Observable<DataGridViewCellEventArgs>` |
| `CellDoubleClickAsObservable` | `DataGridView` | `Observable<DataGridViewCellEventArgs>` |
| `ColumnHeaderMouseClickAsObservable` | `DataGridView` | `Observable<DataGridViewCellMouseEventArgs>` |
| `RowHeaderMouseClickAsObservable` | `DataGridView` | `Observable<DataGridViewCellMouseEventArgs>` |
| `SelectionChangedAsObservable` | `DataGridView` | `Observable<EventArgs>` |
| `RowEnterAsObservable` | `DataGridView` | `Observable<DataGridViewCellEventArgs>` |
| `RowLeaveAsObservable` | `DataGridView` | `Observable<DataGridViewCellEventArgs>` |

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

### Observing Properties in a Collection

```csharp
using ObservableCollections;
using R3;
using R3Utility.ObservableCollections;

ObservableList<Item> collection = [];
Item item1 = new() { Name = "foo" };

collection.Add(item1);
var disposable = collection.ObserveElementProperty(x => x.Name, pushCurrentValueOnSubscribe: false)
                           .Subscribe(property => Console.WriteLine($"Instance:{property.Instance}, propertyName:{property.PropertyName}, Value:{property.Value}"));
        
//Changes are output. `Instance:WpfApp1.Item, propertyName:Name, Value:bar`
item1.Name = "bar";

disposable.Dispose();
```

## Installation

```
dotnet add package R3Utility
dotnet add package R3Utility.ObservableCollections
dotnet add package R3Utility.WinForms
```

## License
R3Utility is distributed under a free and open-source license. Feel free to use it in your projects!

## Contributions
We welcome contributions to the R3Utility project! If you have any suggestions, bug reports, or feature requests, please feel free to open an issue or submit a pull request.
We're excited to make R3Utility better together.