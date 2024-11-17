using Countify.UI.Models;

namespace Countify.UI.ViewModels;

public class MainViewModel : ViewModelBase
{
    public StatusViewModel StatusViewModel { get; } = new();

    public DocumentViewModel DocumentViewModel { get; }

    public MainViewModel()
    {
        IWordReader wordReader = new WordReader();
        IDataModel dataModel = new DataModel(wordReader);
        IFileHandling fileHandling = new FileHandling();
        DocumentViewModel = new DocumentViewModel(dataModel, fileHandling);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            DocumentViewModel.Dispose();
            Context.Dispose();
        }
        base.Dispose(disposing);
    }
}