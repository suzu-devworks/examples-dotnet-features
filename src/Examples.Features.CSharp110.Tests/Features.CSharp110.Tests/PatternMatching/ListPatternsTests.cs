namespace Examples.Features.CSharp110.Tests.PatternMatching;

#pragma warning disable CA1861 // Avoid constant arrays as arguments to params methods

/// <summary>
///  Tests for List patterns of pattern matching in C# 11.0.
/// </summary>
public class ListPatternsTests
{
    [Fact]
    public void When_EvaluatedInIfExpression_Then_MatchesSequencePattern()
    {
        int[] numbers = { 1, 2, 3 };

        // error CS8936: Feature 'list pattern' is not available in C# 10.0. Please use language version 11.0 or greater.

        Assert.True(numbers is [1, 2, 3]);
        Assert.False(numbers is [1, 2, 4]);
        Assert.False(numbers is [1, 2, 3, 4]);
        Assert.True(numbers is [0 or 1, <= 2, >= 3]);

        // error CS9135: A constant value of type 'int[]' is expected
        // Assert.True(numbers is numbers);

        // To match any element, use the discard pattern

        Assert.True(numbers is [1, _, _]);
        Assert.False(numbers is [1, _, _, _]);

        // You can capture elements with var patterns.

        if (numbers is [var first, _, _])
        {
            Assert.Equal(1, first);
        }

        // To match elements only at the start or/and the end of an input sequence,
        // use slice pattern

        Assert.True(new[] { 1, 2, 3, 4, 5 } is [> 0, > 0, ..]);
        Assert.True(new[] { 1, 1 } is [_, _, ..]);
        Assert.False(new[] { 0, 1, 2, 3, 4 } is [> 0, > 0, ..]);
        Assert.False(new[] { 1 } is [1, 2, ..]);

        Assert.True(new[] { 1, 2, 3, 4 } is [.., > 0, > 0]);
        Assert.False(new[] { 2, 4 } is [.., > 0, 2, 4]);
        Assert.True(new[] { 2, 4 } is [.., 2, 4]);

        Assert.True(new[] { 1, 2, 3, 4 } is [>= 0, .., 2 or 4]);
        Assert.True(new[] { 1, 0, 0, 1 } is [1, 0, .., 0, 1]);
        Assert.False(new[] { 1, 0, 1 } is [1, 0, .., 0, 1]);

        // error CS8980: Slice patterns may only be used once and directly inside a list pattern.
        // Assert.False(new[] { 0, 1, 2, 3, 4 } is [.., 2, 3, ..]);

        Assert.True(new[] { new[] { 1, 2 }, new[] { 3, 4, 5, 6 }, new[] { 7 } } is [{ Length: > 1 }, ..]);

        // error CS8985: List patterns may not be used for a value of type 'IEnumerable<int>'. No suitable 'Length' or 'Count' property was found.
        // var values = Enumerable.Range(1, 10);
        // Assert.True(values is [1, 2, 3, ..]);
    }

}
