using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS70.PatternMatching
{
    /// <summary>
    /// Tests for Declaration and type patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class DeclarationAndTypePatternsTests
    {
        [Theory]
        [MemberData(nameof(IfExpressionData))]
        public void WhenUsingIfExpressions(object input, bool expected)
        {
            var actual = IsEnumerableOfString(input);
            actual.Is(expected);

            return;

            bool IsEnumerableOfString(object value)
            {
                // Declaration Pattern: `is <type> <variable>`
                if (input is IEnumerable<string> converted)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static TheoryData<object, bool> IfExpressionData
            => new TheoryData<object, bool>
            {
                { new[] { "Hello world" }, true },
                { new List<string> { "Hello world" }, true },
                { "Hello world", false },
            };


        [Fact]
        public void WhenUsingSwitchStatements()
        {
            string actual;

            actual = GetMessage(new int[] { 10, 20, 30 });
            actual.Is("array length 3.");

            actual = GetMessage(new List<char> { 'a', 'b', 'c', 'd' });
            actual.Is("[ abcd ].");

            actual = GetMessage("abcde".Select(x => x));
            actual.Is("default message.");

            return;

            string GetMessage<T>(IEnumerable<T> source)
            {
                switch (source)
                {
                    // Declaration Pattern: `case <type> <variable>:`
                    case Array array:
                        return $"array length {array.Length}.";

                    case ICollection<T> collection:
                        return $"[ {string.Join("", collection)} ].";

                    default:
                        return "default message.";
                }
            }

        }

    }
}
