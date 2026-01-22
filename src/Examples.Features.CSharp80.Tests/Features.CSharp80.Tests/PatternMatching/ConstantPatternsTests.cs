using System;
using Xunit;

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for constant patterns introduced in C# 8.0.
    /// </summary>
    public class ConstantPatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_MatchesSpecifiedConstants(int input, decimal expected)
        {
            Assert.Equal(expected, GetGroupTicketPrice(input));

            decimal GetGroupTicketPrice(int visitorCount)
            {
                // C# 8.0 Constant Pattern.
                return visitorCount switch
                {
                    1 => 12.0m,
                    2 => 20.0m,
                    3 => 27.0m,
                    4 => 32.0m,
                    0 => 0.0m,
                    _ => throw new ArgumentException($"Not supported number is {visitorCount}", nameof(visitorCount))
                };
            }

        }

        public static TheoryData<int, decimal> SwitchExpressionData
            => new TheoryData<int, decimal>
            {
                { 0, 0.0m },
                { 1, 12.0m },
                { 2, 20.0m },
                { 3, 27.0m },
                { 4, 32.0m },
            };

    }

}
