using Xunit;

namespace Examples.Features.CSharp72.Tests.LeadingUnderscoresInNumericLiterals
{
    /// <summary>
    /// Tests for Leading underscores in numeric literals in C# 7.2.
    /// </summary>
    public class LeadingUnderscoresInNumericLiterals
    {
        [Fact]
        public void When_DeclaringNumberLiterals_Then_CanUseLeadingUnderscores()
        {
            // C# 7.0 or later (_)
            long actual_70_1_ = 0xdead_beaf;
            Assert.Equal(3735928495L, actual_70_1_);

            double actual_70_2_ = 123_456_789.987_654;
            Assert.Equal(123456789.987654, actual_70_2_);

            // C# 7.2 or later
            // C# 7.1 : error CS8302: Feature 'leading digit separator' is not available in C# 7.1. Please use language version 7.2 or greater.
            long actual_72_1 = 0x_dead_beaf;
            Assert.Equal(3735928495L, actual_72_1);

            int actual_72_3 = 0b_1001_1111;
            Assert.Equal(159, actual_72_3);
        }
    }
}
