using System.Globalization;
using System.Runtime.CompilerServices;
using Examples.Features.CSharp100.Tests.InterpolatedStringHandlers.Fixtures;
using Microsoft.Extensions.Logging;

namespace Examples.Features.CSharp100.Tests.InterpolatedStringHandlers;

public class DefaultInterpolatedStringHandlerTests
{
    [Fact]
    public void When_UsedLikeStringBuilder_Then_GetsFormattedString()
    {
        var world = "World";
        var answer = 42;

        var handler = new DefaultInterpolatedStringHandler(10, 2);
        handler.AppendLiteral("Hello ");
        handler.AppendFormatted(world);
        handler.AppendLiteral("! The answer is ");
        handler.AppendFormatted(answer);

        var result = handler.ToStringAndClear();
        Assert.Equal("Hello World! The answer is 42", result);
    }

    [Theory]
    [InlineData("en-US", "Hello at Saturday, June 1, 2024 value is 1,234.560")]
    [InlineData("de-DE", "Hello at Samstag, 1. Juni 2024 value is 1.234,560")]
    [InlineData("ja-JP", "Hello at 2024年6月1日土曜日 value is 1,234.560")]
    public void When_UsedCultureSpecificFormatting_Then_GetsFormattedString(string culture, string expectedMessage)
    {
        CultureInfo provider = new(culture);
        provider.NumberFormat.NumberDecimalDigits = 3; // Ensure that the number is formatted with three decimal places
        DateTimeOffset timestamp = new(2024, 6, 1, 0, 0, 0, TimeSpan.Zero);
        double value = 1234.56;

        var handler = new DefaultInterpolatedStringHandler(10, 2, provider);
        handler.AppendLiteral("Hello at ");
        handler.AppendFormatted(timestamp, "D"); // The date will be formatted according to the specified culture.
        handler.AppendLiteral(" value is ");
        handler.AppendFormatted(value, "N"); // The value will be formatted according to the specified culture.

        var result = handler.ToStringAndClear();
        Assert.Equal(expectedMessage, result);
    }

    [InterpolatedStringHandler]
    public ref struct LogInterpolatedStringHandler
    {
        // Storage for the built-up string
        private readonly DefaultInterpolatedStringHandler _innerHandler;

        public LogInterpolatedStringHandler(int literalLength, int formattedCount, ILogger logger, LogLevel logLevel,
            out bool shouldAppend)
        {
            if (!logger.IsEnabled(logLevel))
            {
                // If shouldAppend is false, later AppendFormatted etc. will not be called.
                shouldAppend = false;
                _innerHandler = default;
                return;
            }

            shouldAppend = true;
            _innerHandler = new DefaultInterpolatedStringHandler(literalLength, formattedCount);
        }

        public void AppendLiteral(string value) => _innerHandler.AppendLiteral(value);
        public void AppendFormatted<T>(T value) => _innerHandler.AppendFormatted(value);
        public override string ToString() => _innerHandler.ToStringAndClear();
    }

    [Fact]
    public void When_LogLevelDisabledWithThisHandler_Then_ArgumentAreIsNotExecuted()
    {
        // It doesn't seem to work with LoggerMessageAttribute.
        static void LogMessage(
            ILogger logger,
            LogLevel logLevel,
            [InterpolatedStringHandlerArgument("logger", "logLevel")] LogInterpolatedStringHandler builder)
        {
#pragma warning disable CA1873 // Avoid potentially expensive logging/
#pragma warning disable CA2254 // Template should be a static expression.
            logger.Log(logLevel, builder.ToString());
#pragma warning restore CA2254 // Template should be a static expression.
#pragma warning restore CA1873 // Avoid potentially expensive logging.
        }

        var logger = new TestLogger<DefaultInterpolatedStringHandlerTests>() { EnabledLevel = LogLevel.Warning };
        var wasMethodCalled = false;

        string GetValue()
        {
            wasMethodCalled = true;
            return "Heavy Data";
        }

        LogMessage(logger, LogLevel.Trace, $"Trace Level. This won't be printed. The method will still be called: {GetValue()}");

        Assert.False(wasMethodCalled);
    }

}
