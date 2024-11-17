using Countify.UI.Logging;

namespace Countify.UI;

public sealed class Context
{
    public static ILogger<Context> Logger { get; internal set; }

    public static readonly ILogListener LogListener;

    private static readonly ILoggerFactory? LoggerFactory;

    static Context()
    {
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder => builder
            .SetMinimumLevel(LogLevel.Trace)
            .AddEventSourceLogger());

        Logger = LoggerFactory.CreateLogger<Context>();
        LogListener = new LogListener();
    }

    public static void Dispose()
    {
        LoggerFactory?.Dispose();
    }
}

