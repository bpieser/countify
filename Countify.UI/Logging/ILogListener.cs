namespace Countify.UI.Logging;

public interface ILogListener : IDisposable
{
    event EventHandler<LogEventArgs> LogEventReported;
}
