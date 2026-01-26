using System.Linq.Expressions;

namespace Examples.Features.CSharp100.Tests.ImprovementsOnLambdaExpressions;

/// <summary>
/// Tests for Natural type of Improvements on lambda expressions in C# 10.0.
/// </summary>
public class NaturalTypeOfLambdaExpressionTests
{
    [Fact]
    public void When_LambdaInferenceEnabled_Then_DelegateResolved()
    {
        // C# 9.0 OK
        Func<string, int> parseInt = (string s) => int.Parse(s);

        var original = parseInt("123");
        Assert.Equal(123, original);

        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        var parse = (string s) => int.Parse(s);

        var actual = parse("123");
        Assert.Equal(123, actual);
    }

    [Fact]
    public void When_AssignedToObjectOrDelegate_Then_TypeInferred()
    {
        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'object' because it is not a delegate type
        object parse1 = (string s) => int.Parse(s);

        Assert.IsType<Func<string, int>>(parse1, exactMatch: false);

        // C# 9.0: error CS1660: Cannot convert lambda expression to type 'Delegate' because it is not a delegate type
        Delegate parse2 = (string s) => int.Parse(s);

        Assert.IsType<Func<string, int>>(parse2, exactMatch: false);
    }

    [Fact]
    public void When_SingleOverloadMethodGroup_Then_NaturalTypeInferred()
    {
        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        var read = Console.Read; // Just one overload; Func<int> inferred

        Assert.IsType<Func<int>>(read, exactMatch: false);

        // error CS8917: The delegate type could not be inferred.
        // C# 9.0 : error CS8773: Feature 'inferred delegate type' is not available in C# 9.0. Please use language version 10.0 or greater.
        //var write = Console.Write; // ERROR: Multiple overloads, can't choose
    }

    [Fact]
    public void When_AssignedToExpression_Then_TypedExpressionProduced()
    {
        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'LambdaExpression' because it is not a delegate type
        LambdaExpression parseExpr1 = (string s) => int.Parse(s);

        Assert.IsType<Expression<Func<string, int>>>(parseExpr1, exactMatch: false);

        // C# 9.0 : error CS1660: Cannot convert lambda expression to type 'Expression' because it is not a delegate type
        Expression parseExpr2 = (string s) => int.Parse(s);

        Assert.IsType<Expression<Func<string, int>>>(parseExpr2, exactMatch: false);
    }

}

