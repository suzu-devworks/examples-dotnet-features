using System;
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
        [InlineData(103, true)]
        [InlineData(202, false)]
        [InlineData(301, true)]
        public void When_UsingIfExpressions_Then_ReturnsByExplicitlyConstants(object input, bool expected)
        {
            Assert.Equal(expected, IsIntConstant(input));

            bool IsIntConstant(object value)
            {
                // Constant Pattern: `is <constant>`
                return (value is 103) || (value is 207) || (value is 301);
            }
        }

        [Theory]
        [MemberData(nameof(SwitchStatementsData))]
        public void When_UsingSwitchStatements_Then_ReturnsByExplicitlyConstants(int input, decimal expected)
        {
            Assert.Equal(expected, GetGroupTicketPrice(input));

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
        public void When_UsingIfExpressionsIsNull_Then_CheckingForNull(object input, bool expected)
        {
            Assert.Equal(expected, IsNull(input));

            bool IsNull(object value)
            {
                // Null Constant Pattern: `is null`
                return value is null;
            }
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData("abc", false)]
        public void When_UsingSwitchStatementsCaseIsNull_Then_CheckingForNull(object input, bool expected)
        {
            Assert.Equal(expected, IsNull(input));

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
        public void When_UsingExpressionsWithEnum_Then_CanBeUsedAsConstant(ContactType input, bool expected)
        {
            Assert.Equal(expected, IsPhoneContact(input));

            bool IsPhoneContact(object value)
            {
                // Constant Pattern: `is <constant>`
                return value is ContactType.MobilePhone || value is ContactType.Phone;
            }
        }

        [Theory]
        [InlineData(ContactType.None, null)]
        [InlineData(ContactType.Phone, "000-000-0000")]
        [InlineData(ContactType.MobilePhone, "000-0000-0000")]
        [InlineData(ContactType.Email, "xxxxxxx@xxxxxxxx.xx.xx")]
        [InlineData(ContactType.SocialNetwork, "@xxxxxxxxxx")]
        public void When_UsingSwitchStatementsWithEnum_Then_CanBeUsedAsConstant(ContactType input, string expected)
        {
            Assert.Equal(expected, GetPattern(input));

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
