using Microsoft.Extensions.Logging;

namespace Examples.Features.CSharp100.Tests.Helpers;

/// <summary>
/// A test logger that implements ILogger<T> but only logs messages at or above the specified log level.
/// </summary>
/// <typeparam name="T"></typeparam>
public class TestLogger<T> : ILogger<T>
{
    public LogLevel EnabledLevel { get; init; } = LogLevel.Error;

    private readonly List<string> _logs = new();
    public IReadOnlyList<string> Logs => _logs;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(LogLevel logLevel) => logLevel >= EnabledLevel;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _logs.Add(formatter(state, exception));
    }
}
