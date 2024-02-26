using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS71.InferredTupleElementNames
{
    /// <summary>
    /// Tests inferred tuple element names in C# 7.1.
    /// </summary>
    public class InferredTupleElementNamesTests
    {
        [Fact]
        public void WhenUsingNamedTuple()
        {
            // C# 7.1 or later
            // it may be inferred from the name of the corresponding variable in a tuple initialization expression.
            {
                double sum = 4.5;
                int count = 3;

                var actual = (sum, count);

                // C# 7.0 : Tuple element name 'sum' is inferred. Please use language version 7.1 or greater to access an element by its inferred name.
                actual.sum.Is(4.5);
                actual.count.Is(3);

#pragma warning disable IDE0033 // Use explicitly provided tuple name
                actual.Item1.Is(4.5);
                actual.Item2.Is(3);
#pragma warning restore IDE0033
            }

            return;
        }

    }
}
