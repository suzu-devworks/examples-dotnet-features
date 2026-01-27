namespace Examples.Features.CSharp120.Tests.DefaultLambdaParameters;

#pragma warning disable IDE0039 // Use local function instead of lambda

/// <summary>
/// Tests for Default lambda parameters in C# 12.0.
/// </summary>
public class DefaultLambdaParametersTests
{
    [Fact]
    public void When_UsingDefaultLambdaParameter_Then_AddsIncrement()
    {
        var incrementBy = (int source, int increment = 1) => source + increment;

        Assert.Equal(6, incrementBy(5));
        Assert.Equal(7, incrementBy(5, 2));

    }

    [Fact]
    public void When_UsingParamsLambda_Then_SumsValues()
    {
        var sum = (params int[] values) =>
        {
            int sum = 0;
            foreach (var value in values)
                sum += value;

            return sum;
        };

        var empty = sum();
        Assert.Equal(0, empty);

        var sequence = new[] { 1, 2, 3, 4, 5 };
        var total = sum(sequence);
        Assert.Equal(15, total);
    }
}
