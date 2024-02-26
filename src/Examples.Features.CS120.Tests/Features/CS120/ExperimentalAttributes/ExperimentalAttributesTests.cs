using System.Diagnostics.CodeAnalysis;

namespace Examples.Features.CS120.ExperimentalAttributes;

/// <summary>
/// Tests for Experimental attribute in C# 12.0.
/// </summary>
public class ExperimentalAttributesTests
{
    [Experimental("Foo001")]
    private class Fixture;

    [Fact]
    public void BasicUsage()
    {
#pragma warning disable Foo001 // 'feature' is for evaluation purposes only and is subject to change or removal in future updates.
        Fixture fixture = new();
#pragma warning restore Foo001

        fixture.Should().NotBeNull();

        return;
    }
}
