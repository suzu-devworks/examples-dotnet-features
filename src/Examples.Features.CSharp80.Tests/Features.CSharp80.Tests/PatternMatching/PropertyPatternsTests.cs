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
        [MemberData(nameof(IfExpressionsData))]
        public void When_PropertyPatternUsed_Then_DetectsEndOnXAxis(Segment input, bool expected)
        {
            var actual = IsAnyEndOnXAxis(input);
            Assert.Equal(expected, actual);

            // Property patterns is equals: is { property: value }
            static bool IsAnyEndOnXAxis(Segment segment) =>
                segment is { Start: { Y: 0 } } || segment is { End: { Y: 0 } };
        }

        public static TheoryData<Segment, bool> IfExpressionsData
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

    }
}
