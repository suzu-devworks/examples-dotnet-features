using System;
using System.Collections.Generic;
using Xunit;

namespace Examples.Features.CSharp70.Tests.LocalFunctions
{
    /// <summary>
    /// Tests for Enumerable extension methods using Local functions in C# 7.0.
    /// </summary>
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void When_CreatingOwnWhereLinqMethod_Then_CanBeCalled()
        {
            var numbers = new[] { 1, 2, 3, 4, 5 };
            var actual = numbers.WhereImmediateArgumentCheck(x => x > 2);

            var exception = new[] { 3, 4, 5 };
            Assert.Equal(exception, actual);

            Assert.Throws<ArgumentNullException>(() => numbers.WhereImmediateArgumentCheck(null));
        }

        [Fact]
        public void When_CreatingOwnSelectLinqMethod_Then_CanBeCalled()
        {
            var numbers = new[] { 1, 2, 3 };
            var actual = numbers.SelectImmediateArgumentCheck(x => x * x);
            var expected = new[] { 1, 4, 9 };
            Assert.Equal(expected, actual);

            Assert.Throws<ArgumentNullException>(() => numbers.SelectImmediateArgumentCheck<int, int>(null));
        }
    }

    public static class Extensions
    {
        public static IEnumerable<T> WhereImmediateArgumentCheck<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // This check is immediately.
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(predicate);

            return Inner();

            IEnumerable<T> Inner()
            {
                foreach (var x in source)
                    if (predicate(x))
                        yield return x;
            }
        }

        public static IEnumerable<U> SelectImmediateArgumentCheck<T, U>(this IEnumerable<T> source, Func<T, U> selector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(selector);

            return Inner();

            IEnumerable<U> Inner()
            {
                foreach (var x in source)
                    yield return selector(x);
            }
        }
    }
}
