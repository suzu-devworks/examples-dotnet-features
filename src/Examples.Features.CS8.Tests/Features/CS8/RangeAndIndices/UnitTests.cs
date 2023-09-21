using System;
using ChainingAssertion;
using Xunit;

#nullable enable

namespace Examples.Features.CS8.RangeAndIndices
{
    /// <summary>
    /// Tests for C# 8.0, Range And Indices.
    /// </summary>
    public class RangeAndIndicesTests
    {
        [Fact]
        public void WhenCreatingRange()
        {
            // Range Literal
            var expected = 1..10;

            // Range constructor.
            (new Range(new Index(1), new Index(10))).Is(expected);

            // Index implicit operator (int).
            (new Range(1, 10)).Is(expected);

            // Variables to Range
            var start = 1;
            var end = 9 + 1;
            (start..end).Is(expected);

            // Variables to Range(From End).
            (^start..end).Is(^1..10);

            //　Ommited start or end.
            (new Range(new Index(3, fromEnd: true), Index.End)).Is(^3..);
            (new Range(Index.Start, 5)).Is(..5);

            // start and end reverse.
            (new Range(1, Index.Start)).Is(1..0);
            (new Range(Index.End, 1)).Is(^0..1);
            (new Range(Index.End, Index.Start)).Is(^0..0);

            return;
        }

        [Fact]
        public void WhenCreatingIndex()
        {
            // Index implicit operator (int).
            Index expected = 1;

            Index.FromStart(1).Is(expected);
            (new Index(1)).Is(expected);

            // Index LIteral(fromEnd).
            var reverse = ^4;

            Index.FromEnd(4).Is(reverse);
            (new Index(4, fromEnd: true)).Is(reverse);

            //Variable to Index(fromEnd).
            var num = 4;
            (^num).Is(reverse);

            return;
        }

        [Fact]
        public void WhenSubStringWithRange()
        {
            const string TEXT = "0123456789";

            TEXT[..].Is("0123456789");

            TEXT[0..].Is("0123456789");
            TEXT[1..].Is("123456789");
            TEXT[2..].Is("23456789");
            TEXT[3..].Is("3456789");

            TEXT[^0..].Is("");
            TEXT[^1..].Is("9");
            TEXT[^2..].Is("89");
            TEXT[^3..].Is("789");

            // Non-copy Slice.
            // ReadOnlySpan<char>.ToString() is new string(ReadOnlySpan<char>) ... maybe OK.
            TEXT.AsSpan()[^3..].ToString().Is("789");

            // Not Equals ? ref ?
            (TEXT.AsSpan()[^3..] == new ReadOnlySpan<char>("789".ToCharArray())).IsFalse();

            // Call ReadOnlySpan<char>[int32]
            TEXT.AsSpan()[6].Is('6');

            // Get ReadOnlySpan<char>.Length - 4
            // Call ReadOnlySpan<char>[int32]
            TEXT.AsSpan()[^4].Is('6');

            // Get ReadOnlySpan<char>.Length - Index.GetOffset(int32)
            // Call ReadOnlySpan<char>[int32]
            var index = ^4;
            TEXT.AsSpan()[index].Is('6');

            return;
        }

    }

}
