using System.Collections.Generic;
using System.Linq;
using Xunit;

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable
#pragma warning disable CA1861 // Avoid constant arrays as arguments to params methods

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Declaration and type patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class DeclarationAndTypePatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_AssignedResultToNewVariable(object input, string? expected)
        {
            var actual = GetMessage(input);
            Assert.Equal(expected, actual);

            static string? GetMessage(object value)
            {
                // C# 8.0 Declaration Pattern.
                return value switch
                {
                    string message => message,
                    IEnumerable<string> messages => messages.FirstOrDefault(),
                    _ => null,
                };
            }
        }

        public static TheoryData<object, string?> SwitchExpressionData

            => new TheoryData<object, string?>
            {
                { new[] { "Hello world" }, "Hello world"  },
                { new List<string> { "Hello world" }, "Hello world"  },
                { "Hello world", "Hello world" },
                { new[] { 1, 2, 3 }, null }
            };

    }
}
