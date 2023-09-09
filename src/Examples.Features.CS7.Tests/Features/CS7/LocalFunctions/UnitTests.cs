using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChainingAssertion;
using Xunit;

// for C# 7.0

namespace Examples.Features.CS7.LocalFunctions
{
    /// <summary>
    /// Tests for C# 7.0, Local functions.
    /// </summary>
    /// <seealso href="https://ufcpp.net/study/csharp/functional/fun_localfunctions/" />
    public class UnitTests
    {

        [Fact]
        public void CaseForIteratorArgumentCheck()
        {
            var sequential = Enumerable.Range(0, 10);

            // NG sequential.WhereWithNormal(null), ArgumentNullException is not thrown.
            var enumrable = Extensions.WhereWithNormal(sequential, null);
            // NG An ArgumentNullException is raised when executing a sequence. it's late.
            Assert.Throws<ArgumentNullException>(() => enumrable.Any());

            // OK.
            Assert.Throws<ArgumentNullException>(
                () => Extensions.WhereWithChecked(sequential, null));

            return;
        }

        public static class Extensions
        {
            public static IEnumerable<T> WhereWithNormal<T>(/* this */ IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null) { throw new ArgumentNullException(nameof(source)); }
                if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

                foreach (var x in source)
                    if (predicate(x))
                        yield return x;
            }

            public static IEnumerable<T> WhereWithChecked<T>(/* this */  IEnumerable<T> source, Func<T, bool> predicate)
            {
                if (source == null) { throw new ArgumentNullException(nameof(source)); }
                if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

                IEnumerable<T> Inner()
                {
                    foreach (var x in source)
                        if (predicate(x))
                            yield return x;
                }

                return Inner();
            }
        }

        [Fact]
        public void CaseForCalledOnlyFromOnePlace()
        {
            var sequence = Enumerable.Range(0, 10);

            // old sequence.SelectToArray(x => x * 2);
            var olds = EnumerableExtensions.SelectToArray(sequence.ToArray(), x => x * 2);

            // sequence.SelectToArray7(x => x * 2);
            var actuals = EnumerableExtensions.SelectToArray7(sequence.ToArray(), x => x * 2);

            actuals.SequenceEqual(olds).IsTrue();

            return;
        }

        public static class EnumerableExtensions
        {

            public static U[] SelectToArray<T, U>(/* this */ T[] array, Func<T, U> selector)
            {
                return Select(array, selector).ToArray();
            }

            // only from SelectToArray().
            private static IEnumerable<U> Select<T, U>(IEnumerable<T> source, Func<T, U> selector)
            {
                foreach (var x in source)
                    yield return selector(x);
            }

            // CS7.0 or later
            public static U[] SelectToArray7<T, U>(/*this*/ T[] array, Func<T, U> selector)
            {
                IEnumerable<U> Inner()
                {
                    foreach (var x in array)
                        yield return selector(x);
                }

                return Inner().ToArray();
            }
        }

        [Fact]
        public async Task CaseForAsyncMethodCache()
        {
            // call.
            (await Loader.LoadAsync(100).ConfigureAwait(false)).Is("100");
            // cached.
            (await Loader.LoadAsync(200).ConfigureAwait(false)).Is("100");
            // cached.
            (await Loader.LoadAsync(300).ConfigureAwait(false)).Is("100");

            return;
        }

        private static class Loader
        {
            public static Task<string> LoadAsync(int num)
            {
                async Task<string> Inner()
                {
                    // async call.
                    await Task.Delay(20).ConfigureAwait(false);
                    return num.ToString();
                }

                cachedLoadTask = cachedLoadTask ?? Inner();
                return cachedLoadTask;
            }

            private static Task<string> cachedLoadTask;
        }

    }

}
