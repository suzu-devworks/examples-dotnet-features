using ChainingAssertion;
using Examples.Features.CS80.PatternMatchingEnhancements.Fixtures;
using Xunit;

namespace Examples.Features.CS80.PatternMatchingEnhancements
{
    /// <summary>
    /// Tests for Positional patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class PositionalPatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionsData))]
        public void WhenUsingSwitchExpressions(Shape input, string? expected)
        {
            var actual = GetClassify(input);
            actual.Is(expected);

            return;

            static string GetClassify(Shape shape)
            {
                return shape switch
                {
                    // Positional patterns : <type>(item1, item2, item3 ... ) =>
                    Rectangle(100, 100, null) => "Found 100x100 rectangle without a point",
                    Rectangle(100, 100, _) => "Found 100x100 rectangle",
                    _ => "Different, or null shape"
                };
            }
        }

        public static TheoryData<Shape, string?> SwitchExpressionsData
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
