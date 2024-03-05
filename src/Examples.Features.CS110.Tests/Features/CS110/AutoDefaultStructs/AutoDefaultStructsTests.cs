namespace Examples.Features.CS110.AutoDefaultStructs;

/// <summary>
/// Tests for Auto-default structs in C# 11.0.
/// </summary>
public class AutoDefaultStructsTests
{
    [Fact]
    public void BasicUsage()
    {
        // initialization default constructor.
        var actual1 = new MyStruct();

        using (new AssertionScope())
        {
            actual1.Id.Should().Be(0);
            actual1.Name.Should().BeNull();
        }

        // No explicit initialization required.
        var actual2 = new Sample();

        using (new AssertionScope())
        {
            actual2.X.Should().Be(1);
            actual2.Y.Should().Be(0);
            actual2.Z.Should().BeNull();
        }

        return;
    }

    // before C# 11.0

    private struct MyStruct
    {
#pragma warning disable CS0649 // Field 'xxx' is never assigned to, and will always have its default value xx
        public int Id;
        public string? Name;
#pragma warning restore CS0649
    }

    // C# 11.0 feature

#pragma warning disable IDE0250 // Struct can be made 'readonly'

    struct Sample
    {
        public int X { get; } = 1;
        public int Y { get; }
        public string? Z { get; }

        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Y' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property.
        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Z' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property.
        public Sample() { }

        // C# 10.0 : error CS0843: Auto-implemented property 'AutoDefaultStructsTests.Sample.Y' must be fully assigned before control is returned to the caller.Consider updating to language version '11.0' to auto-default the property. [/workspaces/examples-dotnet-features/src/Examples.Features.CS100.Tests/Examples.Features.CS100.Tests.csproj]
        public Sample(string z) => Z = z;

        // C# 10.0 : OK
        public Sample(int y, string z) => (Y, Z) = (y, z);

    }

#pragma warning restore IDE0250

}
