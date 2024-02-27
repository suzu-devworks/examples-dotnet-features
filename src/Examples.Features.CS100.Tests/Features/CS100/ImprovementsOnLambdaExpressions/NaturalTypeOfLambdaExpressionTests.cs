using System.Linq.Expressions;

#pragma warning disable IDE0039 // Use local function instead of lambda

namespace Examples.Features.CS100.ImprovementsOnLambdaExpressions;

/// <summary>
/// Tests for Natural type of Improvements on lambda expressions in C# 10.0.
/// </summary>
public class NaturalTypeOfLambdaExpressionTests
{
    [Fact]
    public void WhenCompilerInfersDelegateTypes()
    {
        // C# 9.0 OK
        Func<string, int> parseInt = (string s) => int.Parse(s);

        var original = parseInt("123");
        original.Should().Be(123);

        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        var parse = (string s) => int.Parse(s);

        var actual = parse("123");
        actual.Should().Be(123);

        return;
    }

    [Fact]
    public void WhenAssignedToLessExplicitTypes()
    {
        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'object' because it is not a delegate type
        object parse1 = (string s) => int.Parse(s);

        parse1.Should().BeAssignableTo<Func<string, int>>();

        // C# 9.0: error CS1660: Cannot convert lambda expression to type 'Delegate' because it is not a delegate type
        Delegate parse2 = (string s) => int.Parse(s);

        parse2.Should().BeAssignableTo<Func<string, int>>();

        return;
    }

    [Fact]
    public void WhenExactlyOneOverloadOnMethodGroups_HasNaturalType()
    {
        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        var read = Console.Read; // Just one overload; Func<int> inferred

        read.Should().BeAssignableTo<Func<int>>();

        // error CS8917: The delegate type could not be inferred.
        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        //var write = Console.Write; // ERROR: Multiple overloads, can't choose

        return;
    }

    [Fact]
    public void WhenAssignedToLambdaExpression()
    {
        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'LambdaExpression' because it is not a delegate type
        LambdaExpression parseExpr1 = (string s) => int.Parse(s);

        parseExpr1.Should().BeAssignableTo<Expression<Func<string, int>>>();

        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'Expression' because it is not a delegate type
        Expression parseExpr2 = (string s) => int.Parse(s);

        parseExpr2.Should().BeAssignableTo<Expression<Func<string, int>>>();

        return;
    }

}

