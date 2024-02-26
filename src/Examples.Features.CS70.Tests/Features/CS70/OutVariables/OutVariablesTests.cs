using System;
using ChainingAssertion;
using Xunit;
using Xunit.Sdk;

namespace Examples.Features.CS70.OutVariables
{
    /// <summary>
    /// Tests for Out variables in C# 7.0.
    /// </summary>
    public class OutVariablesTests
    {
        [Fact]
        public void BasicUsage()
        {
            var numOfText = "12345678";

            // C# 6.0
#pragma warning disable IDE0018 // Inline variable declaration
            int convertedWithCS60;
#pragma warning restore IDE0018
            if (!int.TryParse(numOfText, out convertedWithCS60))
            {
                throw new XunitException("fail");
            }

            // C# 7.0 or later
            if (!int.TryParse(numOfText, out int convertedCS70))
            {
                throw new XunitException("fail");
            }

            // This is in scope.
            convertedWithCS60.Is(12345678);
            convertedCS70.Is(12345678);

            return;
        }

        [Fact]
        public void WhenUsedToShorthandConverter()
        {
            int? ToIntOrDefault(string text)
                => int.TryParse(text, out var @value) ? @value : (int?)null;

            var actual1 = ToIntOrDefault("ABC");
            actual1.IsNull();

            DateTime? ToDateTimeOrDefault(string text, DateTime? defaultValue = null)
                => DateTime.TryParse(text, out var @date) ? @date : (defaultValue ?? DateTime.Now);

            var actual2 = ToDateTimeOrDefault("2024-02-29T12:34:56.789Z");
            actual2.Is(new DateTime(2024, 2, 29, 12, 34, 56, 789, DateTimeKind.Utc));

            return;
        }

    }
}
