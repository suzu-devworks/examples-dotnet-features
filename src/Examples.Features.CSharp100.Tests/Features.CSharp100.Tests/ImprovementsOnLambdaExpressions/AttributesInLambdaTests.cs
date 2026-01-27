using System.Diagnostics.CodeAnalysis;

namespace Examples.Features.CSharp100.Tests.ImprovementsOnLambdaExpressions;

/// <summary>
/// Tests for Attributes of Improvements on lambda expressions in C# 10.0.
/// </summary>
public class AttributesInLambdaTests
{
    [Fact]
    public void When_AttributesAddedToLambda_Then_AttributeDetected()
    {
        // C# 9.0 : Tests.cs(16,41): error CS8773: Feature 'lambda attributes' is not available in C# 9.0. Please use language version 10.0 or greater.
        Func<string?, int?> parse = [ProvidesNullCheck] (s) => (s is not null) ? int.Parse(s) : null;

        var attrs = parse.Method.GetCustomAttributes(inherit: false)
            .OfType<ProvidesNullCheckAttribute>();
        var has = attrs.Any();

        Assert.True(has);
    }

    [Fact]
    public void When_AttributesAddedToLambdaParameters_Then_AllAttributesDetected()
    {
        // C# 9.0 : error CS8773: Feature 'lambda attributes' is not available in C# 9.0. Please use language version 10.0 or greater.
        var concat = ([DisallowNull] string a, [DisallowNull] string b) => a + b;

        var paramAttrs = concat.Method.GetParameters()
            .SelectMany(p => p.GetCustomAttributes(inherit: false)
                .OfType<DisallowNullAttribute>());

        Assert.Equal(2, paramAttrs.Count());

        // C# 9.0 : error CS8773: Feature 'lambda attributes' is not available in C# 9.0. Please use language version 10.0 or greater.
        var inc = [return: NotNullIfNotNull(nameof(s))] (int? s) => s.HasValue ? s++ : null;

        var returnAttrs = inc.Method.ReturnTypeCustomAttributes
                    .GetCustomAttributes(inherit: false)
                    .OfType<NotNullIfNotNullAttribute>();

        Assert.Single(returnAttrs);
    }

    [AttributeUsage(AttributeTargets.Method)]
    private class ProvidesNullCheckAttribute : Attribute
    {
    }

}

