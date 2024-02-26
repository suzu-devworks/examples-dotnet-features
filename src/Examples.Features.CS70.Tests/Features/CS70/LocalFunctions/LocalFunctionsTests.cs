using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS70.LocalFunctions
{
    /// <summary>
    /// Tests for Local functions in C# 7.0.
    /// </summary>
    public class LocalFunctionsTests
    {
        [Fact]
        public void WhenUsedForImmediateArgumentCheckingOfQuery()
        {
            var query = Enumerable.Range(0, 10);

            // Executing the sequence results in an ArgumentNullException.
            // Not when creating the query.
            var enumerable = query.WhereWithLateCheck(null);

            Assert.Throws<ArgumentNullException>(() => enumerable.Any());

            // OK.
            Assert.Throws<ArgumentNullException>(
                () => query.WhereWithImmediateCheck(null));

            return;
        }

        [Fact]
        public void WhenUsedToLocalizeMethods()
        {
            var query = Enumerable.Range(0, 10);
            var actual = query.SelectToArray(x => x * 2);

            actual.IsStructuralEqual(new[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18 });

            return;
        }

        [Fact]
        public async Task WhenUsedToCachingAsynchronousTasks()
        {
            var loader = new CachingLoader();
            var actual1 = await loader.LoadAsync(100);
            var actual2 = await loader.LoadAsync(200); // cached
            var actual3 = await loader.LoadAsync(300); // cached

            actual1.Is("100");
            actual2.Is(x => x != "200" && x == "100");
            actual3.Is(x => x != "300" && x == "100");

            return;
        }

        private class CachingLoader
        {
            public Task<string> LoadAsync(int num)
            {
                _cached = _cached ?? Inner();
                return _cached;

                async Task<string> Inner()
                {
                    // call any async methods.
                    await Task.CompletedTask;

                    return num.ToString();
                }
            }

            private Task<string> _cached;
        }

    }

    internal static class Extensions
    {
        public static IEnumerable<T> WhereWithLateCheck<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // This check does not take effect until the sequence is started.
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            foreach (var x in source)
                if (predicate(x))
                    yield return x;
        }

        public static IEnumerable<T> WhereWithImmediateCheck<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            // This check is immediately.
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            return Inner();

            IEnumerable<T> Inner()
            {
                foreach (var x in source)
                    if (predicate(x))
                        yield return x;
            }
        }

        public static U[] SelectToArray<T, U>(this IEnumerable<T> source, Func<T, U> selector)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }
            if (selector == null) { throw new ArgumentNullException(nameof(selector)); }

            return Inner().ToArray();

            IEnumerable<U> Inner()
            {
                foreach (var x in source)
                    yield return selector(x);
            }
        }
    }

}
