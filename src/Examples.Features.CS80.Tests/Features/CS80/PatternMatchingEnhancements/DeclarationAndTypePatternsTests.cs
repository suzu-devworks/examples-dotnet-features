using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS80.PatternMatchingEnhancements
{
    /// <summary>
    /// Tests for Declaration and type patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class DeclarationAndTypePatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionsData))]
        public void WhenUsingSwitchExpressions(object input, string? expected)
        {
            var actual = GetMessage(input);
            actual.Is(expected);

            return;

            static string? GetMessage(object value)
            {
                return value switch
                {
                    // Declaration Pattern: `<type> <variable> => ...`
                    string message => message,
                    IEnumerable<string> messages => messages.FirstOrDefault(),
                    _ => null,
                };
            }
        }

        public static TheoryData<object, string?> SwitchExpressionsData
            => new TheoryData<object, string?>
            {
                { new[] { "Hello world" }, "Hello world"  },
                { new List<string> { "Hello world" }, "Hello world"  },
                { "Hello world", "Hello world" },
                { new[] { 1, 2, 3 }, null }
            };

    }
}
