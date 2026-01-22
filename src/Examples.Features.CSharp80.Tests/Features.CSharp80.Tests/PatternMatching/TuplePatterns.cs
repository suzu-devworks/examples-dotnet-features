using System;
using Examples.Features.CSharp80.Tests.PatternMatching.Fixtures;
using Xunit;

#pragma warning disable xUnit1045 // Avoid using TheoryData type arguments that might not be serializable

namespace Examples.Features.CSharp80.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Tuple(Positional) patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class TuplePatterns
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionData))]
        public void When_EvaluatedInSwitchExpression_Then_MatchesValueFromDeconstruction(Point input, string? expected)
        {
            var actual = GetClassify(input);
            Assert.Equal(expected, actual);

            static string GetClassify(Point point)
            {
                // C# 8.0 Tuple Pattern.
                // Specify a tuple, but you can also define a deconstructor.
                return point switch
                {
                    (0, 0) => "Origin",
                    (1, 0) => "positive X basis end",
                    (0, 1) => "positive Y basis end",
                    _ => "Just a point",
                };
            }
        }

        public static TheoryData<Point, string?> SwitchExpressionData
           => new TheoryData<Point, string?>
            {
                { new Point { X = 0, Y = 0 }, "Origin" },
                { new Point { X = 0, Y = 1 }, "positive Y basis end" },
                { new Point { X = 1, Y = 0 }, "positive X basis end" },
                { new Point { X = 1, Y = 1 }, "Just a point" }
            };


        [Fact]
        public void When_EvaluatedInTuplePattern_Then_ActsAsStateMachine()
        {
            // Mads Torgersen's Blog: "Do More with Patterns in C# 8.0"
            // spell-checker: words Mads Torgersen
            Assert.Equal(State.Opened, NewState(State.Locked, Operation.Open, new StateKey { IsValid = true }));
            Assert.Equal(State.Closed, NewState(State.Opened, Operation.Close, new StateKey { IsValid = false }));
            Assert.Equal(State.Opened, NewState(State.Opened, Operation.Lock, new StateKey { IsValid = false }));

            static State NewState(State state, Operation operation, StateKey key)
            {
                var newState = (state, operation, key.IsValid) switch
                {
                    (State.Opened, Operation.Close, _) => State.Closed,
                    (State.Opened, Operation.Open, _) => throw new ArgumentException("Can't open an opened door"),
                    (State.Opened, Operation.Lock, true) => State.Locked,
                    (State.Locked, Operation.Open, true) => State.Opened,
                    (State.Closed, Operation.Open, false) => State.Locked,
                    (State.Closed, Operation.Lock, true) => State.Locked,
                    (State.Closed, Operation.Close, _) => throw new ArgumentException("Can't close a closed door"),
                    _ => state
                };

                return newState;
            }
        }

    }
}
