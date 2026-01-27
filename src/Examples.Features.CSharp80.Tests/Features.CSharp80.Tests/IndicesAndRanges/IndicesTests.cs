using System;
using Xunit;

namespace Examples.Features.CSharp80.Tests.IndicesAndRanges
{
    /// <summary>
    /// Tests for Indices and ranges in C# 8.0.
    /// </summary>
    public class IndicesTests
    {
        [Fact]
        public void When_IndexUsed_Then_IndexesCreateCorrectly()
        {
            // Index implicit operator (int).
            Index expected = 1;

            Assert.Equal(Index.FromStart(1), expected);
            Assert.Equal(new Index(1), expected);

            // Index LIteral(fromEnd).
            var reverse = ^4;

            Assert.Equal(Index.FromEnd(4), reverse);
            Assert.Equal(new Index(4, fromEnd: true), reverse);

            // Variable(int) to Index(fromEnd).
            var num = 4;
            Assert.Equal(^num, reverse);
        }

    }
}
