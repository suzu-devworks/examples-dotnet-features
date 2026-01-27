using Xunit;

namespace Examples.Features.CS71.InferredTupleElementNames
{
    /// <summary>
    /// Tests inferred tuple element names in C# 7.1.
    /// </summary>
    public class InferredTupleElementNamesTests
    {
        [Fact]
        public void When_UsingNamedTuple_Then_ElementNamesAreInferred()
        {
            // C# 7.1 or later
            // it may be inferred from the name of the corresponding variable in a tuple initialization expression.
            {
                double sum = 4.5;
                int count = 3;

                // Tuple element names are inferred from the variable names 'sum' and 'count'.
                var actual = (sum, count);

                // C# 7.0 : Tuple element name 'sum' is inferred. Please use language version 7.1 or greater to access an element by its inferred name.
                Assert.Equal(4.5, actual.sum);
                Assert.Equal(3, actual.count);

                Assert.Equal(4.5, actual.Item1);
                Assert.Equal(3, actual.Item2);
            }
        }

    }
}
