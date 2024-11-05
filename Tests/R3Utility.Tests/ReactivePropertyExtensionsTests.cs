using System.ComponentModel;

namespace R3Utility.Tests;
public class ReactivePropertyExtensionsTests
{
    [Fact]
    public void ToTwoWayBindableReactiveProperty_ShouldUpdateProperty_WhenValueChanges()
    {
        var testObject = new TestClass { Value = 10 };

        var bindable = testObject.ToTwoWayBindableReactiveProperty(
            x => x.Value,
            pushCurrentValueOnSubscribe: true);

        bindable.Value.Should().Be(10);
        testObject.Value.Should().Be(10);

        testObject.Value = 25;
        bindable.Value.Should().Be(25);
        testObject.Value.Should().Be(25);

        bindable.Value = 100;
        testObject.Value.Should().Be(100);
        bindable.Value.Should().Be(100);
    }

    [Fact]
    public void ToTwoWayBindableReactiveProperty_TwoLevels_ShouldUpdateNestedProperty()
    {
        var innerObject = new Inner { NestedIntValue = 5, NestedStringValue = "aaa" };
        var outerObject = new Outer { InnerObject = innerObject };

        var bindable = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.NestedIntValue,
            pushCurrentValueOnSubscribe: true);

        outerObject.InnerObject.NestedIntValue.Should().Be(5);
        bindable.Value.Should().Be(5);

        bindable.Value = 15;
        outerObject.InnerObject.NestedIntValue.Should().Be(15);
        bindable.Value.Should().Be(15);

        outerObject.InnerObject.NestedIntValue = 75;
        outerObject.InnerObject.NestedIntValue.Should().Be(75);
        bindable.Value.Should().Be(75);


        var bindable2 = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.NestedStringValue,
            pushCurrentValueOnSubscribe: true);
        outerObject.InnerObject.NestedStringValue.Should().Be("aaa");
        bindable2.Value.Should().Be("aaa");

        bindable2.Value = "sss";
        outerObject.InnerObject.NestedStringValue.Should().Be("sss");
        bindable2.Value.Should().Be("sss");

        outerObject.InnerObject.NestedStringValue = "zzz";
        outerObject.InnerObject.NestedStringValue.Should().Be("zzz");
        bindable2.Value.Should().Be("zzz");
    }

    [Fact]
    public void ToTwoWayBindableReactiveProperty_ThreeLevels_ShouldUpdateDeepNestedProperty()
    {
        var deepInnerObject = new DeepInner { DeepNestedValue = "initial" };
        var innerObject = new Inner { DeepInnerObject = deepInnerObject };
        var outerObject = new Outer { InnerObject = innerObject };

        var bindable = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.DeepInnerObject,
            z => z.DeepNestedValue,
            pushCurrentValueOnSubscribe: true);

        outerObject.InnerObject.DeepInnerObject.DeepNestedValue.Should().Be("initial");
        bindable.Value.Should().Be("initial");

        bindable.Value = "updated";
        outerObject.InnerObject.DeepInnerObject.DeepNestedValue.Should().Be("updated");
        bindable.Value.Should().Be("updated");

        outerObject.InnerObject.DeepInnerObject.DeepNestedValue = "finish";
        outerObject.InnerObject.DeepInnerObject.DeepNestedValue.Should().Be("finish");
        bindable.Value.Should().Be("finish");
    }

    // Helper classes for testing
    public class TestClass : INotifyPropertyChanged
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

    public class Outer : INotifyPropertyChanged
    {
        private Inner? _innerObject;
        public event PropertyChangedEventHandler? PropertyChanged;

        public Inner? InnerObject
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
        private DeepInner? _deepInnerObject;
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

        public DeepInner? DeepInnerObject
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
}
