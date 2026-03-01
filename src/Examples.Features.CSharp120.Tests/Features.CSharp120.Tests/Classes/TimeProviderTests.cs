using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace Examples.Features.CSharp120.Tests.Classes;

/// <summary>
/// Tests for using TimeProvider and FakeTimeProvider
/// Abstraction of time management available in .NET 8 and later
/// </summary>
public class TimeProviderTests
{
    /// <summary>
    /// When retrieving the current time using default TimeProvider,
    /// Should return valid UTC and local time values.
    /// </summary>
    [Fact]
    public void When_RetrievingCurrentTime_Then_ReturnsValidUtcAndLocalTime()
    {
        // Get the default TimeProvider
        var timeProvider = TimeProvider.System;

        // GetUtcNow() returns the current UTC time
        var utcNow = timeProvider.GetUtcNow();
        Assert.NotEqual(default, utcNow);
        Assert.Equal(DateTimeKind.Utc, utcNow.UtcDateTime.Kind);
        Assert.True(Math.Abs((utcNow - DateTimeOffset.UtcNow).TotalSeconds) < 1,
            "UTC time should be close to system time");

        // GetLocalNow() returns the localized current time
        var localNow = timeProvider.GetLocalNow();
        Assert.NotEqual(default, localNow);
        Assert.Equal(DateTimeKind.Local, localNow.LocalDateTime.Kind);
        Assert.True(Math.Abs((localNow - DateTimeOffset.Now).TotalSeconds) < 1,
            "Local time should be close to system time");
    }

    /// <summary>
    /// When setting and fixing the time in FakeTimeProvider,
    /// Should be able to control the time during tests.
    /// </summary>
    [Fact]
    public void When_SettingFixedTime_Then_TimeIsFixed()
    {
        // Create FakeTimeProvider with initial time
        var fakeTime = new FakeTimeProvider();

        var initialUtcNow = fakeTime.GetUtcNow();
        // Default initial time of FakeTimeProvider is 2000-01-01
        Assert.Equal(new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), initialUtcNow);

        // Set a specific time
        var fixedTime = new DateTimeOffset(2024, 3, 1, 10, 30, 0, TimeSpan.Zero);
        fakeTime.SetUtcNow(fixedTime);

