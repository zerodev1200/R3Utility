
using System.ComponentModel;

namespace R3Utility.Tests;
// Helper classes for testing
internal class TestClass : INotifyPropertyChanged
{
    private int _value;
    public event PropertyChangedEventHandler? PropertyChanged;

    public int Value
    {
        get => _value;
        set
        {
            _value = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }
    }
}

internal class Outer : INotifyPropertyChanged
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

internal class Inner : INotifyPropertyChanged
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

internal class DeepInner : INotifyPropertyChanged
{
    private string _deepNestedStringValue = string.Empty;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string DeepNestedStringValue
    {
        get => _deepNestedStringValue;
        set
        {
            _deepNestedStringValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeepNestedStringValue)));
        }
    }

    private int _deepNestedIntValue = 0;
    public int DeepNestedIntValue
    {
        get => _deepNestedIntValue;
        set
        {
            _deepNestedIntValue = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DeepNestedIntValue)));
        }
    }
}
