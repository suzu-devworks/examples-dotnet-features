namespace Examples.Features.CS100.ImprovementsOnLambdaExpressions;

/// <summary>
/// Tests for Explicit return type of Improvements on lambda expressions in C# 10.0.
/// </summary>
public class ExplicitReturnTypeTests
{
    [Fact]
    public void WhenReturnTypeCannotBeInferred()
    {
        // error CS8917: The delegate type could not be inferred.
        // var choose1 = (bool b) => b ? 1 : "two";

        // C# 9.0 : error CS8773: Feature 'lambda return type' is not available in C# 9.0. Please use language version 10.0 or greater.
        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        var choose2 = object (bool b) => b ? 1 : "two"; // Func<bool, object>

        choose2.Should().BeAssignableTo<Func<bool, object>>();

        // error CS8917: The delegate type could not be inferred.
        //var parse = (string s) => int.TryParse(s, out var num) ? num : null;
        var parse = int? (string s) => int.TryParse(s, out var num) ? num : null;

        parse.Should().BeAssignableTo<Func<string, int?>>();

        return;
    }
}