        var afterSet = fakeTime.GetUtcNow();
        Assert.Equal(fixedTime, afterSet);
    }

    /// <summary>
    /// When advancing time in FakeTimeProvider,
    /// Should simulate the passage of time accurately.
    /// </summary>
    [Fact]
    public void When_AdvancingTime_Then_TimeProgressesCorrectly()
    {
        var fakeTime = new FakeTimeProvider(new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.Zero));
        var startTime = fakeTime.GetUtcNow();

        // Advance by 1 hour
        fakeTime.Advance(TimeSpan.FromHours(1));
        var afterOneHour = fakeTime.GetUtcNow();

        Assert.Equal(TimeSpan.FromHours(1), afterOneHour - startTime);

        // Advance by an additional 30 minutes
        fakeTime.Advance(TimeSpan.FromMinutes(30));
        var afterOneAndHalfHours = fakeTime.GetUtcNow();

        Assert.Equal(TimeSpan.FromHours(1.5), afterOneAndHalfHours - startTime);
    }

    /// <summary>
    /// When injecting TimeProvider into a service,
    /// Should measure elapsed time correctly.
    /// </summary>
    [Fact]
    public void When_MeasuringElapsedTime_Then_ElapsedTimeIsAccurate()
    {
        var fakeTime = new FakeTimeProvider(new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.Zero));
        var service = new TimeSensitiveService(fakeTime);

        // Verify initial state
        var initialElapsed = service.GetElapsedTime();
        Assert.Equal(TimeSpan.Zero, initialElapsed);

        // Advance by 2 hours
        fakeTime.Advance(TimeSpan.FromHours(2));
        var afterAdvance = service.GetElapsedTime();
        Assert.Equal(TimeSpan.FromHours(2), afterAdvance);
    }

    public class TimeSensitiveService
    {
        private readonly TimeProvider _timeProvider;
        private readonly long _initialTimestamp;

        public TimeSensitiveService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _initialTimestamp = timeProvider.GetTimestamp();
        }

        public TimeSpan GetElapsedTime()
        {
            return _timeProvider.GetElapsedTime(_initialTimestamp);
        }
    }

    /// <summary>
    /// When simulating multiple timed operations,
    /// Should verify time-dependent logic like retries.
    /// </summary>
    [Fact]
    public void When_SimulatingRetries_Then_TimeDifferencesAreCorrect()
    {
        var fakeTime = new FakeTimeProvider(new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.Zero));
        var service = new RetryService(fakeTime);

        // First attempt
        var attempt1 = fakeTime.GetUtcNow();
        service.ExecuteWithRetry();

        // Wait 3 seconds and retry
        fakeTime.Advance(TimeSpan.FromSeconds(3));
        var attempt2 = fakeTime.GetUtcNow();
        service.ExecuteWithRetry();

        // Wait another 3 seconds
        fakeTime.Advance(TimeSpan.FromSeconds(3));
        var attempt3 = fakeTime.GetUtcNow();

        // Verify time differences between attempts
        Assert.Equal(TimeSpan.FromSeconds(3), attempt2 - attempt1);
        Assert.Equal(TimeSpan.FromSeconds(3), attempt3 - attempt2);
    }

    public class RetryService
    {
        private readonly TimeProvider _timeProvider;
        private List<DateTimeOffset>? _retryHistory;

        public RetryService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public void ExecuteWithRetry()
        {
            _retryHistory ??= [];
            _retryHistory.Add(_timeProvider.GetUtcNow());
        }
    }

    /// <summary>
    /// When using local time with different timezones,
    /// Should handle timezone offset correctly.
    /// </summary>
    [Fact]
    public void When_UsingTimeZone_Then_TimeZoneOffsetIsHandled()
    {
        var fakeTime = new FakeTimeProvider(
            new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.FromHours(9)) // UTC+9
        );

        var localTime = fakeTime.GetLocalNow();
        Assert.Equal(9, localTime.Offset.Hours);
    }

    /// <summary>
    /// When using a custom TimeProvider with a fixed timezone,
    /// Should return local time with the correct timezone offset.
    /// </summary>
    [Fact]
    public void When_UsingCustomProvider_Then_FixedTimeZoneHandled()
    {
        var jstProvider = new JstTimeProvider();
        var localTime = jstProvider.GetLocalNow();
        Assert.Equal(TimeSpan.FromHours(9), localTime.Offset);
    }

    public class JstTimeProvider : TimeProvider
    {
        private static readonly TimeZoneInfo JstZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Tokyo");
        public override TimeZoneInfo LocalTimeZone => JstZone;
    }

    /// <summary>
    /// When retrieving time multiple times with advancing,
    /// Should verify progressive time advancement.
    /// </summary>
    [Fact]
    public void When_RetrievingTimeMultipleTimes_Then_TimeProgressesCorrectly()
    {
        var fakeTime = new FakeTimeProvider(new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.Zero));
        var startTime = fakeTime.GetUtcNow();

        var times = new List<DateTimeOffset>();
        for (int i = 0; i < 3; i++)
        {
            times.Add(fakeTime.GetUtcNow());
            fakeTime.Advance(TimeSpan.FromSeconds(10));
        }

        // Verify first retrieval is at start time
        Assert.Equal(startTime, times[0]);

        // Verify time progresses correctly
        Assert.Equal(TimeSpan.FromSeconds(10), times[1] - times[0]);
        Assert.Equal(TimeSpan.FromSeconds(20), times[2] - times[0]);
    }

    /// <summary>
    /// When using dependency injection with TimeProvider,
    /// Should support both production and test implementations.
    /// </summary>
    [Fact]
    public void When_UsingDependencyInjection_Then_ServiceReturnsCorrectTime()
    {
        var services = new ServiceCollection();
        services.AddSingleton<TimeProvider>(
            new FakeTimeProvider(
                    new DateTimeOffset(2024, 6, 15, 15, 30, 0, TimeSpan.Zero)));
        services.AddSingleton<ClockService>();
        using var provider = services.BuildServiceProvider();

        var service = provider.GetRequiredService<ClockService>();

        var currentTime = service.GetCurrentTime();
        Assert.Equal(new DateTimeOffset(2024, 6, 15, 15, 30, 0, TimeSpan.Zero), currentTime);
    }

    public class ClockService
    {
        private readonly TimeProvider _timeProvider;

        public ClockService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public DateTimeOffset GetCurrentTime()
        {
            return _timeProvider.GetUtcNow();
        }
    }

    [Fact]
    public void When_UsingTimerByTimeProvider_Then_TimerBehavesCorrectly()
    {
        var fakeTime = new FakeTimeProvider(new DateTimeOffset(2024, 3, 1, 10, 0, 0, TimeSpan.Zero));
        using var service = new HeartbeatService(fakeTime);

        // Start the timer
        Assert.Equal(0, service.BeatCount);

        // Advance time by 59 seconds - should not trigger the timer yet
        fakeTime.Advance(TimeSpan.FromSeconds(59));
        Assert.Equal(0, service.BeatCount);

        // Advance time by 1 second - should trigger the timer
        fakeTime.Advance(TimeSpan.FromSeconds(1));
        Assert.Equal(1, service.BeatCount);

        // Advance time by 10 minutes - should trigger the timer 10 more times
        fakeTime.Advance(TimeSpan.FromMinutes(10));
        Assert.Equal(11, service.BeatCount);
    }

    public class HeartbeatService : IDisposable
    {
        private readonly ITimer _timer;
        public int BeatCount { get; private set; }

        public HeartbeatService(TimeProvider timeProvider)
        {
            _timer = timeProvider.CreateTimer(
                callback: _ => BeatCount++,
                state: null,
                dueTime: TimeSpan.FromMinutes(1),
                period: TimeSpan.FromMinutes(1));
        }

        public void Dispose()
        {
            _timer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
