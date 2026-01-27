using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples.Features.CSharp73.Tests.OverloadResolutions
{
    /// <summary>
    /// Tests for Overload resolution now has fewer ambiguous cases in C# 7.3.
    /// </summary>
    public class OverloadResolutionsTests
    {
        [Fact]
        public void When_CallingWithGenericConstraints_Then_CorrectOverloadIsResolved()
        {
            // C# 7.2 : error CS0121: The call is ambiguous between the following methods or properties: 'OverloadResolutionExtensions.FirstOrNull<T>(IEnumerable<T>)' and 'StructOverloadResolutionExtensions.FirstOrNull<T>(IEnumerable<T>)'
            // call OverloadResolutionExtensions with the string as a class.
            var actual1 = new[] { "a", "b", "c" }.FirstOrNull();
            Assert.IsType<string>(actual1);

            // C# 7.2 : error CS0121: The call is ambiguous between the following methods or properties: 'OverloadResolutionExtensions.FirstOrNull<T>(IEnumerable<T>)' and 'StructOverloadResolutionExtensions.FirstOrNull<T>(IEnumerable<T>)'
            // call StructOverloadResolutionExtensions with the int as a strict.
            var actual2 = new[] { 1, 2, 3 }.FirstOrNull();
            Assert.IsType<int>(actual2);

            // C# 7.2 : OK
            // call OverloadResolutionExtensions with the int as a strict.
            var actual3 = new[] { 1, 2, 3 }.FirstOrNull(default);
            Assert.IsType<int>(actual3);
        }

        [Fact]
        public void When_CallingStaticAndInstanceMethods_Then_CorrectMethodIsResolved()
        {
            // C# 7.2 : error CS0121: The call is ambiguous between the following methods or properties: 'OverloadResolutionsTests.Invoker.Invoke(Static)' and 'OverloadResolutionsTests.Invoker.Invoke(Instance)'
            var called = Invoker.Invoke();
            Assert.Equal("Called static method.", called);

            // C# 7.2 : error CS0121: The call is ambiguous between the following methods or properties: 'OverloadResolutionsTests.Invoker.Invoke(Static)' and 'OverloadResolutionsTests.Invoker.Invoke(Instance)'
            var instanceCalled = new Invoker().Invoke();
            Assert.Equal("Called instance method.", instanceCalled);
        }

        private struct Instance { }

        private struct Static { }

        private class Invoker
        {
            public static string Invoke(Static _ = default) => "Called static method.";

            public string Invoke(Instance _ = default) => "Called instance method.";

        }
    }

    internal static class OverloadResolutionExtensions
    {
        public struct Dummy { }

        public static T FirstOrNull<T>(this IEnumerable<T> source)
            where T : class
            => source.FirstOrDefault();

        public static T? FirstOrNull<T>(this IEnumerable<T> source, Dummy _ = default)
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
