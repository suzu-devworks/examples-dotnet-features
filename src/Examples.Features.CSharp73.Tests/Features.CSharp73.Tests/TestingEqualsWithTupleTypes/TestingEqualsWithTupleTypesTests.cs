using Xunit;

namespace Examples.Features.CSharp73.Tests.TestingEqualsWithTupleTypes
{
    /// <summary>
    /// Tests for Testing == and != with tuple types in C# 7.3.
    /// </summary>
    public class TestingEqualsWithTupleTypesTests
    {
        [Fact]
        public void When_ComparingTuples_Then_ImplicitConversionsWork()
        {
            (int a, byte b) left = (5, 10);
            (long a, int b) right = (5, 10);
            // implicit conversions:
            //  int -> long : ok
            //  byte -> int: ok
            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            Assert.True(left == right);
            Assert.False(left != right);

            (decimal, float) right2 = (5m, 10.0f);
            // implicit conversions:
            //  int -> decimal : ok
            //  byte -> float: ok
            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            Assert.True(left == right2);
            Assert.False(left != right2);

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
            Assert.True(values1 == values2);
            Assert.False(values1 != values2);
        }

        [Fact]
        public void When_ComparingTuples_Then_NamesAreIgnored()
        {
            var left = (A: 5, B: 10);
            var right = (B: 5, A: 10);

            // C# 7.2 : error CS8320: Feature 'tuple equality' is not available in C# 7.2. Please use language version 7.3 or greater.
            Assert.True(left == right);
            Assert.False(left != right);
        }

    }
}
