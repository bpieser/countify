using Countify.Core;
using Countify.UI.Models;

namespace Tester.Countify.UI.Tests;

public class DataModelTests
{
    private const string TestFilePath = @"..\..\..\TestData\SimpleSample.txt";

    [Fact]
    public async Task ReadAsync_ShouldWorkCorrectly()
    {
        // Arrange
        WordReader wordReader = new();
        DataModel dataModel = new(wordReader);

        // Act
        await dataModel.ReadAsync(TestFilePath, CancellationToken.None);

        //Assert
        Assert.Equal(7, dataModel.WordInfos.Count());
    }

    [Fact]
    public async Task SortAscendingAsync_ShouldWorkCorrectly()
    {
        // Arrange
        WordReader wordReader = new();
        DataModel dataModel = new(wordReader);

        // Act
        await dataModel.ReadAsync(TestFilePath, CancellationToken.None);
        await dataModel.SortByCountAsync(true);

        //Assert
        List<WordInfo> wordInfos = dataModel.WordInfos.ToList();
        Assert.Equal("1:1", wordInfos[0].Word);
        Assert.Equal(1UL, wordInfos[0].Count);
        Assert.Equal("Adam", wordInfos[5].Word);
        Assert.Equal(2UL, wordInfos[5].Count);
        Assert.Equal("Seth", wordInfos[6].Word);
        Assert.Equal(2UL, wordInfos[6].Count);
    }

    [Fact]
    public async Task SortDescendingAsync_ShouldWorkCorrectly()
    {
        // Arrange
        WordReader wordReader = new();
        DataModel dataModel = new(wordReader);

        // Act
        await dataModel.ReadAsync(TestFilePath, CancellationToken.None);
        await dataModel.SortByCountAsync(false);

        //Assert
        List<WordInfo> wordInfos = dataModel.WordInfos.ToList();
        
        Assert.Equal("Adam", wordInfos[0].Word);
        Assert.Equal(2UL, wordInfos[0].Count);
        Assert.Equal("Seth", wordInfos[1].Word);
        Assert.Equal(2UL, wordInfos[1].Count);
        Assert.Equal("Iared", wordInfos[6].Word);
        Assert.Equal(1UL, wordInfos[6].Count);
    }
}

