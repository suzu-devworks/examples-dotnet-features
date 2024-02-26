using System.Linq;
using ChainingAssertion;
using Examples.Features.CS72.InModifierOnParameters.Fixtures;
using Xunit;

namespace Examples.Features.CS72.InModifierOnParameters
{
    /// <summary>
    /// Tests for Verifying hidden copy with in modifier parameter in C# 7.2.
    /// </summary>
    public class HiddenCopiesOfMemoryTests
    {
        [Fact]
        public void WhenUsingInModifier_WithMutableObject_HiddenCopiesOfMemoryForReadOnly()
        {
            var counter = new Counter();

            // call by value.
            {
                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(counter);          // Counter is memory copied.
                    actual.Is(1);
                }

                int GetCount(Counter c) => c.IncrementedCount;
            }

            // call by in modifier
            {
                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(counter);        // Counter is memory copied to maintain reading.
                    actual.Is(1);
                }

                int GetCount(in Counter c) => c.IncrementedCount;
            }

            // call by ref modifier
            {

                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(ref counter);   // no copy
                    actual.Is(i);
                }

                int GetCount(ref Counter c) => c.IncrementedCount;
            }

            return;
        }

        [Fact]
        public void WhenUsingInModifier_WithReadOnlyStruct_AvoidsHiddenCopiesOfMemory()
        {
            var value = new NoReadOnly(100);
            var actual = HiddenCopies.GetValue(value, 999);
            actual.Is(100);         // not modified.

            var readValue = new ReadOnly(200);
            var readonlyActual = HiddenCopies.GetValue(readValue, 999);
            readonlyActual.Is(200); // not modified.

            return;
        }

        private static class HiddenCopies
        {
            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static int GetValue(in NoReadOnly input, int modifier)
            {
                // Since in is added, it is treated as readonly
                input.SetValue(modifier);   // copy occurs when calling SetValue()
                return input.Value;         // input is not modified.
            }

            // C# 7.1 : error CS8302: Feature 'readonly references' is not available in C# 7.1. Please use language version 7.2 or greater.
            public static int GetValue(in ReadOnly input, int modifier)
            {
                // read-only structures cannot be modified internally and are not copied.
                input.SetValue(modifier);
                return input.Value;
            }
        }

    }
}
