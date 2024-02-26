using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS80.IndicesAndRanges
{
    /// <summary>
    /// Tests for Indices and ranges in C# 8.0.
    /// </summary>
    public class RangesTests
    {
        [Fact]
        public void BasicUsage()
        {
            // Range Literal
            var expected = 1..10;

            // Range constructor.
            new Range(new Index(1), new Index(10)).Is(expected);

            // Index implicit operator (int).
            new Range(1, 10).Is(expected);

            // Variables to Range
            var start = 1;
            var end = 9 + 1;
            (start..end).Is(expected);

            // Variables to Range(From End).
            (^start..end).Is(^1..10);

            //　Omitted start or end.
            new Range(new Index(3, fromEnd: true), Index.End).Is(^3..);
            new Range(Index.Start, 5).Is(..5);

            // start and end reverse.
            new Range(1, Index.Start).Is(1..0);
            new Range(Index.End, 1).Is(^0..1);
            new Range(Index.End, Index.Start).Is(^0..0);

            return;
        }

        [Fact]
        public void WhenGettingSubstring_WithRange()
        {
            string text = "0123456789";

            text[..].Is("0123456789");

            text[0..].Is("0123456789");
            text[1..].Is("123456789");
            text[2..].Is("23456789");
            text[3..].Is("3456789");

            text[^0..].Is("");
            text[^1..].Is("9");
            text[^2..].Is("89");
            text[^3..].Is("789");

            // Non-copy Slice.
            // ReadOnlySpan<char>.ToString() is new string(ReadOnlySpan<char>) ... maybe OK.
            text.AsSpan()[^3..].ToString().Is("789");

            // Not Equals ? ref ?
            (text.AsSpan()[^3..] == new ReadOnlySpan<char>("789".ToCharArray())).IsFalse();

            // Call ReadOnlySpan<char>[int32]
            text.AsSpan()[6].Is('6');

            // Get ReadOnlySpan<char>.Length - 4
            // Call ReadOnlySpan<char>[int32]
            text.AsSpan()[^4].Is('6');

            // Get ReadOnlySpan<char>.Length - Index.GetOffset(int32)
            // Call ReadOnlySpan<char>[int32]
            var index = ^4;
            text.AsSpan()[index].Is('6');

            return;
        }
    }
}
