namespace R3Utility.Tests;

public class TestFixture
{
    public FakeTimeProvider TimeProvider { get; set; } = new();
    public FakeFrameProvider FrameProvider { get; set; } = new();
    public TestFixture()
    {
        ObservableSystem.DefaultTimeProvider = TimeProvider;
        ObservableSystem.DefaultFrameProvider = FrameProvider;
    }
}
