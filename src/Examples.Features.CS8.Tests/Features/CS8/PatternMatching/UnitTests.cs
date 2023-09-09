using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;
using Xunit.Sdk;

// for C# 8.0

namespace Examples.Features.CS8.PatternMatching
{
    /// <summary>
    /// Tests for C# 8.0, Pattern matching enhancements.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    /// <seealso href="https://learn.microsoft.com/ja-jp/archive/msdn-magazine/2019/may/csharp-8-0-pattern-matching-in-csharp-8-0" />
    public class UnitTests
    {
        [Fact]
        public void WhenUsingSwitchExpressions_WithDeclarationPattern()
        {
            object input = new[] { "Hello, World!" };

            // switch sxpressions.
            {
                var actual = (input) switch
                {
                    string message => message,
                    IEnumerable<string> messages => messages.FirstOrDefault(),
                    _ => throw new XunitException("Fail."),
                };

                actual!.Is("Hello, World!");
            }

            return;
        }

        [Fact]
        public void WhenUsingSwitchExpressions_WithVarPattern()
        {
            var rand = new Random();
            var input = Enumerable.Range(0, 5)
                        .Select(s => rand.Next(minValue: -10, maxValue: 11));

            // switch sxpressions.
            {
                var actual = (input) switch
                {
                    var x when -10 <= x.Min() && x.Max() <= 11 => "OK",
                    _ => throw new XunitException("Fail."),
                };
                actual.Is("OK");
            }

            return;
        }

        [Fact]
        public void PropertyPattern()
        {
            // Property patterns is equals: is { property: value }
            static bool IsAnyEndOnXAxis(Segment segment) =>
                segment is { Start: { Y: 0 } } || segment is { End: { Y: 0 } };

            IsAnyEndOnXAxis(new Segment
            {
                Start = new Point { X = 100, Y = 200 },
                End = new Point { X = 300, Y = 0 },
            })
            .IsTrue();

            IsAnyEndOnXAxis(new Segment
            {
                Start = new Point { X = 100, Y = 0 },
                End = new Point { X = 300, Y = 400 },
            })
            .IsTrue();

            IsAnyEndOnXAxis(new Segment
            {
                Start = new Point { X = 100, Y = 200 },
                End = new Point { X = 300, Y = 400 },
            })
            .IsFalse();

            return;
        }

        [Fact]
        public void PositionalPattern()
        {
            Shape shape = new Rectangle
            {
                Width = 100,
                Height = 100,
                Point = new Point { X = 0, Y = 100 }
            };

            // use Rectangle.Deconstruct()
            var classify = shape switch
            {
                // Positional patterns : type (item1, item2, item3 ... )
                Rectangle(100, 100, null) => "Found 100x100 rectangle without a point",
                Rectangle(100, 100, _) => "Found 100x100 rectangle",
                _ => "Different, or null shape"
            };

            classify.Is("Found 100x100 rectangle");

            return;
        }

        [Fact]
        public void TuplePattern()
        {
            var point = new Point { X = 0, Y = 1 };

            var classify = point switch
            {
                // Tuple patterns : (item1, item2, item3 ... )
                (0, 0) => "Origin",
                (1, 0) => "positive X basis end",
                (0, 1) => "positive Y basis end",
                _ => "Just a point",
            };

            classify.Is("positive Y basis end");

            return;
        }

        [Fact]
        public void CaseForStatemachine_WithTuplePattern()
        {
            // Mads Torgersen's "Do More with Patterns in C# 8.0"
            static State NewState(State state, Operation operation, StateKey key)
            {
                var newState = (state, operation, key.IsValid) switch
                {
                    (State.Opened, Operation.Close, _) => State.Closed,
                    (State.Opened, Operation.Open, _) => throw new Exception("Can't open an opened door"),
                    (State.Opened, Operation.Lock, true) => State.Locked,
                    (State.Locked, Operation.Open, true) => State.Opened,
                    (State.Closed, Operation.Open, false) => State.Locked,
                    (State.Closed, Operation.Lock, true) => State.Locked,
                    (State.Closed, Operation.Close, _) => throw new Exception("Can't close a closed door"),
                    _ => state
                };

                return newState;
            }

            NewState(State.Locked, Operation.Open, new StateKey { IsValid = true })
                .Is(State.Opened);

            NewState(State.Opened, Operation.Close, new StateKey { IsValid = false })
                .Is(State.Closed);

            NewState(State.Opened, Operation.Lock, new StateKey { IsValid = false })
                .Is(State.Opened);

            return;
        }

    }

}

