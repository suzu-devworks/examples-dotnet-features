using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS72.ConditionalRefExpressions
{
    /// <summary>
    /// Tests for Conditional <c>ref</c> expressions in C# 7.2.
    /// </summary>
    public class ConditionalRefExpressionsTests
    {
        [Fact]
        public void BasicUsage()
        {
            int a = 100;
            int b = 200;

            // C# 7.1 : error CS8302: Feature 'ref conditional expression' is not available in C# 7.1. Please use language version 7.2 or greater.
            ref int alias = ref ((a < b) ? ref b : ref a);

            alias *= 2;

            a.Is(100);
            b.Is(400);

            return;
        }
    }
}
