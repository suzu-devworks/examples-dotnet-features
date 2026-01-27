using Xunit;

namespace Examples.Features.CSharp70.Tests.TuplesAndDeconstruction
{
    /// <summary>
    /// Tests for Tuples and deconstruction in C# 7.0.
    /// </summary>
    public class TuplesTests
    {
        [Fact]
        public void When_UsingNamedTuple_Then_CanAccessNamingMember()
        {
            // define anonymous tuple.
            {
                var anonymous = (4.5, 3);

                Assert.Equal(4.5, anonymous.Item1);
                Assert.Equal(3, anonymous.Item2);
            }

            // define named tuple.
            {
                var named = (Sum: 4.5, Count: 3);

                Assert.Equal(4.5, named.Sum);
                Assert.Equal(3, named.Count);
            }

            // define named tuple.
            {
                (double Sum, int Count) namingTarget = (4.5, 3);

                Assert.Equal(4.5, namingTarget.Sum);
                Assert.Equal(3, namingTarget.Count);
            }
        }

        [Fact]
        public void When_ReAssignmentTuple_Then_FollowsElementTypeConversionRule()
        {
            (int, double) original = (17, 3.14);

            // Allows implicit conversion from int to double.
            (double First, double Second) doubleFirst = original;

            Assert.Equal(17.0, doubleFirst.First);
            Assert.Equal(3.14, doubleFirst.Second);

            // Allows implicit conversion from int to decimal.
            (decimal First, double Second) decimalFirst = original;

            Assert.Equal(17m, decimalFirst.First);
            Assert.Equal(3.14, decimalFirst.Second);

            // error CS0266 Cannot implicitly convert type '(int, double)' to '(int First, int Second)'.
            //(int First, int Second) invalid = original;
        }

        [Fact]
        public void When_DeconstructionTuple_Then_ReceivingAsTuple()
        {
            var original = ("post office", 3.6);

            // Use the var keyword outside the parentheses to declare implicitly typed variables.
            {
                var (destination, distance) = original;

                Assert.IsType<string>(destination);
                Assert.IsType<double>(distance);
                Assert.Equal(("post office", 3.6), (destination, distance));
            }

            // Explicitly declare the type of each variable inside parentheses.
            {
                (string destination, double distance) = original;

                Assert.Equal(("post office", 3.6), (destination, distance));
            }

            // Declare some types explicitly and other types implicitly (with var) inside the parentheses.
            {
                (var destination, double distance) = original;

                Assert.IsType<string>(destination);
                Assert.Equal(("post office", 3.6), (destination, distance));
            }

            // Use existing variables.
            {
                string destination;
                double distance;
                (destination, distance) = original;

                Assert.Equal(("post office", 3.6), (destination, distance));
            }

        }

    }
}
