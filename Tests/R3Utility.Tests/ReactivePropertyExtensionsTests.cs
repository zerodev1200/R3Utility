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

        bindable.Value.ShouldBe(10);
        testObject.Value.ShouldBe(10);

        testObject.Value = 25;
        bindable.Value.ShouldBe(25);
        testObject.Value.ShouldBe(25);

        bindable.Value = 100;
        testObject.Value.ShouldBe(100);
        bindable.Value.ShouldBe(100);

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

        outerObject.InnerObject.NestedIntValue.ShouldBe(5);
        bindable.Value.ShouldBe(5);

        bindable.Value = 15;
        outerObject.InnerObject.NestedIntValue.ShouldBe(15);
        bindable.Value.ShouldBe(15);

        outerObject.InnerObject.NestedIntValue = 75;
        outerObject.InnerObject.NestedIntValue.ShouldBe(75);
        bindable.Value.ShouldBe(75);


        var bindable2 = outerObject.ToTwoWayBindableReactiveProperty(
            x => x.InnerObject,
            y => y.NestedStringValue,
            pushCurrentValueOnSubscribe: true);
        outerObject.InnerObject.NestedStringValue.ShouldBe("aaa");
        bindable2.Value.ShouldBe("aaa");

        bindable2.Value = "sss";
        outerObject.InnerObject.NestedStringValue.ShouldBe("sss");
        bindable2.Value.ShouldBe("sss");

        outerObject.InnerObject.NestedStringValue = "zzz";
        outerObject.InnerObject.NestedStringValue.ShouldBe("zzz");
        bindable2.Value.ShouldBe("zzz");
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

        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.ShouldBe("initial");
        bindable.Value.ShouldBe("initial");

        bindable.Value = "updated";
        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.ShouldBe("updated");
        bindable.Value.ShouldBe("updated");

        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue = "finish";
        outerObject.InnerObject.DeepInnerObject.DeepNestedStringValue.ShouldBe("finish");
        bindable.Value.ShouldBe("finish");
        bindable.Dispose();
    }
}
