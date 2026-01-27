using Examples.Features.CSharp100.Tests.ExtendedPropertyPatterns.Fixtures;

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable

namespace Examples.Features.CSharp100.Tests.ExtendedPropertyPatterns;

/// <summary>
/// Tests for Extended property patterns in C# 10.0.
/// </summary>
public class PropertyPatternsTests
{
    [Theory]
    [MemberData(nameof(IfExpressionsData))]
    public void When_EvaluatedInIfExpression_Then_MatchesValueFromNestingProperty(Segment input, bool expected)
    {
        var actual8 = IsAnyEndOnXAxisWithCS8(input);
        Assert.Equal(expected, actual8);

        var actual10 = IsAnyEndOnXAxisWithCS10(input);
        Assert.Equal(expected, actual10);

        static bool IsAnyEndOnXAxisWithCS8(Segment segment)
        {
            // Property patterns is equals: is { property: value }
            // C# 8.0 or later
            return segment is { Start: { Y: 0 } } || segment is { End: { Y: 0 } };
        }

        static bool IsAnyEndOnXAxisWithCS10(Segment segment)
        {
            // Property patterns is equals: is { property: value }
            // C# 9.0 : error CS8773: Feature 'extended property patterns' is not available in C# 9.0. Please use language version 10.0 or greater.
            return segment is { Start.Y: 0 } || segment is { End.Y: 0 };
        }
    }

    public static TheoryData<Segment, bool> IfExpressionsData
        => new()
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
