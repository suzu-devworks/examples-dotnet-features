using System.Runtime.CompilerServices;
using System.Text;
using Examples.Features.CSharp100.Tests.InterpolatedStringHandlers.Fixtures;
using Microsoft.Extensions.Logging;

namespace Examples.Features.CSharp100.Tests.InterpolatedStringHandlers;

public partial class CustomStringInterpolationHandlerTests
{
    private static ITestOutputHelper? Output => TestContext.Current.TestOutputHelper;

    [InterpolatedStringHandler]
    public ref struct LogInterpolatedStringHandler
    {
        // Storage for the built-up string
        private readonly StringBuilder _builder;

        public LogInterpolatedStringHandler(int literalLength, int formattedCount, Logger logger, LogLevel logLevel,
            out bool shouldAppend)
        {
            // If shouldAppend is false, later AppendFormatted etc. will not be called.
            shouldAppend = logger.EnabledLevel <= logLevel;

            _builder = new StringBuilder(literalLength);
            Output?.WriteLine($"\tliteral length: {literalLength}, formattedCount: {formattedCount}");
        }

        public void AppendLiteral(string s)
        {
            Output?.WriteLine($"\tAppendLiteral called: {{{s}}}");

            _builder.Append(s);
            Output?.WriteLine($"\tAppended the literal string");
        }

        public void AppendFormatted<T>(T t)
        {
            Output?.WriteLine($"\tAppendFormatted called: {{{t}}} is of type {typeof(T)}");

            _builder.Append(t?.ToString());
            Output?.WriteLine($"\tAppended the formatted object");
        }

        public override string ToString() => _builder.ToString() ?? "";
    }

    public class Logger
    {
        public LogLevel EnabledLevel { get; init; } = LogLevel.Error;

        public void LogMessage(LogLevel level, string msg)
        {
            if (EnabledLevel < level) return;
            Output?.WriteLine(msg);
        }

        // InterpolatedStringHandlerArgument "" is this object, "level" is the log level argument passed to the method. This allows the handler to make decisions based on the log level.
        public void LogMessage(LogLevel level, [InterpolatedStringHandlerArgument("", "level")] LogInterpolatedStringHandler builder)
        {
            if (EnabledLevel < level) return;
            Output?.WriteLine(builder.ToString());
        }
    }

    [Fact]
    public void When_ImplementingHandlerPatternAsLearned_Then_CannotAssertButWorksAsExpected()
    {
        var logger = new Logger() { EnabledLevel = LogLevel.Warning };
        var time = DateTime.Now;

        logger.LogMessage(LogLevel.Error, $"Error Level. CurrentTime: {time}. This is an error. It will be printed.");
        logger.LogMessage(LogLevel.Trace, $"Trace Level. CurrentTime: {time}. This won't be printed.");
        logger.LogMessage(LogLevel.Warning, "Warning Level. This warning is a string, not an interpolated string expression.");
    }

    [Fact]
    public void When_LogLevelDisabledWithThisHandler_Then_ArgumentAreIsNotExecuted()
    {
        var logger = new Logger() { EnabledLevel = LogLevel.Warning };
        bool wasMethodCalled = false;

        string GetValue()
        {
            wasMethodCalled = true;
            return "Heavy Data";
        }

        logger.LogMessage(LogLevel.Trace, $"Trace Level. This won't be printed. The method will still be called: {GetValue()}");

        Assert.False(wasMethodCalled);
    }

    [Fact]
    public void When_LogLevelDisabledWithThisILogger_Then_ArgumentAreIsStillExecuted()
    {
        var logger = new TestLogger<CustomStringInterpolationHandlerTests>() { EnabledLevel = LogLevel.Warning };
#pragma warning disable IDE0059 // Remove unnecessary value assignment.
        bool wasMethodCalled = false;
#pragma warning restore IDE0059 // Remove unnecessary value assignment.

        string GetValue()
        {
            wasMethodCalled = true;
            return "Heavy Data";
        }

#pragma warning disable CA1873 // Avoid potentially expensive logging
        LogWithSourceGenerator(logger, GetValue());
#pragma warning restore CA1873 // Avoid potentially expensive logging

        // The method will be called because the check is outside of the LogWithSourceGenerator method,
        // which means that the interpolated string handler won't be used to prevent the execution of the method.
        Assert.True(wasMethodCalled);
    }

    [LoggerMessage(Level = LogLevel.Trace, Message = "This is a log message with a parameter: {Value}")]
    private static partial void LogWithSourceGenerator(ILogger logger, string value);
}
