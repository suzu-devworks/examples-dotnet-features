using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Examples.Features.CSharp70.Tests.LocalFunctions
{
    /// <summary>
    /// Tests for Local functions in C# 7.0.
    /// </summary>｀
    public class LocalFunctionsTests
    {
        [Fact]
        public void When_UsingLocalFunctions_Then_CanBeCalledRecursively()
        {
            Assert.Equal(120, Factorial(5));

            int Factorial(int n)
            {
                return n == 0 ? 1 : n * Factorial(n - 1);
            }
        }

        [Fact]
        public void When_UsingLocalFunctions_Then_CanReturnIterator()
        {
            var evens = GetEvenNumbersUpTo(10).ToArray();
            var expected = new[] { 0, 2, 4, 6, 8, 10 };
            Assert.Equal(expected, evens);

            IEnumerable<int> GetEvenNumbersUpTo(int max)
            {
                for (int i = 0; i <= max; i += 2)
                {
                    yield return i;
                }
            }
        }

        [Fact]
        public async Task When_UsingLocalFunctions_Then_CanBeCalledAsyncMethod()
        {
            var data = await FetchDataAsync(42);
            Assert.Equal("Data for ID: 42", data);

            async Task<string> FetchDataAsync(int id)
            {
                await Task.Delay(100); // Simulate async work
                return $"Data for ID: {id}";
            }
        }

        [Fact]
        public async Task When_UsingIteratorWithLocalFunction_Then_CanBeCheckedImmediately()
        {
            var oddNumbersWithoutLocal = OddSequenceWithoutLocal(20, 10);

            // No exceptions will occur until the sequence is processed.
            await Assert.ThrowsAnyAsync<ArgumentException>(() =>
            {
                foreach (var _ in oddNumbersWithoutLocal)
                {
                    // Force enumeration to trigger exception
                }
                Assert.Fail("Expected exception was not thrown.");
                return Task.CompletedTask;
            });

            // You can check the sequence at the time it was created.
            await Assert.ThrowsAnyAsync<ArgumentException>(() =>
            {
                var _ = OddSequenceWithLocal(20, 10);
                Assert.Fail("Expected exception was not thrown.");
                return Task.CompletedTask;
            });

            var oddNumbers = OddSequenceWithLocal(10, 20);
            var expected = new[] { 11, 13, 15, 17, 19 };
            Assert.Equal(expected, oddNumbers.ToArray());
        }

        public static IEnumerable<int> OddSequenceWithoutLocal(int start, int end)
        {
            if (start < 0 || start > 99)
                throw new ArgumentOutOfRangeException(nameof(start), "start must be between 0 and 99.");
            if (end > 100)
                throw new ArgumentOutOfRangeException(nameof(end), "end must be less than or equal to 100.");
            if (start >= end)
                throw new ArgumentException("start must be less than end.");

            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 1)
                    yield return i;
            }
        }

        public static IEnumerable<int> OddSequenceWithLocal(int start, int end)
        {
            if (start < 0 || start > 99)
                throw new ArgumentOutOfRangeException(nameof(start), "start must be between 0 and 99.");
            if (end > 100)
                throw new ArgumentOutOfRangeException(nameof(end), "end must be less than or equal to 100.");
            if (start >= end)
                throw new ArgumentException("start must be less than end.");

            return GetOddSequenceEnumerator();

            IEnumerable<int> GetOddSequenceEnumerator()
            {
                for (int i = start; i <= end; i++)
                {
                    if (i % 2 == 1)
                        yield return i;
                }
            }
        }

    }
}
