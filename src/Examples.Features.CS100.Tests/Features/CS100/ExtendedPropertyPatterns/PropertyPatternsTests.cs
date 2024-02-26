using Examples.Features.CS100.ExtendedPropertyPatterns.Fixtures;

namespace Examples.Features.CS100.ExtendedPropertyPatterns;

/// <summary>
/// Tests for Extended property patterns in C# 10.0.
/// </summary>
public class PropertyPatternsTests
{
    [Theory]
    [MemberData(nameof(IfExpressionsData))]
    public void WhenUsingIfExpressions_WithNestedProperty(Segment input, bool expected)
    {
        var actual8 = IsAnyEndOnXAxis8(input);
        actual8.Should().Be(expected);

        var actual10 = IsAnyEndOnXAxis10(input);
        actual10.Should().Be(expected);

        return;

        static bool IsAnyEndOnXAxis8(Segment segment)
        {
            // Property patterns is equals: is { property: value }
            // C# 8.0 or later
#pragma warning disable IDE0170 // Simplify property pattern
            return segment is { Start: { Y: 0 } } || segment is { End: { Y: 0 } };
#pragma warning restore IDE0170
        }

        static bool IsAnyEndOnXAxis10(Segment segment)
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
