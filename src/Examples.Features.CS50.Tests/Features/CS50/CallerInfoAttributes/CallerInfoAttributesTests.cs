using System.Runtime.CompilerServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Examples.Features.CS50.CallerInfoAttributes
{
    /// <summary>
    /// Tests for Caller info attributes in C# 5.0.
    /// </summary>
    public class CallerInfoAttributesTests
    {
        [Fact]
        public void BasicUsage()
        {
            using (var scope = new AssertionScope())
            {
                GetFilePath().Should().EndWith(@"CallerInfoAttributesTests.cs");
                GetLineNumber().Should().Be(19);
                GetMemberName().Should().Be("BasicUsage");
            }

            return;
        }

        private static string GetFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        private static int GetLineNumber([CallerLineNumber] int line = -1)
        {
            return line;
        }

        private static string GetMemberName([CallerMemberName] string name = null)
        {
            return name;
        }

    }
}
