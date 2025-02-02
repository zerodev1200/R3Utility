using ObservableCollections;
using R3Utility.ObservableCollections;

namespace R3Utility.Tests;
public class ObservableCollectionsExtensionsTests
{
    [Fact]
    public void ObserveElementsPropertyTest_ObservableList()
    {
        ObservableList<TestClass> source = [];

        TestClass testObject1 = new() { Value = 10 };
        TestClass testObject2 = new() { Value = 20 };
        TestClass testObject3 = new() { Value = 30 };
        TestClass testObject4 = new() { Value = 40 };

        using var liveList = source.ObserveElementProperty(x => x.Value).Select(x => x.Value).ToLiveList();

        liveList.AssertEqual([]);

        source.Add(testObject1);    //source is 1
        liveList.AssertEqual([10]);

        testObject1.Value = 11;
        liveList.AssertEqual([10, 11]);

        source.Add(testObject2);    //source is 1,2
        liveList.AssertEqual([10, 11, 20]);

        testObject2.Value = 21;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.Remove(testObject2);    //source is 1
        testObject2.Value = 22;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.AddRange([testObject3, testObject4]); //source is 1,3,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40]);

        testObject1.Value = 15;
        testObject2.Value = 25;
        testObject3.Value = 35;
        testObject4.Value = 45;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        source.Insert(2, testObject1); //source is 1,3,1,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        testObject1.Value = 16;
        testObject2.Value = 26;
        testObject3.Value = 36;
        testObject4.Value = 46;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46]);

        source.RemoveRange(0, 2); // source is 1,4
        testObject1.Value = 17;
        testObject2.Value = 27;
        testObject3.Value = 37;
        testObject4.Value = 47;

        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);

        source.Clear();
        testObject1.Value = 18;
        testObject2.Value = 28;
        testObject3.Value = 38;
        testObject4.Value = 48;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);
    }

    [Fact]
    public void ObserveElementsPropertyTest_TwoLevels()
    {
        ObservableList<Outer> source = [];

        Outer outer1 = new();
        Outer outer2 = new();
        Outer outer3 = new();
        Outer outer4 = new();

        outer1.InnerObject.NestedIntValue = 10;
        outer2.InnerObject.NestedIntValue = 20;
        outer3.InnerObject.NestedIntValue = 30;
        outer4.InnerObject.NestedIntValue = 40;

        using var liveList = source.ObserveElementProperty(x => x.InnerObject, x => x.NestedIntValue).Select(x => x.Value).ToLiveList();

        liveList.AssertEqual([]);

        source.Add(outer1);    //source is 1
        liveList.AssertEqual([10]);

        outer1.InnerObject.NestedIntValue = 11;
        liveList.AssertEqual([10, 11]);

        source.Add(outer2);    //source is 1,2
        liveList.AssertEqual([10, 11, 20]);

        outer2.InnerObject.NestedIntValue = 21;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.Remove(outer2);    //source is 1
        outer2.InnerObject.NestedIntValue = 22;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.AddRange([outer3, outer4]); //source is 1,3,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40]);

        outer1.InnerObject.NestedIntValue = 15;
        outer2.InnerObject.NestedIntValue = 25;
        outer3.InnerObject.NestedIntValue = 35;
        outer4.InnerObject.NestedIntValue = 45;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        source.Insert(2, outer1); //source is 1,3,1,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        outer1.InnerObject.NestedIntValue = 16;
        outer2.InnerObject.NestedIntValue = 26;
        outer3.InnerObject.NestedIntValue = 36;
        outer4.InnerObject.NestedIntValue = 46;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46]);

        source.RemoveRange(0, 2); // source is 1,4
        outer1.InnerObject.NestedIntValue = 17;
        outer2.InnerObject.NestedIntValue = 27;
        outer3.InnerObject.NestedIntValue = 37;
        outer4.InnerObject.NestedIntValue = 47;

        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);

        source.Clear();
        outer1.InnerObject.NestedIntValue = 18;
        outer2.InnerObject.NestedIntValue = 28;
        outer3.InnerObject.NestedIntValue = 38;
        outer4.InnerObject.NestedIntValue = 48;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);
    }

    [Fact]
    public void ObserveElementsPropertyTest_ThreeLevels()
    {
        ObservableList<Outer> source = [];

        Outer outer1 = new();
        Outer outer2 = new();
        Outer outer3 = new();
        Outer outer4 = new();

        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 10;
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 20;
        outer3.InnerObject.DeepInnerObject.DeepNestedIntValue = 30;
        outer4.InnerObject.DeepInnerObject.DeepNestedIntValue = 40;

        using var liveList = source.ObserveElementProperty(x => x.InnerObject, x => x.DeepInnerObject, x => x.DeepNestedIntValue).Select(x => x.Value).ToLiveList();

        liveList.AssertEqual([]);

        source.Add(outer1);    //source is 1
        liveList.AssertEqual([10]);

        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 11;
        liveList.AssertEqual([10, 11]);

        source.Add(outer2);    //source is 1,2
        liveList.AssertEqual([10, 11, 20]);

        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 21;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.Remove(outer2);    //source is 1
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 22;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.AddRange([outer3, outer4]); //source is 1,3,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40]);

        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 15;
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 25;
        outer3.InnerObject.DeepInnerObject.DeepNestedIntValue = 35;
        outer4.InnerObject.DeepInnerObject.DeepNestedIntValue = 45;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        source.Insert(2, outer1); //source is 1,3,1,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 16;
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 26;
        outer3.InnerObject.DeepInnerObject.DeepNestedIntValue = 36;
        outer4.InnerObject.DeepInnerObject.DeepNestedIntValue = 46;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46]);

        source.RemoveRange(0, 2); // source is 1,4
        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 17;
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 27;
        outer3.InnerObject.DeepInnerObject.DeepNestedIntValue = 37;
        outer4.InnerObject.DeepInnerObject.DeepNestedIntValue = 47;

        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);

        source.Clear();
        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 18;
        outer2.InnerObject.DeepInnerObject.DeepNestedIntValue = 28;
        outer3.InnerObject.DeepInnerObject.DeepNestedIntValue = 38;
        outer4.InnerObject.DeepInnerObject.DeepNestedIntValue = 48;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 17, 47]);
    }

    [Fact]
    public void ObserveElementsPropertyTest_ObservableHashSet()
    {
        ObservableHashSet<TestClass> source = [];

        TestClass testObject1 = new() { Value = 10 };
        TestClass testObject2 = new() { Value = 20 };
        TestClass testObject3 = new() { Value = 30 };
        TestClass testObject4 = new() { Value = 40 };

        using var liveList = source.ObserveElementProperty(x => x.Value).Select(x => x.Value).ToLiveList();

        liveList.AssertEqual([]);

        source.Add(testObject1);    //source is 1
        liveList.AssertEqual([10]);

        testObject1.Value = 11;
        liveList.AssertEqual([10, 11]);

        source.Add(testObject2);    //source is 1,2
        liveList.AssertEqual([10, 11, 20]);

        testObject2.Value = 21;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.Remove(testObject2);    //source is 1
        testObject2.Value = 22;
        liveList.AssertEqual([10, 11, 20, 21]);

        source.AddRange([testObject3, testObject4, testObject1]); //source is 1,3,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40]);

        testObject1.Value = 15;
        testObject2.Value = 25;
        testObject3.Value = 35;
        testObject4.Value = 45;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        source.Add(testObject1); //source is 1,3,4
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45]);

        testObject1.Value = 16;
        testObject2.Value = 26;
        testObject3.Value = 36;
        testObject4.Value = 46;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46]);

        source.Remove(testObject3); // source is 1,4
        source.Remove(testObject1); // source is 4
        testObject1.Value = 17;
        testObject2.Value = 27;
        testObject3.Value = 37;
        testObject4.Value = 47;

        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 47]);

        source.Clear();
        testObject1.Value = 18;
        testObject2.Value = 28;
        testObject3.Value = 38;
        testObject4.Value = 48;
        liveList.AssertEqual([10, 11, 20, 21, 30, 40, 15, 35, 45, 16, 36, 46, 47]);
    }

    [Fact]
    public void PropertyPackTest()
    {
        ObservableList<Outer> source = [];
        Outer outer1 = new();
        outer1.InnerObject.NestedIntValue = 10;
        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 20;

        source.Add(outer1);

        object instance = new();
        string propertyName = string.Empty;
        int value = 0;
        var d1 = source.ObserveElementProperty(x => x.InnerObject, x => x.NestedIntValue)
                       .Subscribe(x =>
                       {
                           instance = x.Instance;
                           propertyName = x.PropertyName;
                           value = x.Value;
                       });

        outer1.InnerObject.NestedIntValue = 100;

        value.Should().Be(100);
        instance.GetType().Should().Be(typeof(Outer));
        propertyName.Should().Be("InnerObject.NestedIntValue");
        d1.Dispose();

        var d2 = source.ObserveElementProperty(x => x.InnerObject, x => x.DeepInnerObject, x => x.DeepNestedIntValue)
                       .Subscribe(x =>
                       {
                           instance = x.Instance;
                           propertyName = x.PropertyName;
                           value = x.Value;
                       });
        outer1.InnerObject.DeepInnerObject.DeepNestedIntValue = 200;

        value.Should().Be(200);
        instance.GetType().Should().Be(typeof(Outer));
        propertyName.Should().Be("InnerObject.DeepInnerObject.DeepNestedIntValue");
        d2.Dispose();
    }
}
