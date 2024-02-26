using System;
using ChainingAssertion;
using Examples.Features.CS71.PatternMatching.Fixtures;
using Xunit;

namespace Examples.Features.CS71.PatternMatching
{
    /// <summary>
    /// Tests type declaration and patterns for pattern matching in C# 7.1.
    /// </summary>
    public class DeclarationAndTypePatternsTests
    {
        [Fact]
        public void WhenUsingIfExpressions_WithGenericTypeParameters()
        {
            string actual;

            actual = DoIfStatement(new Derived1());
            actual.Is(nameof(Derived1));

            actual = DoIfStatement(new Derived2());
            actual.IsNull();

            actual = DoIfStatement(new Derived3());
            actual.IsNull();

            return;

            string DoIfStatement<T>(T input) where T : Based
            {
                string result = null;

                // C# 7.0 : error CS8314: An expression of type 'T' cannot be handled by a pattern of type 'Derived1' in C# 7.0. Please use language version 7.1 or greater.
                if (input is Derived1 derived)
                {
                    result = derived.SayDerived1();
                }

                return result;
            }
        }

        [Fact]
        public void WhenUsingSwitchStatements_WithGenericTypeParameters()
        {
            string actual;

            actual = DoSwitchStatement(new Derived1());
            actual.Is(nameof(Derived1));

            actual = DoSwitchStatement(new Derived2());
            actual.Is(nameof(Derived2));

            actual = DoSwitchStatement(new Derived3());
            actual.Is(nameof(Derived3));

            return;

            string DoSwitchStatement<T>(T input) where T : Based
            {
                string result = null;
                switch (input)
                {
                    // C# 7.0 : error CS8314: An expression of type 'T' cannot be handled by a pattern of type 'Derived1' in C# 7.0. Please use language version 7.1 or greater.
                    case Derived1 d:
                        result = d.SayDerived1();
                        break;
                    // C# 7.0 : error CS8314: An expression of type 'T' cannot be handled by a pattern of type 'Derived2' in C# 7.0. Please use language version 7.1 or greater.
                    case Derived2 d:
                        result = d.SayDerived2();
                        break;
                    // C# 7.0 : error CS8314: An expression of type 'T' cannot be handled by a pattern of type 'Derived3' in C# 7.0. Please use language version 7.1 or greater.
                    case Derived3 d:
                        result = d.SayDerived3();
                        break;
                    default:
                        throw new ArgumentException($"Not supported value is {input}", nameof(input));
                }
                return result;
            }

        }

    }

}
