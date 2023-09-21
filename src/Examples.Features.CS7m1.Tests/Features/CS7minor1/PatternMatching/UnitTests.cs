using ChainingAssertion;
using Xunit;
using Xunit.Sdk;

// for C# 7.1

namespace Examples.Features.CS7minor1.PatternMatching
{
    /// <summary>
    /// Tests for C# 7.1 Pattern matching.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/proposals/csharp-7.1/generics-pattern-match" />
    public class UnitTests
    {
        [Fact]
        public void WhenUsingDeclarationAndTypePatterns_WithGenerics()
        {

            // if statement.
            string DoIfStatement<T>(T input) where T : Base
            {
                string actual = null;
                // 7.0 was an error.
                if (input is Derived1 derived)
                {
                    actual = derived.SayDerived1();
                }
                return actual;
            }

            DoIfStatement(new Derived1()).Is(nameof(Derived1));
            DoIfStatement(new Derived2()).IsNull();
            DoIfStatement(new Derived3()).IsNull();

            // switch statement.
            string DoSwitchStatement<T>(T input) where T : Base
            {
                string actual = null;
                switch (input)
                {
                    // 7.0 was an error.
                    case Derived1 d:
                        actual = d.SayDerived1();
                        break;
                    // 7.0 was an error.
                    case Derived2 d:
                        actual = d.SayDerived2();
                        break;
                    // 7.0 was an error.
                    case Derived3 d:
                        actual = d.SayDerived3();
                        break;
                    default:
                        throw new XunitException("Fail.");
                }
                return actual;
            }

            DoSwitchStatement(new Derived1()).Is(nameof(Derived1));
            DoSwitchStatement(new Derived2()).Is(nameof(Derived2));
            DoSwitchStatement(new Derived3()).Is(nameof(Derived3));

            return;
        }

    }

}
