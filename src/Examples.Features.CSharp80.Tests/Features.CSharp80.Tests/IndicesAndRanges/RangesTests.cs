using System;
using Xunit;

namespace Examples.Features.CSharp80.Tests.IndicesAndRanges
{
    /// <summary>
    /// Tests for Indices and ranges in C# 8.0.
    /// </summary>
    public class RangesTests
    {
        [Fact]
        public void When_RangeUsed_Then_RangesCreateCorrectly()
        {
            // Range Literal
            var expected = 1..10;

            // Range constructor.
            Assert.Equal(expected, new Range(new Index(1), new Index(10)));

            // Index implicit operator (int).
            Assert.Equal(expected, new Range(1, 10));

            // Variables to Range
            var start = 1;
            var end = 9 + 1;
            Assert.Equal(expected, start..end);

            // Variables to Range(From End).
            Assert.Equal(^1..10, ^start..end);

            //　Omitted start or end.
            Assert.Equal(^3.., new Range(new Index(3, fromEnd: true), Index.End));
            Assert.Equal(..5, new Range(Index.Start, 5));

            // start and end reverse.
            Assert.Equal(1..0, new Range(1, Index.Start));
            Assert.Equal(^0..1, new Range(Index.End, 1));
            Assert.Equal(^0..0, new Range(Index.End, Index.Start));
        }

        [Fact]
        public void When_RangeUsedOnString_Then_SubstringReturnsCorrectly()
        {
            string text = "0123456789";

            Assert.Equal("0123456789", text[..]);

            Assert.Equal("0123456789", text[0..]);
            Assert.Equal("123456789", text[1..]);
            Assert.Equal("23456789", text[2..]);
            Assert.Equal("3456789", text[3..]);

            Assert.Equal("", text[^0..]);
            Assert.Equal("9", text[^1..]);
            Assert.Equal("89", text[^2..]);
            Assert.Equal("789", text[^3..]);

            // Non-copy Slice.
            // ReadOnlySpan<char>.ToString() is new string(ReadOnlySpan<char>) ... maybe OK.
            Assert.Equal("789", text.AsSpan()[^3..].ToString());

            // Not Equals ? ref ?
            Assert.False(text.AsSpan()[^3..] == new ReadOnlySpan<char>("789".ToCharArray()));

            // Call ReadOnlySpan<char>[int32]
            Assert.Equal('6', text.AsSpan()[6]);

            // Get ReadOnlySpan<char>.Length - 4
            // Call ReadOnlySpan<char>[int32]
            Assert.Equal('6', text.AsSpan()[^4]);
            // Get ReadOnlySpan<char>.Length - Index.GetOffset(int32)
            // Call ReadOnlySpan<char>[int32]
            var index = ^4;
            Assert.Equal('6', text.AsSpan()[index]);
        }
    }
}
