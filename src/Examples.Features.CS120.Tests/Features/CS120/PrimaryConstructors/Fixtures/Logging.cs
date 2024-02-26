namespace Examples.Features.CS120.PrimaryConstructors.Fixtures;

public interface ILogger
{
    void LogInformation(string format, params string[] parameters);
}

public class LoggingController(ILogger logger)
{
    private readonly ILogger _logger = logger;

    public void LegalAction()
    {
        _logger.LogInformation("logging in {name}", this.GetType().Name);
    }

    public void IllegalAction()
    {
        // By enabling this method, a CS9124 warning will be displayed in the readonly field.
        // This is an effective method of not changing parameters.
        // logger = default!;

        throw new ApplicationException("CS9124");
    }
}
