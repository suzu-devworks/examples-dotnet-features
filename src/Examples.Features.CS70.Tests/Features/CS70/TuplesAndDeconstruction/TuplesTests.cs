using ChainingAssertion;
using Xunit;

#pragma warning disable IDE0042 // Deconstruct variable declaration

namespace Examples.Features.CS70.TuplesAndDeconstruction
{
    /// <summary>
    /// Tests for Tuples and deconstruction in C# 7.0.
    /// </summary>
    public class TuplesTests
    {
        [Fact]
        public void WhenUsingNamedTuple()
        {
            // define anonymous tuple.
            {
                (double, int) actual = (4.5, 3);

                actual.Item1.Is(4.5);
                actual.Item2.Is(3);
            }

            // define named tuple.
            {
                var actual = (Sum: 4.5, Count: 3);

                actual.Sum.Is(4.5);
                actual.Count.Is(3);
            }

            // define named tuple.
            {
                (double Sum, int Count) actual = (4.5, 3);

                actual.Sum.Is(4.5);
                actual.Count.Is(3);
            }

            return;
        }

        [Fact]
        public void WhenUsingTupleAssignment()
        {
            (int, double) t1 = (17, 3.14);

            // Allows implicit conversion from int to double.
            (double First, double Second) t2 = t1;

            t2.First.Is(17.0);
            t2.Second.Is(3.14);

            // Allows implicit conversion from int to decimal.
            (decimal First, double Second) t3 = t1;

            t3.First.Is(17m);
            t3.Second.Is(3.14);

            // error CS0266 Cannot implicitly convert type '(int, double)' to '(int First, int Second)'.
            //(int First, int Second) t4 = t1;

            return;
        }

        [Fact]
        public void WhenUsingTupleDeconstruction()
        {
            var t = ("post office", 3.6);

            // Use the var keyword outside the parentheses to declare implicitly typed variables.
            var (destination1, distance1) = t;

            destination1.IsInstanceOf<string>();
            distance1.IsInstanceOf<double>();
            (destination1, distance1).Is(("post office", 3.6));

            // Explicitly declare the type of each variable inside parentheses.
            (string destination2, double distance2) = t;

            (destination2, distance2).Is(("post office", 3.6));

            // Declare some types explicitly and other types implicitly (with var) inside the parentheses.
            (var destination3, double distance3) = t;

            destination3.IsInstanceOf<string>();
            (destination3, distance3).Is(("post office", 3.6));

            // Use existing variables.
            string destination4;
            double distance4;
            (destination4, distance4) = t;

            (destination4, distance4).Is(("post office", 3.6));

            return;
        }

    }
}
