using System;
using System.Linq;
using ChainingAssertion;
using Examples.Features.CS70.PatternMatching.Fixtures;
using Xunit;

namespace Examples.Features.CS70.PatternMatching
{
    /// <summary>
    /// Tests for var patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class VarPatternsTests
    {

        [Theory]
        [MemberData(nameof(IfExpressionData))]
        public void WhenUsingIfExpressions(int input, int limit, bool expected)
        {
            var actual = IsAcceptable(input, limit);
            actual.Is(expected);

            return;

            // It's not difficult to understand, but I want to write an if.s
            // bool IsAcceptable(int id, int absLimit) =>
            //     SimulateDataFetch(id) is var results
            //         && results.Min() >= -absLimit
            //         && results.Max() <= absLimit;
            bool IsAcceptable(int id, int absLimit)
            {
                // var Pattern: `is var <variable>`
                if (SimulateDataFetch(id) is var results
                    && results.Min() >= -absLimit
                    && results.Max() <= absLimit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            int[] SimulateDataFetch(int id)
            {
                var rand = new Random();
                return Enumerable
                        .Range(start: 0, count: 5)
                        .Select(s => rand.Next(minValue: -10, maxValue: 11))
                        .ToArray();
            }

        }

        public static TheoryData<int, int, bool> IfExpressionData
            => new TheoryData<int, int, bool>
            {
                { 1, 11, true},
                { 2, 0, false},
            };


        [Theory]
        [MemberData(nameof(SwitchStatementsData))]
        public void WhenUsingSwitchStatements(Point input, Point expected)
        {
            var actual = Transform(() => input);
            actual.IsStructuralEqual(expected);

            return;

            Point Transform(Func<Point> factory)
            {
                switch (factory())
                {
                    // var Pattern: `case var <variable>`
                    case var p when p.X < p.Y:
                        return new Point(-p.X, p.Y);
                    case var p when p.X > p.Y:
                        return new Point(p.X, -p.Y);
                    case var p:
                        return new Point(p.X, p.Y);
                    default:
                        throw new ArgumentException($"Not supported value", nameof(factory));
                }
            };
        }

        public static TheoryData<Point, Point> SwitchStatementsData
            => new TheoryData<Point, Point>
            {
                { new Point(1, 2), new Point(-1, 2) },
                { new Point(5, 2), new Point(5, -2) },
                { new Point(12, 12), new Point(12, 12) },
            };
    }
}
