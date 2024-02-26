namespace Examples.Features.CS120.DefaultLambdaParameters;

#pragma warning disable IDE0039 // Use local function instead of lambda

/// <summary>
/// Tests for Default lambda parameters in C# 12.0.
/// </summary>
public class DefaultLambdaParametersTests
{
    [Fact]
    public void BasicUsage()
    {
        var incrementBy = (int source, int increment = 1) => source + increment;

        incrementBy(5).Should().Be(6);

        incrementBy(5, 2).Should().Be(7);

        return;
    }

    [Fact]
    public void UseParams()
    {
        var sum = (params int[] values) =>
        {
            int sum = 0;
            foreach (var value in values)
                sum += value;

            return sum;
        };

        var empty = sum();
        empty.Should().Be(0);

        var sequence = new[] { 1, 2, 3, 4, 5 };
        var total = sum(sequence);
        total.Should().Be(15);

        return;
    }
}
