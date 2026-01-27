using System.Linq;
using Examples.Features.CSharp72.Tests.InModifierOnParameters.Fixtures;
using Xunit;

namespace Examples.Features.CSharp72.Tests.InModifierOnParameters
{
    /// <summary>
    /// Tests for Verifying hidden copy with in modifier parameter in C# 7.2.
    /// </summary>
    public class HiddenCopiesOfMemoryTests
    {
        [Fact]
        public void When_UsingInModifierWithMutableObject_Then_HiddenCopiesOfMemoryForReadOnly()
        {
            var counter = new Counter();

            // call by value.
            {
                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(counter);          // Counter is memory copied.
                    Assert.Equal(1, actual);
                }

                int GetCount(Counter c) => c.IncrementedCount;
            }

            // call by in modifier
            {
                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(counter);        // Counter is memory copied to maintain reading.
                    Assert.Equal(1, actual);
                }

                int GetCount(in Counter c) => c.IncrementedCount;
            }

            // call by ref modifier
            {
                foreach (var i in Enumerable.Range(1, 3))
                {
                    var actual = GetCount(ref counter);   // no copy
                    Assert.Equal(i, actual);
                }

                int GetCount(ref Counter c) => c.IncrementedCount;
            }
        }

        public struct Counter
        {
            private int _count;

            public int IncrementedCount => ++_count;
        }

        [Fact]
        public void When_UsingInModifierWithReadOnlyStruct_Then_AvoidsHiddenCopiesOfMemory()
        {
            var value = new NoReadOnly(100);
            var actual = HiddenCopies.GetValue(value, 999);
            Assert.Equal(100, actual);         // not modified.

            var readValue = new ReadOnly(200);
            var readonlyActual = HiddenCopies.GetValue(readValue, 999);
            Assert.Equal(200, readonlyActual); // not modified.
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
