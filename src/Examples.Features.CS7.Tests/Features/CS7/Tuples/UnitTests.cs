using System.Collections.Generic;
using ChainingAssertion;
using Xunit;

#pragma warning disable IDE0042 //Variable declaration can be deconstructed

namespace Examples.Features.CS7.Tuples
{
    /// <summary>
    /// Tests for C# 7.0 Tuples and deconstruction.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/builtin-types/value-tuples"/>
    public class TuplesAndDeconstructionTests
    {

        [Fact]
        public void WhenUsingNamedTuple()
        {
            // define anonymous tuple.
            (double, int) t1 = (4.5, 3);

            t1.Item1.Is(4.5);
            t1.Item2.Is(3);

            // define named tuple.
            (double Sum, int Count) t2 = (4.5, 3);

            t2.Sum.Is(4.5);
            t2.Count.Is(3);

            // define named tuple with deconstructed.
            (double sum, int count) = (4.5, 3);

            sum.Is(4.5);
            count.Is(3);

            return;
        }

        [Fact]
        public void WhenAssigned()
        {
            (int, double) t1 = (17, 3.14);

            // Allows implicit conversion from int to double.
            (double First, double Second) t2 = t1;

            t2.First.Is(17.0);
            t2.Second.Is(3.14);

            // Allows implicit conversion from int to decimal.
            (decimal First, double Second) t3 = t1;

            t3.First.Is(17m);
            t3.Second.Is(3.14);

            // error CS0266 Cannot implicitly convert type '(int, double)' to '(int First, int Second)'.
            //(int First, int Second) t4 = t1;

            return;
        }


        [Fact]
        public void WhenCallingDeconstruct_WithExtensions()
        {
            // deconstruct by Extensions.
            var (key, value) = new KeyValuePair<string, int>("one", 1);

            key.Is("one");
            value.Is(1);

            return;
        }

    }

}
