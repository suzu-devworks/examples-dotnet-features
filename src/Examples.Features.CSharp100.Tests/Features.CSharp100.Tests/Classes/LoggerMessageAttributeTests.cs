using Examples.Features.CSharp100.Tests.Helpers;
using Microsoft.Extensions.Logging;

namespace Examples.Features.CSharp100.Tests.Classes;

public partial class LoggerMessageAttributeTests
{
    private readonly ILogger<LoggerMessageAttributeTests> _logger
        = new TestLogger<LoggerMessageAttributeTests>() { EnabledLevel = LogLevel.Information };

    [LoggerMessage(Level = LogLevel.Information, Message = "Hello {Name}, the answer is {Answer}")]
    private partial void LogHello(string name, int answer);

    [Fact]
    public void When_UsingPrimitiveValues_Then_GetsFormattedMessage()
    {
        LogHello("World", 42);

        var testLogger = (TestLogger<LoggerMessageAttributeTests>)_logger;
        Assert.Single(testLogger.Logs);
        Assert.Equal("Hello World, the answer is 42", testLogger.Logs[0]);
    }

    private record struct Point(int X, int Y);

    [LoggerMessage(Level = LogLevel.Information, Message = "Point is at {Point}")]
    private partial void LogPoint(Point point);

    [Fact]
    public void When_UsingCustomType_Then_GetsFormattedMessage()
    {
        LogPoint(new Point(1, 2));

        var testLogger = (TestLogger<LoggerMessageAttributeTests>)_logger;
        Assert.Single(testLogger.Logs);
        Assert.Equal("Point is at Point { X = 1, Y = 2 }", testLogger.Logs[0]);
    }

    private record struct PointWithCustomToString(int X, int Y)
    {
        public override string ToString() => $"({X}, {Y})";
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Point is at {Point}")]
    private static partial void LogPointWithCustomToString(ILogger logger, PointWithCustomToString point);

    [Fact]
    public void When_UsingCustomToString_Then_GetsFormattedMessage()
    {
#pragma warning disable CA1873 // Avoid potentially expensive logging
        LogPointWithCustomToString(_logger, new PointWithCustomToString(1, 2));
#pragma warning restore CA1873 // Avoid potentially expensive logging

        var testLogger = (TestLogger<LoggerMessageAttributeTests>)_logger;
        Assert.Single(testLogger.Logs);
        Assert.Equal("Point is at (1, 2)", testLogger.Logs[0]);
    }

    [Fact]
    public void When_DisabledLogLevel_Then_DoesNotFormatMessage()
    {
        var logger = new TestLogger<LoggerMessageAttributeTests>() { EnabledLevel = LogLevel.Error };

#pragma warning disable CA1873 // Avoid potentially expensive logging
        LogPointWithCustomToString(_logger, new PointWithCustomToString(3, 4));
#pragma warning restore CA1873 // Avoid potentially expensive logging

        Assert.Empty(logger.Logs);
    }
}
