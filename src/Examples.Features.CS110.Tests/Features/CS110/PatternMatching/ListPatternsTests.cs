namespace Examples.Features.CS110.PatternMatching;

/// <summary>
///  Tests for List patterns of pattern matching in C# 11.0.
/// </summary>
public class ListPatternsTests
{
    [Fact]
    public void WhenUsingIfExpressions()
    {
        int[] numbers = { 1, 2, 3 };

        // error CS8936: Feature 'list pattern' is not available in C# 10.0. Please use language version 11.0 or greater.

        (numbers is [1, 2, 3]).Should().BeTrue();
        (numbers is [1, 2, 4]).Should().BeFalse();
        (numbers is [1, 2, 3, 4]).Should().BeFalse();
        (numbers is [0 or 1, <= 2, >= 3]).Should().BeTrue();

        // error CS9135: A constant value of type 'int[]' is expected
        // (numbers is numbers).Should().BeTrue();

        // To match any element, use the discard pattern

        (numbers is [1, _, _]).Should().BeTrue();
        (numbers is [1, _, _, _]).Should().BeFalse();

        // You can capture elements with var patterns.

        if (numbers is [var first, _, _])
        {
            first.Should().Be(1);
        }

        // To match elements only at the start or/and the end of an input sequence,
        // use slice pattern

        (new[] { 1, 2, 3, 4, 5 } is [> 0, > 0, ..]).Should().BeTrue();
        (new[] { 1, 1 } is [_, _, ..]).Should().BeTrue();
        (new[] { 0, 1, 2, 3, 4 } is [> 0, > 0, ..]).Should().BeFalse();
        (new[] { 1 } is [1, 2, ..]).Should().BeFalse();

        (new[] { 1, 2, 3, 4 } is [.., > 0, > 0]).Should().BeTrue();
        (new[] { 2, 4 } is [.., > 0, 2, 4]).Should().BeFalse();
        (new[] { 2, 4 } is [.., 2, 4]).Should().BeTrue();

        (new[] { 1, 2, 3, 4 } is [>= 0, .., 2 or 4]).Should().BeTrue();
        (new[] { 1, 0, 0, 1 } is [1, 0, .., 0, 1]).Should().BeTrue();
        (new[] { 1, 0, 1 } is [1, 0, .., 0, 1]).Should().BeFalse();

        // error CS8980: Slice patterns may only be used once and directly inside a list pattern.
        //(new[] { 0, 1, 2, 3, 4 } is [.., 2, 3, ..]).Should().BeFalse();

        (new[] { new[] { 1, 2 }, new[] { 3, 4, 5, 6 }, new[] { 7 } } is [{ Length: > 1 }, ..]).Should().BeTrue();

        // error CS8985: List patterns may not be used for a value of type 'IEnumerable<int>'. No suitable 'Length' or 'Count' property was found.
        // var values = Enumerable.Range(1, 10);
        // (values is [1, 2, 3, ..]).Should().BeTrue();

        return;
    }

}
