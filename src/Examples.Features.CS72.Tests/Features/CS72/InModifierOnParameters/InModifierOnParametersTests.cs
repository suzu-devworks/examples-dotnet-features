using ChainingAssertion;
using Examples.Features.CS72.InModifierOnParameters.Fixtures;
using Xunit;

namespace Examples.Features.CS72.InModifierOnParameters
{
    /// <summary>
    /// Tests for Add the in modifier on parameters in C# 7.2.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/method-parameters#in-parameter-modifier"/>
    public class InModifierOnParametersTests
    {
        [Fact]
        public void WhenUsingAnyModifiers()
        {
            // call by value.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(input);

                input.Values.Is(0);         // not modified.

                void UpdateParams(LargeStructure param)
                {
#pragma warning disable IDE0059 // Remove unnecessary value assignment
                    param = new LargeStructure() { Values = 900 };
#pragma warning restore IDE0059
                }
            }

            // call by in modifier.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(in input);
                UpdateParams(input);       // `in` can be omitted.

                input.Values.Is(0);         // not modified.

                void UpdateParams(in LargeStructure param)
                {
                    // error CS8331 Cannot assign to variable 'in LargeStructure' because it is a readonly variable.
                    //param = new LargeStructure() { Values = 901 };
                    _ = param;
                }
            }

            // call by ref modifier.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(ref input);
                // error CS1620: Argument 1 must be passed with the 'ref' keyword
                //UpdateParams(input);

                input.Values.Is(902);       // modified.

                void UpdateParams(ref LargeStructure param)
                {
                    param = new LargeStructure() { Values = 902 };
                }

            }

            // call by out modifier.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(out input);
                // error CS1620: Argument 1 must be passed with the 'out' keyword
                //UpdateParams(input);

                input.Values.Is(903);       // output.

                UpdateParams(out var input2);

                input2.Values.Is(903);      // output.

                void UpdateParams(out LargeStructure param)
                {
                    param = new LargeStructure() { Values = 903 };
                }
            }

            return;
        }

        [Fact]
        public void WhenUsingInModifier_WithImplicitTypeConversions()
        {
            string actual;

            actual = InParamOnly.Method(5); // OK, temporary variable created.
            actual.Is("called in parameter.");
            // error CS1503: no implicit conversion from long to int
            //actual = InParamOnly.Method(5L);

            short s = 0;
            actual = InParamOnly.Method(s); // OK, temporary int created with the value 0
            actual.Is("called in parameter.");
            // error CS1503: cannot convert from in short to in int
            //actual = InParamOnly.Method(in s);

            int i = 42;
            actual = InParamOnly.Method(i); // passed by readonly reference
            actual.Is("called in parameter.");

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            actual = InParamOnly.Method(in i); // passed by readonly reference, explicitly using `in`
            actual.Is("called in parameter.");

            return;
        }

        private static class InParamOnly
        {
            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static string Method(in int argument)
            {
                _ = argument;
                return "called in parameter.";
            }
        }

        [Fact]
        public void WhenUsingInModifier_WithImplicitTypeConversionsAndOverload_ResultsWillChange()
        {
            string actual;

            actual = Overloads.Method(5); // Calls overload passed by value.
            actual.Is("called by value parameter.");

            // error CS1503: no implicit conversion from long to int
            //Method(5L);

            short s = 0;
            actual = Overloads.Method(s); // Calls overload passed by value.
            actual.Is("called by value parameter.");
            // error CS1503: cannot convert from in short to in int
            //Method(in s);

            int i = 42;
            actual = Overloads.Method(i); // Calls overload passed by value.
            actual.Is("called by value parameter.");

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            actual = Overloads.Method(in i); // passed by readonly reference, explicitly using `in`
            actual.Is("called in parameter.");

            return;

        }

        private static class Overloads
        {
            public static string Method(int argument)
            {
                _ = argument;
                return "called by value parameter.";
            }

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static string Method(in int argument)
            {
                _ = argument;
                return "called in parameter.";
            }
        }

    }
}
