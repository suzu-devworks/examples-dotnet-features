using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Examples.Features.CSharp70.Tests.PatternMatching
{
    /// <summary>
    /// Tests for Declaration and type patterns of pattern matching in C# 7.0.
    /// </summary>
    /// <seealso href="https://learn.microsoft.com/ja-jp/dotnet/csharp/language-reference/operators/patterns" />
    public class DeclarationAndTypePatternsTests
    {
        [Fact]
        public void When_UsingIfExpressions_Then_AssignedResultToNewVariable()
        {
            var input = new[] { "Hello world" };
            Assert.Equal("Hello world", GetString(input));
            Assert.Equal("Hello world", GetString(new List<string> { "Hello world" }));
            Assert.Null(GetString("Hello world"));

            string GetString(object value)
            {
                // Declaration Pattern: `is <type> <variable>`
                if (value is IEnumerable<string> converted)
                {
                    return converted.FirstOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        [Fact]
        public void When_UsingSwitchStatements_Then_CanBeProcessingByTypes()
        {
            var input = new[] { 10, 20, 30 };
            Assert.Equal("array length 3.", GetMessage(input));
            Assert.Equal("[ abcd ].", GetMessage(new List<char> { 'a', 'b', 'c', 'd' }));
            Assert.Equal("default message.", GetMessage("abcde".Select(x => x)));

            string GetMessage<T>(IEnumerable<T> source)
            {
                switch (source)
                {
                    // Declaration Pattern: `case <type> <variable>:`
                    case Array array:
                        return $"array length {array.Length}.";

                    case ICollection<T> collection:
                        return $"[ {string.Join("", collection)} ].";

                    default:
                        return "default message.";
                }
            }
        }
    }
}
