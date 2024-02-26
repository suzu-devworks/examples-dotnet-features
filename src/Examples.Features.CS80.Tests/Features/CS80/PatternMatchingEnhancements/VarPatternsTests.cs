using System;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS80.PatternMatchingEnhancements
{
    /// <summary>
    /// Tests for var patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class VarPatternsTests
    {
        [Theory]
        [MemberData(nameof(IfExpressionData))]
        public void WhenUsingSwitchExpressions(int input, int limit, bool expected)
        {
            var actual = IsAcceptable(input, limit);
            actual.Is(expected);

            return;

            static bool IsAcceptable(int id, int absLimit)
            {
                return SimulateDataFetch(id) switch
                {
                    // var Pattern: `var <variable> when .... => ...`
                    var x when -absLimit <= x.Min() && x.Max() <= absLimit => true,
                    _ => false,
                };
            }

            static int[] SimulateDataFetch(int id)
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

    }
}
