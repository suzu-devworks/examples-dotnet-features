using System;
using Xunit;

namespace Examples.Features.CSharp70.Tests.OutVariables
{
    /// <summary>
    /// Tests for Out variables in C# 7.0.
    /// </summary>
    public class OutVariablesTests
    {
        [Fact]
        public void When_DeclaredAsInlinedVariable_Then_CanBeUsedAfterDeclared()
        {
            var numOfText = "12345678";

            // older
            int convertedWithCs60;
            if (!int.TryParse(numOfText, out convertedWithCs60))
            {
                Assert.Fail();
            }
            Assert.Equal(12345678, convertedWithCs60);

            // C# 7.0 or later
            if (!int.TryParse(numOfText, out int convertedCs70))
            {
                Assert.Fail();
            }
            // This is in scope.
            Assert.Equal(12345678, convertedCs70);
        }

        [Fact]
        public void When_UsingInlineVariable_Then_UsedToShorthandConverter()
        {
            int? ToIntOrDefault(string text)
                => int.TryParse(text, out var @value) ? @value : (int?)null;

            var invalidValue = ToIntOrDefault("ABC");
            Assert.Null(invalidValue);

            DateTime? ToDateTimeOrDefault(string text, DateTime? defaultValue = null)
                => DateTime.TryParse(text, out var @date) ? @date : (defaultValue ?? DateTime.Now);

            var validDateTime = ToDateTimeOrDefault("2024-02-29T12:34:56.789Z");
            Assert.Equal(new DateTime(2024, 2, 29, 12, 34, 56, 789, DateTimeKind.Utc), validDateTime);
        }

    }
}
