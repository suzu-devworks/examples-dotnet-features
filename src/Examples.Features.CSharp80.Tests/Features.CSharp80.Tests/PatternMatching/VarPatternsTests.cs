using System;
using System.Linq;
using Xunit;

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for var patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class VarPatternsTests
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_ValidatesAcceptability(int input, int limit, bool expected)
        {
            var actual = IsAcceptable(input, limit);
            Assert.Equal(expected, actual);

            static bool IsAcceptable(int id, int absLimit)
            {
                // C# 8.0 now allows you to use pattern matching in switch expressions.
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

        public static TheoryData<int, int, bool> SwitchExpressionData
            => new TheoryData<int, int, bool>
            {
                { 1, 11, true},
                { 2, 0, false},
            };

    }
}
