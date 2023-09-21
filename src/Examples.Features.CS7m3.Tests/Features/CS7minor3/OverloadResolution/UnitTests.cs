using System.Collections.Generic;
using System.Linq;
using ChainingAssertion;
using Xunit;

namespace Examples.Features.CS7minor3.OverloadResolution
{
    /// <summary>
    /// Tests for C# 7.3, Overload resolution now has fewer ambiguous cases.
    /// </summary>
    /// <seealso href="https://ufcpp.net/study/csharp/structured/miscoverloadresolution?p=2"/>
    public class UnitTests
    {

        [Fact]
        public void WhenDistinguishing_WithGenericConstraints()
        {
            new[] { "a", "b", "c" }.FirstOrNull().IsInstanceOf<string>();

            // call StructOverloadResolutionExtensions.
            new[] { 1, 2, 3 }.FirstOrNull().IsInstanceOf<int?>();

            // call OverloadResolutionExtensions.
            new[] { 1, 2, 3 }.FirstOrNull(default).IsInstanceOf<int?>();

            return;
        }

        [Fact]
        public void WhenDistinguishing_WithStaticOrInstanceMethods()
        {
            Invoker.Invoke().Is("Called static method.");
            Invoker.InvokeAlias().Is("Called static method.");

            new Invoker().Invoke().Is("Called instance method.");

            return;
        }


        #region --- Fixtures ---

        private struct Instance { }

        private struct Static { }

        private class Invoker
        {
            public string Invoke(Instance _ = default)
            {
                return "Called instance method.";
            }

            public static string Invoke(Static _ = default)
            {
                return "Called static method.";
            }

            public static string InvokeAlias()
            {
                return Invoke();
            }

        }

        #endregion

    }

    internal static class OverloadResolutionExtensions
    {
        public struct StructDummy { }

        public static T FirstOrNull<T>(this IEnumerable<T> source)
            where T : class
            => source.FirstOrDefault();

        public static T? FirstOrNull<T>(this IEnumerable<T> source, StructDummy _ = default)
            where T : struct
            => source.Select(x => (T?)x).FirstOrDefault();

    }

    internal static class StructOverloadResolutionExtensions
    {
        public static T? FirstOrNull<T>(this IEnumerable<T> source)
            where T : struct
            => source.Select(x => (T?)x).FirstOrDefault();
    }

}
