using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS70.BinaryLiteralsAndDigitSeparators
{
    /// <summary>
    /// Tests for Binary literals and Digit separators in C# 7.0.
    /// </summary>
    public class BinaryLiteralsAndDigitSeparatorsTests
    {
        [Fact]
        public void BasicUsage()
        {
            // c# 6.0 (0x)
            int actual61 = 0xFF21;
            actual61.Is(65313);

            // C# 7.0 or later (0b)
            int actual71 = 0b01011;
            actual71.Is(11);

            uint actual72 = 0b101011100u;
            actual72.Is(348u);

            long actual73 = 0b101011100L;
            actual73.Is(348L);

            // C# 7.0 or later (_)
            long actual_71_ = 0xdead_beaf;
            actual_71_.Is(3735928495L);

            double actual_72_ = 123_456_789.987_654;
            actual_72_.Is(123456789.987654);

        }
    }
}
