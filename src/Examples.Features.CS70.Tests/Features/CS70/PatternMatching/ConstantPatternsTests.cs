using System;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS70.PatternMatching
{
    /// <summary>
    /// Tests for Constant patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class ConstantPatternsTests
    {
        [Theory]
        [InlineData(103, true)]
        [InlineData(202, false)]
        [InlineData(301, true)]
        public void WhenUsingIfExpressions(object input, bool expected)
        {
            var actual = IsIntConstant(input);
            actual.Is(expected);

            return;

            bool IsIntConstant(object value)
            {
                // Constant Pattern: `is <constant>`
                if ((input is 103) || (input is 207) || (input is 301))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        [Theory]
        [MemberData(nameof(SwitchStatementsData))]
        public void WhenUsingSwitchStatements(int input, decimal expected)
        {
            var actual = GetGroupTicketPrice(input);
            actual.Is(expected);

            return;

            decimal GetGroupTicketPrice(int visitorCount)
            {
                switch (visitorCount)
                {
                    // Constant Pattern: `case <constant>:`
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

        public static TheoryData<int, decimal> SwitchStatementsData
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
        public void WhenUsingIfExpressions_WithNull(object input, bool expected)
        {
            var actual = IsNull(input);
            actual.Is(expected);

            return;

            bool IsNull(object value)
            {
                // Null Constant Pattern: `is null`
                if (value is null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("abc", false)]
        public void WhenUsingSwitchStatements_WithNull(object input, bool expected)
        {
            bool actual = IsNull(input);
            actual.Is(expected);

            return;

            bool IsNull(object value)
            {
                switch (value)
                {
                    // Null Constant Pattern: `case null:`
                    case null: return true;
                    default:
                        return false;
                }
            }
        }

        public enum ContactType
        {
            None = 0,
            Phone,
            MobilePhone,
            Email,
            SocialNetwork
        }

        [Theory]
        [InlineData(ContactType.None, false)]
        [InlineData(ContactType.Phone, true)]
        [InlineData(ContactType.MobilePhone, true)]
        [InlineData(ContactType.Email, false)]
        [InlineData(ContactType.SocialNetwork, false)]
        public void WhenUsingIfExpressions_WithEnum(ContactType input, bool expected)
        {
            var actual = IsPhoneContact(input);
            actual.Is(expected);

            return;

            bool IsPhoneContact(object value)
            {
                // Constant Pattern: `is <constant>`
                if (value is ContactType.MobilePhone || value is ContactType.Phone)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        [Theory]
        [InlineData(ContactType.None, null)]
        [InlineData(ContactType.Phone, "000-000-0000")]
        [InlineData(ContactType.MobilePhone, "000-0000-0000")]
        [InlineData(ContactType.Email, "xxxxxxx@xxxxxxxx.xx.xx")]
        [InlineData(ContactType.SocialNetwork, "@xxxxxxxxxx")]
        public void WhenUsingSwitchStatements_WithEnum(ContactType input, string expected)
        {
            var actual = GetPattern(input);
            actual.Is(expected);

            return;

            string GetPattern(ContactType value)
            {
                switch (value)
                {
                    // Constant Pattern: `case <constant>:`
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
