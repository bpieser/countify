using Microsoft.Extensions.Logging.EventSource;
using System.Diagnostics.Tracing;

namespace Countify.UI.Logging;

internal class LogListener : EventListener, ILogListener
{
    private readonly List<LogEventArgs> _waitingLogEventArgs = [];

    private readonly List<EventHandler<LogEventArgs>> _logEventReportedHandlers = [];

    private const string EventSourceOfInterestName = "Microsoft-Extensions-Logging";

    public event EventHandler<LogEventArgs> LogEventReported
    {
        add
        {
            _logEventReportedHandlers.Add(value);

            foreach (LogEventArgs logEvent in _waitingLogEventArgs)
                value(this, logEvent);
        }

        remove => _logEventReportedHandlers.Remove(value);
    }

    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        base.OnEventSourceCreated(eventSource);
        if (eventSource.Name != EventSourceOfInterestName)
            return;

        EnableEvents(eventSource, EventLevel.LogAlways, LoggingEventSource.Keywords.FormattedMessage,
            new Dictionary<string, string?> { ["FilterSpecs"] = "*:Trace;" }
        );
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        base.OnEventWritten(eventData);

        if (eventData.EventSource.Name != EventSourceOfInterestName)
            return;

        string message = GetPayloadData(eventData, payloadName: "FormattedMessage", defaultValue: "Cannot get error description.");

        LogEventArgs logEventArgs = new(message);

        _waitingLogEventArgs.Add(logEventArgs);

        foreach (EventHandler<LogEventArgs> logEventReportedHandler in _logEventReportedHandlers)
            logEventReportedHandler(this, logEventArgs);
    }

    private static T GetPayloadData<T>(EventWrittenEventArgs eventData, string payloadName, T defaultValue)
    {
        if (eventData.PayloadNames is null || eventData.Payload is null)
            return defaultValue;

        int count = Math.Min(eventData.PayloadNames.Count, eventData.Payload.Count);
        for (int i = 0; i < count; i++)
        {
            if (eventData.PayloadNames[i] == payloadName && eventData.Payload[i] is T payLoad)
            {
                return payLoad;
            }
        }

        return defaultValue;
    }
}
