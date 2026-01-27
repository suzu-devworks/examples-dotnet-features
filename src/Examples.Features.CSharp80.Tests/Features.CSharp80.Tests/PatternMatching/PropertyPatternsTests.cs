using Examples.Features.CSharp80.Tests.PatternMatching.Fixtures;
using Xunit;

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Property patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class PropertyPatternsTests
    {
        [Theory]
        [MemberData(nameof(IfExpressionData))]
        public void When_EvaluatedInIfExpression_Then_MatchesValueFromNesting(Segment input, bool expected)
        {
            var actual = IsAnyEndOnXAxis(input);
            Assert.Equal(expected, actual);

            static bool IsAnyEndOnXAxis(Segment segment) =>
                // C# 8.0 Property Pattern.
                segment is { Start: { Y: 0 } } || segment is { End: { Y: 0 } };
        }

        public static TheoryData<Segment, bool> IfExpressionData
            => new TheoryData<Segment, bool>
            {
                {
                    new Segment
                    {
                        Start = new Point { X = 100, Y = 200 },
                        End = new Point { X = 300, Y = 0 },
                    },
                    true
                },
                {
                    new Segment
                    {
                        Start = new Point { X = 100, Y = 0 },
                        End = new Point { X = 300, Y = 400 },
                    },
                    true
                },
                {
                    new Segment
                    {
                        Start = new Point { X = 100, Y = 200 },
                        End = new Point { X = 300, Y = 400 },
                    },
                    false
                }
            };

        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_MatchesValueFromNesting(Point input, string expected)
        {
            var actual = Display(input);
            Assert.Equal(expected, actual);

            static string Display(object o) => o switch
            {
                Point { X: 0, Y: 0 } => "origin",
                Point { X: 1, Y: _ } p => $"({p.X}, {p.Y})",
                Point { X: var x, Y: var y } => $"({x}, {y})",
                _ => "unknown"
            };
        }

        public static TheoryData<Point, string> SwitchExpressionData
              => new TheoryData<Point, string>
              {
                  { new Point { X = 0, Y = 0 }, "origin" },
                  { new Point { X = 1, Y = 2 }, "(1, 2)" },
                  { new Point { X = 3, Y = 0 }, "(3, 0)" },
              };
    }
}
