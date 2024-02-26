using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS80.IndicesAndRanges
{
    /// <summary>
    /// Tests for Indices and ranges in C# 8.0.
    /// </summary>
    public class IndicesTests
    {
        [Fact]
        public void BasicUsage()
        {
            // Index implicit operator (int).
            Index expected = 1;

            Index.FromStart(1).Is(expected);
            new Index(1).Is(expected);

            // Index LIteral(fromEnd).
            var reverse = ^4;

            Index.FromEnd(4).Is(reverse);
            new Index(4, fromEnd: true).Is(reverse);

            // Variable(int) to Index(fromEnd).
            var num = 4;
            (^num).Is(reverse);

            return;
        }

    }
}
