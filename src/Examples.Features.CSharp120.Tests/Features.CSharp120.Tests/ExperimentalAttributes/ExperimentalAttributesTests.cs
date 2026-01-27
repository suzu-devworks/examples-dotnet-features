using System.Diagnostics.CodeAnalysis;

namespace Examples.Features.CSharp120.Tests.ExperimentalAttributes;

/// <summary>
/// Tests for Experimental attribute in C# 12.0.
/// </summary>
public class ExperimentalAttributesTests
{
    [Experimental("Foo001")]
    private class Fixture;

    [Fact]
    public void When_UsingExperimentalAttribute_Then_FixtureInstantiates()
    {
#pragma warning disable Foo001 // 'feature' is for evaluation purposes only and is subject to change or removal in future updates.
        Fixture fixture = new();
#pragma warning restore Foo001

        Assert.NotNull(fixture);
    }
}
