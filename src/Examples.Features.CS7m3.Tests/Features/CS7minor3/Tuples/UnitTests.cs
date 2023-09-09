using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS7minor3.Tuples
{
    public class UnitTests
    {
        [Fact]
        public void WhenTestOfEquality()
        {
            (int, byte) left = (5, 10);

            (long, int) right1 = (5, 10);
            (left == right1).IsTrue();
            (left.GetHashCode() == right1.GetHashCode()).IsTrue();

            (decimal, double) right2 = (5m, 10.0);
            (left == right2).IsTrue();

            (int, byte) right3 = (5, 11);
            (left == right3).IsFalse();

            // error CS8384 Tuple types used as operands of an == or != operator must have matching cardinalities
            // (int, byte, byte) right4 = (5, 10, 0);
            // (left == right4).IsTrue();

            return;
        }

    }
}

