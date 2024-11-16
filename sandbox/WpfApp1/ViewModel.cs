using R3;
using R3Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace WpfApp1;

public class ViewModel
{
    [Required]
    public BindableReactiveProperty<string> RP1 { get; } = new BindableReactiveProperty<string>().EnableValidation<ViewModel>();
    [Required, Range(1, 99)]
    public BindableReactiveProperty<int?> RP2 { get; } = new BindableReactiveProperty<int?>().EnableValidation<ViewModel>();
    [Required]
    public BindableReactiveProperty<string> RP3 { get; } = new BindableReactiveProperty<string>("").EnableValidation<ViewModel>();
    public ReactiveCommand SubmitCommand { get; }
    public BindableReactiveProperty<string?> A { get; } = new();
    public BindableReactiveProperty<string?> B { get; } = new();

    public ViewModel()
    {
        var source = ReactiveValidationHelper.CreateCanExecuteSource(RP1, RP2, RP3);
        SubmitCommand = source.ToReactiveCommand(x =>
        {
            Debug.WriteLine($"RP1HasErrors:{RP1.HasErrors}");
            Debug.WriteLine($"RP2HasErrors:{RP2.HasErrors}");
            Debug.WriteLine($"RP3HasErrors:{RP3.HasErrors}");
            RP1.Value = "";
            Debug.WriteLine($"RP1HasErrors:{RP1.HasErrors}");
            Debug.WriteLine($"CanExecute:{SubmitCommand!.CanExecute().ToString()}");
        });
        var item = new Item();

        A = item.ToTwoWayBindableReactiveProperty(x => x.Name)!;
        B = item.ToTwoWayBindableReactiveProperty(x => x.Name)!;

        item.Name = "a";
        Debug.WriteLine($"item.Name:{item.Name}"); //a
        Debug.WriteLine($"a:{A.Value}"); //a
        Debug.WriteLine($"b:{B.Value}"); //a

        A.Value = "b";
        Debug.WriteLine($"item.Name:{item.Name}"); //b
        Debug.WriteLine($"a:{A.Value}"); //b
        Debug.WriteLine($"b:{B.Value}"); //b

        B.Value = "c";
        Debug.WriteLine($"item.Name:{item.Name}"); //c
        Debug.WriteLine($"a:{A.Value}"); //c
        Debug.WriteLine($"b:{B.Value}"); //c

        item.Name = "d";
        Debug.WriteLine($"item.Name:{item.Name}"); //d
        Debug.WriteLine($"a:{A.Value}"); //d
        Debug.WriteLine($"b:{B.Value}"); //d

        var deepInnerObject = new DeepInner { DeepNestedValue = "initial" };
        var innerObject = new Inner { DeepInnerObject = deepInnerObject };
        var outerObject = new Outer { InnerObject = innerObject };

        var bindable = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.NestedIntValue,
            pushCurrentValueOnSubscribe: true);


        var bindable2 = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.DeepInnerObject,
            z => z.DeepNestedValue,
            pushCurrentValueOnSubscribe: true);

        var bindable3 = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.DeepInnerObject,
            pushCurrentValueOnSubscribe: true);


        var bindable4 = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.DeepInnerObject,
            z => z.DeepNestedValue,
            pushCurrentValueOnSubscribe: true);

        bindable.Value = 1;
        bindable2.Value = "1";

    }
}

public class Outer : INotifyPropertyChanged
{
    private Inner _innerObject = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    public Inner InnerObject
    {
        get => _innerObject;
        set
        {
            _innerObject = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InnerObject)));
        }
    }
}

public class Inner : INotifyPropertyChanged
{
    private int _nestedIntValue;
    private string _nestedStringValue = "";
    private DeepInner _deepInnerObject = new();
    public event PropertyChangedEventHandler? PropertyChanged;

    public int NestedIntValue
    {
        get => _nestedIntValue;
        set
        {
            _nestedIntValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NestedIntValue)));
        }
    }

    public string NestedStringValue
    {
        get => _nestedStringValue;
        set
        {
            _nestedStringValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NestedStringValue)));
        }
    }

    public DeepInner DeepInnerObject
    {
        get => _deepInnerObject;
        set
        {
            _deepInnerObject = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeepInnerObject)));
        }
    }
}

public class DeepInner : INotifyPropertyChanged
{
    private string _deepNestedValue = string.Empty;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string DeepNestedValue
    {
        get => _deepNestedValue;
        set
        {
            _deepNestedValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeepNestedValue)));
        }
    }
}