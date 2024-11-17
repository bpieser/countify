using System.Collections.ObjectModel;
using System.IO;
using Countify.UI.Models;

namespace Countify.UI.ViewModels;

public class DocumentViewModel : ViewModelBase
{
    public DelegateCommand LoadFileCommand { get; }
    public DelegateCommand CancelFileReadingCommand { get; }
    public DelegateCommand SortCommand { get; }

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (SetField(ref _isLoading, value))
                CommandsRaiseCanExecute();
        }
    }

    private bool _isFileOpen;
    public bool IsDocumentOpen
    {
        get => _isFileOpen;
        set => SetField(ref _isFileOpen, value);
    }

    private bool _isWaitingForInput = true;
    public bool IsWaitingForInput
    {
        get => _isWaitingForInput;
        set => SetField(ref _isWaitingForInput, value);
    }

    private bool _isSortAscending = true;
    public bool IsSortAscending
    {
        get => _isSortAscending;
        set => SetField(ref _isSortAscending, value);
    }

    private ObservableCollection<WordInfo> _wordCountCollection = [];
    public ObservableCollection<WordInfo> WordCountCollection
    {
        get => _wordCountCollection;
        set => SetField(ref _wordCountCollection, value);
    }

    private CancellationTokenSource _cancellationTokenSource = new();
    private readonly IDataModel _dataModel;
    private readonly IFileHandling _fileHandling;

    public DocumentViewModel(IDataModel dataModel, IFileHandling fileHandling)
    {
        _dataModel = dataModel;
        _fileHandling = fileHandling;

        LoadFileCommand = new DelegateCommand(ReadFile, _ => !IsLoading);
        CancelFileReadingCommand = new DelegateCommand(CancelFileReading, _ => IsLoading);
        SortCommand = new DelegateCommand(Sort, _ => IsDocumentOpen);
    }

    private async void ReadFile()
    {
        (bool isFileSelected, string filePath) = _fileHandling.OpenFileDialog();
        if (!isFileSelected)
            return;

        WordCountCollection.Clear();
        _cancellationTokenSource = new CancellationTokenSource();

        IsLoading = true;
        await _dataModel.ReadAsync(filePath, _cancellationTokenSource.Token);
        WordCountCollection = new ObservableCollection<WordInfo>(_dataModel.WordInfos);
        IsLoading = false;

        if (IsDocumentOpen)
            Context.Logger.LogInformation("{fileName} | Anzahl Wörter: {count}", Path.GetFileName(filePath), WordCountCollection.Count);
    }

    private void CancelFileReading() => _cancellationTokenSource.Cancel();

    private async void Sort()
    {
        IsSortAscending = !IsSortAscending;
        await _dataModel.SortByCountAsync(IsSortAscending);
        WordCountCollection = new ObservableCollection<WordInfo>(_dataModel.WordInfos);
    }

    private void CommandsRaiseCanExecute()
    {
        IsDocumentOpen = WordCountCollection.Any() && !IsLoading;
        IsWaitingForInput = !WordCountCollection.Any() && !IsLoading;

        LoadFileCommand.OnCanExecuteChanged();
        CancelFileReadingCommand.OnCanExecuteChanged();
        SortCommand.OnCanExecuteChanged();
    }
}
