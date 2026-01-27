namespace Examples.Features.CSharp110.Tests.AutoDefaultStructs;

/// <summary>
/// Tests for Auto-default structs in C# 11.0.
/// </summary>
public class AutoDefaultStructsTests
{
    [Fact]
    public void When_BasicUsage_Then_DefaultsApplied()
    {
        // initialization default constructor.
        var actualNoInit = new MyStruct();

        Assert.Equal(0, actualNoInit.Id);
        Assert.Null(actualNoInit.Name);

        var actualWithInit = new MyStruct { Id = 1, Name = "Test" };

        Assert.Equal(1, actualWithInit.Id);
        Assert.Equal("Test", actualWithInit.Name);

        // No explicit initialization required.
        var actual2 = new Sample();

        Assert.Equal(1, actual2.X);
        Assert.Equal(0, actual2.Y);
        Assert.Null(actual2.Z);
    }

    // before C# 11.0

    private struct MyStruct
    {
        public int Id;
        public string? Name;
    }

    // C# 11.0 feature

    private struct Sample
    {
        public int X { get; } = 1;
        public int Y { get; }
        public double? Z { get; }

        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Y' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property.
        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Z' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property.
        public Sample() { }

        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Y' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property. [/workspaces/examples-dotnet-features/src/Examples.Features.CS100.Tests/Examples.Features.CS100.Tests.csproj]
        public Sample(double z) => Z = z;

        // C# 10.0 : OK
        public Sample(int y, double z) => (Y, Z) = (y, z);

    }
}
