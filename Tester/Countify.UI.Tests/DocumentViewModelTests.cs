using Countify.Core;
using Moq;
using System.Collections.ObjectModel;
using Countify.UI.Models;
using Countify.UI.ViewModels;

namespace Tester.Countify.UI.Tests;

public class DocumentViewModelTests
{
    private readonly Mock<IDataModel> _mockDataModel;
    private readonly Mock<IFileHandling> _mockFileHandling;

    private readonly DocumentViewModel _viewModel;

    public DocumentViewModelTests()
    {
        _mockDataModel = new Mock<IDataModel>();
        _mockFileHandling = new Mock<IFileHandling>();
        _viewModel = new DocumentViewModel(_mockDataModel.Object, _mockFileHandling.Object);
    }

    [Fact]
    public async Task LoadFileCommand_Executes_ReadsFile()
    {
        // Arrange
        const string testFilePath = @"Some\Path\To\Sample.txt";
        var testWords = new[] { new WordInfo("TestWord", 32) };

        _mockFileHandling.Setup(x => x.OpenFileDialog()).Returns(() => (true, testFilePath));
        _mockDataModel.Setup(x => x.ReadAsync(testFilePath, It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        _mockDataModel.SetupGet(x => x.WordInfos).Returns(testWords);

        // Act
        _viewModel.LoadFileCommand.Execute(null);

        // Assert
        await Task.Delay(500);
        Assert.Equal(testWords.First(), _viewModel.WordCountCollection.First());
        Assert.False(_viewModel.IsLoading);
        Assert.True(_viewModel.IsDocumentOpen);
        Assert.False(_viewModel.IsWaitingForInput);
    }


    [Fact]
    public async Task SortCommand_Executes_SortsCollection()
    {
        // Arrange
        ObservableCollection<WordInfo> testWords = new(new[]
        {
            new WordInfo("Berlin", 3),
            new WordInfo("Bremen", 1),
            new WordInfo("Bozen", 2)
        });

        _mockDataModel.SetupGet(m => m.WordInfos).Returns(testWords);
        _mockDataModel
            .Setup(m => m.SortByCountAsync(true))
            .Callback<bool>(x =>
            {
                var list = !x ? testWords.OrderBy(w => w.Count) : testWords.OrderByDescending(w => w.Count);
                testWords = new ObservableCollection<WordInfo>(list);
            })
            .Returns(Task.CompletedTask);

        _viewModel.WordCountCollection = new ObservableCollection<WordInfo>(testWords);

        // Act
        _viewModel.SortCommand.Execute(null);

        // Assert initial state
        Assert.False(_viewModel.IsSortAscending);

        // Await and verify
        await Task.Delay(100); // Simulate async delay
        Assert.Equal(testWords, _viewModel.WordCountCollection);
    }
}

