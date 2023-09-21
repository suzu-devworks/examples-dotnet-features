using System;
using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;
using Xunit.Sdk;

// for C# 7.0

namespace Examples.Features.CS7.PatternMatching
{
    /// <summary>
    /// Tests for C# 7.0 Pattern matching.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/proposals/csharp-7.0/pattern-matching" />
    public class UnitTests
    {

        [Fact]
        public void DeclarationPattern()
        {
            object input = new[] { "Hello, World!" };

            // if statement.
            {
                string actual = null;
                // Declaration Pattern: is <type> <variable>`.
                if (input is IEnumerable<string> messages)
                {
                    actual = messages.FirstOrDefault();
                }
                actual.Is("Hello, World!");
            }

            // switch statement.
            {
                string actual;
                switch (input)
                {
                    // `Declaration Pattern: case <type> <variable>:`
                    case string message:
                        actual = message;
                        break;
                    // `Declaration Pattern: case <type> <variable>:`
                    case IEnumerable<string> messages:
                        actual = messages.FirstOrDefault();
                        break;
                    default:
                        throw new XunitException("Fail.");
                }
                actual.Is("Hello, World!");
            }

            return;
        }

        [Fact]
        public void ConstantPattern()
        {
            object input = 103;

            // if statement.
            {
                string actual = null;
                // Constant Pattern: is <constant>`.
                if (input is 103)
                {
                    actual = input.ToString();
                }
                actual.Is("103");
            }

            // switch statement.
            {
                string actual;
                switch (input)
                {
                    // Constant Pattern: case <constant>:`.
                    case 103:
                        actual = input.ToString();
                        break;
                    default:
                        throw new XunitException("Fail.");
                }
                actual.Is("103");
            }

            return;
        }

        [Fact]
        public void NullConstantPattern()
        {
            int? input = null;

            // if statement.
            {
                string actual = null;
                // Null Constant Pattern: is null`.
                if (input is null)
                {
                    actual = "NULL";
                }
                actual.Is("NULL");
            }

            // switch statement.
            {
                string actual;
                switch (input)
                {
                    // Null Constant Pattern: case null:`.
                    case null:
                        actual = "NULL";
                        break;
                    case int _:
                        throw new XunitException("Fail.");
                    default:
                        throw new XunitException("Fail.");
                }
                actual.Is("NULL");
            }

            return;
        }


        [Fact]
        public void VarPattern()
        {
            var rand = new Random();
            var input = Enumerable.Range(0, 5)
                        .Select(s => rand.Next(minValue: -10, maxValue: 11));

            // This is useful when you need a temporary variable in a Boolean expression
            // that holds the result of an intermediate calculation.

            // if statement.
            {
                string actual = null;
                // Var Pattern: is var <variable>`.
                if (input is var x && -10 <= x.Min() && x.Max() <= 11)
                {
                    actual = "OK";
                }
                actual.Is("OK");
            }

            // switch statement.
            {
                string actual = null;
                switch (input)
                {
                    // Var Pattern: case var <variable>: when <expression>`.
                    case var x when -10 <= x.Min() && x.Max() <= 11:
                        actual = "OK";
                        break;
                    default:
                        throw new XunitException("Fail.");
                }
                actual.Is("OK");
            }

            return;
        }

    }

}
