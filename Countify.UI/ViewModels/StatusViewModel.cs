namespace Countify.UI.ViewModels;

public class StatusViewModel : ViewModelBase
{
    private string _statusInformation = "Bereit";
    public string StatusInformation
    {
        get => _statusInformation;
        set => SetField(ref _statusInformation, value);
    }

    public StatusViewModel()
    {
        Context.LogListener.LogEventReported += (_, e) =>
        {
            Report(e.Message);
        };
    }

    private void Report(string message) => StatusInformation = message;
}
