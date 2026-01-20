using Xunit;

namespace Examples.Features.CSharp72.Tests.InModifierOnParameters
{
    /// <summary>
    /// Tests for Add the in modifier on parameters in C# 7.2.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/keywords/method-parameters#in-parameter-modifier"/>
    public class InModifierOnParametersTests
    {
        [Fact]
        public void When_UsingAnyModifiers_Then_ModifiersBehaveCorrectly()
        {
            // call by value.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(input);

                Assert.Equal(0, input.Values);         // not modified.

                void UpdateParams(LargeStructure param)
                {
                    Assert.Equal(0, param.Values);

                    param = new LargeStructure() { Values = 900 };

                    Assert.Equal(900, param.Values);
                }
            }

            // call by in modifier.
            {
                var input = new LargeStructure() { Values = 0 };

                // UpdateParams(in input);
                UpdateParams(input);    // `in` can be omitted.

                Assert.Equal(0, input.Values);  // not modified.

                void UpdateParams(in LargeStructure param)
                {
                    Assert.Equal(0, param.Values);

                    // error CS8331 Cannot assign to variable 'in LargeStructure' because it is a readonly variable.
                    //param = new LargeStructure() { Values = 901 };

                    // error CS8332 Cannot assign to member 'Values' because it is a 'in' parameter
                    // param.Values = 901;

                    Assert.Equal(0, param.Values);
                }
            }

            // call by ref modifier.
            {
                var input = new LargeStructure() { Values = 0 };

                UpdateParams(ref input);
                // error CS1620: Argument 1 must be passed with the 'ref' keyword
                //UpdateParams(input);

                Assert.Equal(902, input.Values);       // modified.

                void UpdateParams(ref LargeStructure param)
                {
                    Assert.Equal(0, param.Values);

                    param = new LargeStructure() { Values = 902 };

                    Assert.Equal(902, param.Values);
                }
            }

            // call by out modifier.
            {
#pragma warning disable IDE0059 // Remove unnecessary value assignment.
                var input = new LargeStructure() { Values = 0 };
#pragma warning restore IDE0059 // Remove unnecessary value assignment.

                UpdateParams(out input);
                // error CS1620: Argument 1 must be passed with the 'out' keyword
                //UpdateParams(input);

                Assert.Equal(903, input.Values);       // output.

                UpdateParams(out var input2);

                Assert.Equal(903, input2.Values);      // output.

                void UpdateParams(out LargeStructure param)
                {
                    // error: CS0170: Use of unassigned out parameter 'param'
                    // Assert.Equal(0, param.Values);

                    param = new LargeStructure() { Values = 903 };

                    Assert.Equal(903, param.Values);
                }
            }
        }

        [Fact]
        public void When_UsingInModifierWithImplicitTypeConversions_Then_CorrectParameterPassed()
        {
            string actual;

            actual = InParamOnly.Method(5); // OK, temporary variable created.
            Assert.Equal("called in parameter is 5.", actual);
            // error CS1503: no implicit conversion from long to int
            //actual = InParamOnly.Method(5L);

            short s = 1;
            actual = InParamOnly.Method(s); // OK, temporary int created with the value 0
            Assert.Equal("called in parameter is 1.", actual);
            // error CS1503: cannot convert from in short to in int
            //actual = InParamOnly.Method(in s);

            int i = 42;
            actual = InParamOnly.Method(i); // passed by readonly reference
            Assert.Equal("called in parameter is 42.", actual);
            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            actual = InParamOnly.Method(in i); // passed by readonly reference, explicitly using `in`
            Assert.Equal("called in parameter is 42.", actual);
        }

        private static class InParamOnly
        {
            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static string Method(in int argument)
            {
                return $"called in parameter is {argument}.";
            }
        }

        [Fact]
        public void When_UsingInModifierWithImplicitTypeConversionsAndOverload_Then_ResultsWillChange()
        {
            string actual;

            actual = Overloads.Method(5); // Calls overload passed by value.
            Assert.Equal("called by value parameter is 5.", actual);

            // error CS1503: no implicit conversion from long to int
            //Method(5L);

            short s = 1;
            actual = Overloads.Method(s); // Calls overload passed by value.
            Assert.Equal("called by value parameter is 1.", actual);
            // error CS1503: cannot convert from in short to in int
            //Method(in s);

            int i = 42;
            actual = Overloads.Method(i); // Calls overload passed by value.
            Assert.Equal("called by value parameter is 42.", actual);
            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            actual = Overloads.Method(in i); // passed by readonly reference, explicitly using `in`
            Assert.Equal("called in parameter is 42.", actual);
        }

        private static class Overloads
        {
            public static string Method(int argument)
            {
                return $"called by value parameter is {argument}.";
            }

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static string Method(in int argument)
            {
                return $"called in parameter is {argument}.";
            }
        }

        public struct LargeStructure
        {
            public int Values;
        }

    }
}
