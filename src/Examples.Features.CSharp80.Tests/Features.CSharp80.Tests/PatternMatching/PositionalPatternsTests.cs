using Examples.Features.CSharp80.Tests.PatternMatching.Fixtures;
using Xunit;

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Positional patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class PositionalPatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_MatchesValueFromDeconstruction(Shape input, string? expected)
        {
            var actual = GetClassify(input);
            Assert.Equal(expected, actual);

            static string GetClassify(Shape shape)
            {
                // C# 8.0 Positional Pattern.
                return shape switch
                {
                    Rectangle(100, 100, null) => "Found 100x100 rectangle without a point",
                    Rectangle(100, 100, _) => "Found 100x100 rectangle",
                    _ => "Different, or null shape"
                };
            }
        }

        public static TheoryData<Shape, string?> SwitchExpressionData
           => new TheoryData<Shape, string?>
           {
                {
                    new Rectangle
                    {
                        Width = 100,
                        Height = 100,
                        Point = new Point { X = 0, Y = 100 }
                    },
                    "Found 100x100 rectangle"
                }
           };
    }
}
