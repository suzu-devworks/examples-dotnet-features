using System;
using Xunit;

namespace Examples.Features.CSharp73.Tests.InitializersOnStackallocArrays
{
    /// <summary>
    /// Tests for Using initializers on stackalloc arrays in C# 7.3.
    /// </summary>
    public class InitializersOnStackallocArraysTests
    {
        [Fact]
        public void When_UsingStackallocWithInitializers_Then_InitializationWorks()
        {
            // OK
            Span<int> span1 = stackalloc int[3];
            Assert.Equal(3, span1.Length);

            // C# 7.2 : error CS8320: Feature 'stackalloc initializer' is not available in C# 7.2. Please use language version 7.3 or greater.

            Span<int> span2 = stackalloc int[3] { 1, 2, 3 };
            Assert.Equal(3, span2.Length);

            Span<int> span3 = stackalloc int[] { 1, 2, 3 };
            Assert.Equal(3, span3.Length);

            Span<int> span4 = stackalloc[] { 1, 2, 3 };
            Assert.Equal(3, span4.Length);
        }
    }
}
