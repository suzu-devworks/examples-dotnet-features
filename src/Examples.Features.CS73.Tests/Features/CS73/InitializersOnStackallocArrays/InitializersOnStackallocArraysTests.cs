using System;
using Xunit;

namespace Examples.Features.CS73.InitializersOnStackallocArrays
{
    /// <summary>
    /// Tests for Using initializers on stackalloc arrays in C# 7.3.
    /// </summary>
    public class InitializersOnStackallocArraysTests
    {
        [Fact]
        public void BasicUsage()
        {
            // OK
            Span<int> array1 = stackalloc int[3];

            // C# 7.2 : error CS8320: Feature 'stackalloc initializer' is not available in C# 7.2. Please use language version 7.3 or greater.
            Span<int> array2 = stackalloc int[3] { 1, 2, 3 };
            Span<int> array3 = stackalloc int[] { 1, 2, 3 };
            Span<int> array4 = stackalloc[] { 1, 2, 3 };

            return;
        }
    }
}
