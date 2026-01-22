using System;
using Examples.Features.CSharp70.Tests.PatternMatching.Fixtures;
using Xunit;

namespace Examples.Features.CSharp70.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Constant patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class ConstantPatternsTests
    {
        [Theory]
        [MemberData(nameof(IfExpressionData))]
        public void When_EvaluatedInIfExpression_Then_MatchesSpecifiedConstants(int input, bool expected)
        {
            Assert.Equal(expected, IsIntConstant(input));

            bool IsIntConstant(int value)
            {
                // C# 7.0 Constant Pattern.
                return (value is 103) || (value is 207) || (value is 301);
            }
        }

        public static TheoryData<int, bool> IfExpressionData
            => new TheoryData<int, bool>
            {
                        { 103, true },
                        { 202, false },
                        { 250, false },
                        { 301, true },
            };

        [Theory]
        [MemberData(nameof(SwitchStatementData))]
        public void When_EvaluatedInSwitchStatement_Then_MatchesSpecifiedConstants(int input, decimal expected)
        {
            Assert.Equal(expected, GetGroupTicketPrice(input));

            decimal GetGroupTicketPrice(int visitorCount)
            {
                // C# 7.0 Constant Pattern.
                switch (visitorCount)
                {
                    case 1: return 12.0m;
                    case 2: return 20.0m;
                    case 3: return 27.0m;
                    case 4: return 32.0m;
                    case 0: return 0.0m;
                    default:
                        throw new ArgumentException($"Not supported number is {visitorCount}", nameof(visitorCount));
                }
            }

        }

        public static TheoryData<int, decimal> SwitchStatementData
            => new TheoryData<int, decimal>
            {
                { 0, 0.0m },
                { 1, 12.0m },
                { 2, 20.0m },
                { 3, 27.0m },
                { 4, 32.0m },
            };

        [Theory]
        [InlineData(null, true)]
        [InlineData("abc", false)]
        public void When_EvaluatedInIfExpression_Then_CanCheckForNull(object input, bool expected)
        {
            Assert.Equal(expected, IsNull(input));

            bool IsNull(object value)
            {
                // C# 7.0 Constant Pattern (is null).
                return value is null;
            }
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("abc", false)]
        public void When_EvaluatedInSwitchStatement_Then_CanCheckForNull(object input, bool expected)
        {
            Assert.Equal(expected, IsNull(input));

            bool IsNull(object value)
            {
                // C# 7.0 Constant Pattern (is null).
                switch (value)
                {
                    case null: return true;
                    default:
                        return false;
                }
            }
        }

        [Theory]
        [InlineData(ContactType.None, false)]
        [InlineData(ContactType.Phone, true)]
        [InlineData(ContactType.MobilePhone, true)]
        [InlineData(ContactType.Email, false)]
        [InlineData(ContactType.SocialNetwork, false)]
        public void When_EvaluatedInIfExpression_WithEnum_Then_MatchesSpecifiedConstants(ContactType input, bool expected)
        {
            Assert.Equal(expected, IsPhoneContact(input));

            bool IsPhoneContact(object value)
            {
                // C# 7.0 Constant Pattern with Enum.
                return value is ContactType.MobilePhone || value is ContactType.Phone;
            }
        }

        [Theory]
        [InlineData(ContactType.None, null)]
        [InlineData(ContactType.Phone, "000-000-0000")]
        [InlineData(ContactType.MobilePhone, "000-0000-0000")]
        [InlineData(ContactType.Email, "xxxxxxx@xxxxxxxx.xx.xx")]
        [InlineData(ContactType.SocialNetwork, "@xxxxxxxxxx")]
        public void When_EvaluatedInSwitchStatement_WithEnum_Then_MatchesSpecifiedConstants(ContactType input, string expected)
        {
            Assert.Equal(expected, GetPattern(input));

            string GetPattern(ContactType value)
            {
                // C# 7.0 Constant Pattern with Enum.
                switch (value)
                {
                    case ContactType.Phone: return "000-000-0000";
                    case ContactType.MobilePhone: return "000-0000-0000";
                    case ContactType.Email: return "xxxxxxx@xxxxxxxx.xx.xx";
                    case ContactType.SocialNetwork: return "@xxxxxxxxxx";
                    default:
                        return null;
                }
            }
        }

    }
}
