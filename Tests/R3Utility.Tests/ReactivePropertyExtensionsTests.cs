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

        bindable.Dispose();
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
        bindable.Dispose();
    }

    [Fact]
    public void ToTwoWayBindableReactiveProperty_ThreeLevels_ShouldUpdateDeepNestedProperty()
    {
        var deepInnerObject = new DeepInner { DeepNestedStringValue = "initial" };
        var innerObject = new Inner { DeepInnerObject = deepInnerObject };
        var outerObject = new Outer { InnerObject = innerObject };

        var bindable = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.DeepInnerObject,
            z => z.DeepNestedStringValue,
            pushCurrentValueOnSubscribe: true);

        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.Should().Be("initial");
        bindable.Value.Should().Be("initial");

        bindable.Value = "updated";
        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.Should().Be("updated");
        bindable.Value.Should().Be("updated");

        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue = "finish";
        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.Should().Be("finish");
        bindable.Value.Should().Be("finish");
        bindable.Dispose();
    }
}
