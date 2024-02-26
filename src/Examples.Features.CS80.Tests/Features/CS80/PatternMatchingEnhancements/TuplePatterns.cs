using System;
using ChainingAssertion;
using Examples.Features.CS80.PatternMatchingEnhancements.Fixtures;
using Xunit;

namespace Examples.Features.CS80.PatternMatchingEnhancements
{
    /// <summary>
    /// Tests for Tuple(Positional) patterns of pattern matching in C# 8.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class TuplePatterns
    {
        [Theory]
        [MemberData(nameof(SwitchExpressionsData))]
        public void WhenUsingSwitchExpressions(Point input, string? expected)
        {
            var actual = GetClassify(input);
            actual.Is(expected);

            return;

            static string GetClassify(Point point)
            {
                // Specify a tuple, but you can also define a deconstructor.
                return point switch
                {
                    // Tuple patterns : (item1, item2, item3 ... ) => ...
                    (0, 0) => "Origin",
                    (1, 0) => "positive X basis end",
                    (0, 1) => "positive Y basis end",
                    _ => "Just a point",
                };
            }
        }

        public static TheoryData<Point, string?> SwitchExpressionsData
           => new TheoryData<Point, string?>
            {
                { new Point { X = 0, Y = 0 }, "Origin" },
                { new Point { X = 0, Y = 1 }, "positive Y basis end" },
                { new Point { X = 1, Y = 0 }, "positive X basis end" },
                { new Point { X = 1, Y = 1 }, "Just a point" }
            };


        [Fact]
        public void WhenCasingForStateMachine()
        {
            // Mads Torgersen's "Do More with Patterns in C# 8.0"

            NewState(State.Locked, Operation.Open, new StateKey { IsValid = true })
                .Is(State.Opened);

            NewState(State.Opened, Operation.Close, new StateKey { IsValid = false })
                .Is(State.Closed);

            NewState(State.Opened, Operation.Lock, new StateKey { IsValid = false })
                .Is(State.Opened);

            return;

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
