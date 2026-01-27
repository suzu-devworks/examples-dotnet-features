using System;
using System.Linq;
using Examples.Features.CSharp70.Tests.PatternMatching.Fixtures;
using Xunit;

namespace Examples.Features.CSharp70.Tests.PatternMatching
{
    /// <summary>
    /// Tests for var patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class VarPatternsTests
    {
        [Fact]
        public void When_EvaluatedInIfExpression_Then_IntermediateResultIsCaptured()
        {
            Assert.True(IsAcceptable(1, 11));
            Assert.False(IsAcceptable(2, 0));

            bool IsAcceptable(int id, int absLimit) =>
                // C# 7.0 var Pattern.
                SimulateDataFetch(id) is var results
                    && results.Min() >= -absLimit
                    && results.Max() <= absLimit;

            int[] SimulateDataFetch(int id)
            {
                var rand = new Random();
                return Enumerable
                        .Range(start: 0, count: 5)
                        .Select(s => rand.Next(minValue: -10, maxValue: 11))
                        .ToArray();
            }
        }

        [Theory]
        [MemberData(nameof(SwitchStatementData))]
        public void When_EvaluatedInSwitchStatement_Then_IntermediateResultIsCaptured(Point input, Point expected)
        {
            var actual = Transform(input);
            Assert.Equal((expected.X, expected.Y), (actual.X, actual.Y));

            Point Transform(Point point)
            {
                // C# 7.0 var Pattern.
                switch (point)
                {
                    case var p when p is null:
                        return new Point(0, 0);
                    case var p when p.X < p.Y:
                        return new Point(-p.X, p.Y);
                    case var p when p.X > p.Y:
                        return new Point(p.X, -p.Y);
                    case var p:
                        return new Point(p.X, p.Y);
                    default:
                        throw new ArgumentException($"Not supported value", nameof(point));
                }
            }
        }

        public static TheoryData<Point, Point> SwitchStatementData
            => new TheoryData<Point, Point>
            {
                { new Point(1, 2), new Point(-1, 2) },
                { new Point(5, 2), new Point(5, -2) },
                { new Point(12, 12), new Point(12, 12) },
                { null, new Point(0, 0) },
            };
    }
}
