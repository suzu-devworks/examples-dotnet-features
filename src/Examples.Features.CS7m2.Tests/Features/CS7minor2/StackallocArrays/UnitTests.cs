using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ChainingAssertion;
using Xunit;

// for C# 7.2

namespace Examples.Features.CS7minor2.StackallocArrays
{
    /// <summary>
    /// Tests for C# 7.2, Initializers on stackalloc arrays.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/stackalloc" />
    public class UnitTests
    {

        [Fact]
        public void WhenCreatingReadBuffer_WithSpan()
        {
            var encoding = Encoding.UTF8;
            var bufferSize = 32;

            // No need to use `unsafe` context.
            // Don't need to pin it using the `fixed` statement.
            Span<byte> buffer = bufferSize <= 128 ? stackalloc byte[bufferSize] : new byte[bufferSize];

            var outlist = new List<byte>();

            using (var reader = new MemoryStream(encoding.GetBytes(Text)))
            {
                int count;
                while ((count = reader.Read(buffer)) > 0)
                {
                    outlist.AddRange(buffer.Slice(0, count).ToArray());
                }
            }

            encoding.GetString(outlist.ToArray()).Is(Text);

            return;
        }

        private static readonly string Text = @"The C# language relies on types and methods
            in what the C# specification defines as a standard library for some of the features.
            The .NET platform delivers those types and methods in a number of packages.
            One example is exception processing. Every throw statement or expression
            is checked to ensure the object being thrown is derived from Exception. Similarly,
            every catch is checked to ensure that the type being caught is derived from Exception.
            Each version may add new requirements. To use the latest language features in older environments,
            you may need to install specific libraries. These dependencies are documented in the page for each specific version.
            You can learn more about the relationships between language and library for background on this dependency.";

        [Fact]
        public void WhenCreatingReadBuffer_WithFactory()
        {
            Span<byte> CreateBuffer(int size)
            {
                if (size > 1000) { throw new ArgumentOutOfRangeException(); }

                Span<byte> stack = stackalloc byte[size];
                // error CS8352 Cannot use variable 'stack' in this context
                //      because it may expose referenced variables outside of their declaration scope.
                // return stack;

                return new byte[size];
            };

            Span<byte> buffer = CreateBuffer(32);
            buffer.Length.Is(32);

            return;
        }

    }

}

