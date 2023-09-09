using ChainingAssertion;
using Xunit;

#pragma warning disable IDE0059 // Unnecessary assignment of a value to 'xxx'
#pragma warning disable IDE0060 // Remove unused parameter 'xxx'

// for C# 7.2

namespace Examples.Features.CS7minor2.InModifierParameters
{
    /// <summary>
    /// Tests for C# 7.2, The in modifier on parameters.
    /// </summary>
    /// <seealso href="https://qiita.com/med-orange/items/b49ca4fc0e8604d83eb2""/>
    /// <seealso href="https://ufcpp.net/study/csharp/sp_ref.html#in-copy""/>
    public class InModifierOnParametersTests
    {

        [Fact]
        public void WhenUsingInParaeters_DoesNotAffectTheCaller()
        {
            // define function for in parameter.
            void DoSomething(
                LargeStructure x1,      // IDE0060
                in LargeStructure x2,   // IDE0060
                in LargeStructure x3,   // IDE0060
                ref LargeStructure x4,
                out LargeStructure x5
            )
            {
                x1 = new LargeStructure() { Values = 109 };    // IDE0059

                // error CS8331 Cannot assign to variable 'in LargeStructure' because it is a readonly variable.
                //x2 = new LargeStructure() { Values = 200 };
                //x3 = new LargeStructure() { Values = 300 };

                x4 = new LargeStructure() { Values = 400 };
                x5 = new LargeStructure() { Values = 500 };

                return;
            }

            var val1 = new LargeStructure() { Values = 10 };
            var val2 = new LargeStructure() { Values = 20 };
            var val3 = new LargeStructure() { Values = 30 };
            var val4 = new LargeStructure() { Values = 40 };
            var val5 = new LargeStructure() { Values = 50 };    // IDE0059

            DoSomething(val1, val2, in val3, ref val4, out val5);

            val1.Values.Is(10);
            val2.Values.Is(20);
            val3.Values.Is(30);
            val4.Values.Is(400);    // update
            val5.Values.Is(500);    // update

            return;
        }

        [Fact]
        public void WhenUsingInParaeters_HasHiddenCopy()
        {
            string DoSomething(in LargeStructure value)
            {
                // hidden copy. @value = value;
                return value.DoFunc();
            }

            DoSomething(new LargeStructure { Values = 100 }).Is("100");

            string DoRedonlySomethig(in ReadonlyLargeStructure value)
            {
                // no-copy.
                return value.DoFunc();
            };

            DoRedonlySomethig(new ReadonlyLargeStructure(200)).Is("200");

            return;
        }

    }
}
