using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS72.LeadingUnderscoresInNumericLiterals
{
    /// <summary>
    /// Tests for Leading underscores in numeric literals in C# 7.2.
    /// </summary>
    public class LeadingUnderscoresInNumericLiterals
    {
        [Fact]
        public void BasicUsage()
        {
            // C# 7.0 or later (_)
            long actual_70_1_ = 0xdead_beaf;
            actual_70_1_.Is(3735928495L);

            double actual_70_2_ = 123_456_789.987_654;
            actual_70_2_.Is(123456789.987654);

            // C# 7.2 or later
            // C# 7.1 : error CS8302: Feature 'leading digit separator' is not available in C# 7.1. Please use language version 7.2 or greater.
            long actual_72_1 = 0x_dead_beaf;
            actual_72_1.Is(3735928495L);

            int actual_72_3 = 0b_1001_1111;
            actual_72_3.Is(159);

            return;
        }
    }
}
