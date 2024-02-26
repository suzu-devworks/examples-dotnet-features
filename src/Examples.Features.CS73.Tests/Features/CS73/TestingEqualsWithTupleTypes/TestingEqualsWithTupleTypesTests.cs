using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS73.TestingEqualsWithTupleTypes
{
    /// <summary>
    /// Tests for Testing == and != with tuple types in C# 7.3.
    /// </summary>
    public class TestingEqualsWithTupleTypesTests
    {
        [Fact]
        public void WhenTestingEqualityOfTuples_WithDifferentTypes()
        {
            (int a, byte b) left = (5, 10);
            (long a, int b) right = (5, 10);
            // implicit conversions:
            //  int -> long : ok
            //  byte -> int: ok
            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            (left == right).IsTrue();
            (left != right).IsFalse();

            (decimal, float) right2 = (5m, 10.0f);
            // implicit conversions:
            //  int -> decimal : ok
            //  byte -> float: ok
            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            (left == right2).IsTrue();
            (left != right2).IsFalse();

            // error CS8384 Tuple types used as operands of an == or != operator must have matching cardinalities
            // (int a, byte b, int c) right3 = (5, 10, 0);
            // (left == right3).IsFalse();

            (long a, (double x, decimal y) b) values1 = (1, (2, 3));
            (byte, (float, short)) values2 = (1, (2, 3));
            // implicit conversions:
            //  byte -> long : ok
            //  float -> double: ok
            //  short -> decimal: ok
            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            (values1 == values2).IsTrue();
            (values1 != values2).IsFalse();

            return;
        }

        [Fact]
        public void WhenTestingEqualityOfTuples_WithDifferentNames()
        {
            var left = (A: 5, B: 10);
            var right = (B: 5, A: 10);

            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            (left == right).IsTrue();
            (left != right).IsFalse();

            return;
        }

    }
}
