using System.Runtime.CompilerServices;
using Xunit;

namespace Examples.Features.CSharp50.Tests.CallerInfoAttributes
{
    /// <summary>
    /// Tests for Caller info attributes in C# 5.0.
    /// </summary>
    public class CallerInfoAttributesTests
    {
        [Fact]
        public void When_UsingCallerFilePath_Then_ReturnsThisFilePath()
        {
            Assert.EndsWith(@"CallerInfoAttributesTests.cs", GetFilePath());
        }

        private static string GetFilePath([CallerFilePath] string path = null)
        {
            return path;
        }

        [Fact]
        public void When_UsingCallerLineNumber_Then_ReturnsThisFilePath()
        {
            Assert.Equal(25, GetLineNumber());
        }

        private static int GetLineNumber([CallerLineNumber] int line = -1)
        {
            return line;
        }

        [Fact]
        public void When_UsingCallerMemberName_Then_ReturnsThisMethodName()
        {
            Assert.Equal("When_UsingCallerMemberName_Then_ReturnsThisMethodName", GetMemberName());
        }

        private static string GetMemberName([CallerMemberName] string name = null)
        {
            return name;
        }

    }
}
