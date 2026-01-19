using Xunit;

namespace Examples.Features.CSharp70.Tests.BinaryLiteralsAndDigitSeparators
{
    /// <summary>
    /// Tests for Binary literals and Digit separators in C# 7.0.
    /// </summary>
    public class BinaryLiteralsAndDigitSeparatorsTests
    {
        [Fact]
        public void When_UsingBinaryLiterals_Then_ReturnsDigits()
        {
            // older (0x)

            var hexIntLiteral = 0xFF21;
            Assert.IsType<int>(hexIntLiteral);

            var hexUintLiteral = 0xDEADBEAF;
            Assert.IsType<uint>(hexUintLiteral);

            // C# 7.0 or later (0b)

            var binIntLiteral = 0b01011;
            Assert.IsType<int>(binIntLiteral);

            var binUintLiteral = 0b101011100u;
            Assert.IsType<uint>(binUintLiteral);

            var binLongLiteral = 0b101011100L;
            Assert.IsType<long>(binLongLiteral);

            // CS0103: The name 'm' does not exist in the current context
            // var decimalLiteral = 0b101011100m;
        }

        [Fact]
        public void When_UsingDigitSeparators_Then_NoImpactOnValue()
        {
            // C# 7.0 or later (_)

            var hexUintLiteral = 0xDEAD_BEAF;
            Assert.IsType<uint>(hexUintLiteral);

            var binLongLiteral = 0b01_01_01_11_00L;
            Assert.IsType<long>(binLongLiteral);

            // There seems to be no need for regularity in the separator.
            var doubleLiteral = 123_45_6_789.987654;
            Assert.IsType<double>(doubleLiteral);
        }

    }
}
