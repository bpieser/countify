namespace Countify.UI.Logging;

public class LogEventArgs(string message) : EventArgs
{
    public string Message { get; } = message;

    public override string ToString() => Message;
}