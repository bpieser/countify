using Countify.Core;

namespace Tester.Countify.Core.Tests;

public class WordReaderTests : IClassFixture<TestDataGenerator>
{
    private const string DummyTestFilePath = "DummyTest.txt";

    public WordReaderTests(TestDataGenerator testDataGenerator)
    {
        testDataGenerator.Run();
    }

    [Theory]
    [InlineData("SimpleSample.txt")]
    [InlineData("SmallSample.txt")]
    [InlineData("FullSample.txt")]
    [InlineData("CaseSensitiveWords.txt")]
    [InlineData("Generated_SmallDataFile_TenWords.txt")]
    [InlineData("Generated_HugeDataFile_TenWords.txt")]
    public async Task ReadAsync_ShouldCountWordsCorrectly(string testFileName)
    {
        // Arrange
        string testFilePath = Path.Combine(@"..\..\..\TestData\", testFileName);
        string referenceFilePath = Path.Combine(@"..\..\..\TestData\ReferenceData", $"Reference_{testFileName}");
        string outputFilePath = Path.Combine(@"..\..\..\TestData\ReferenceData", $"Output_{testFileName}");

        var wordReader = new WordReader();

        // Act
        var result = await wordReader.ReadAsync(testFilePath, CancellationToken.None);
        var wordInfos = result.ToList();
        File.WriteAllLines(outputFilePath, wordInfos.Select(x => $"{x.Word} {x.Count}"));

        List<string> lines = [];
        lines.AddRange(File.ReadAllLines(referenceFilePath));
        List<(string word, ulong count)> referenceList = [];
        foreach (string line in lines)
        {
            string[] split = line.Split(' ');
            referenceList.Add(new ValueTuple<string, ulong>(split[0], ulong.Parse(split[1])));
        }

        // Assert
        Assert.Equal(referenceList.Count, wordInfos.Count);
        for (int i = 0; i < referenceList.Count; i++)
        {
            Assert.Equal(referenceList[i].word, wordInfos[i].Word);
            Assert.Equal(referenceList[i].count, wordInfos[i].Count);
        }
    }

    [Fact]
    public async Task ReadAsync_ShouldHandleEmptyFile()
    {
        // Arrange
        await File.WriteAllTextAsync(DummyTestFilePath, string.Empty);

        var wordReader = new WordReader();

        // Act
        var result = await wordReader.ReadAsync(DummyTestFilePath, CancellationToken.None);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task ReadAsync_ShouldRespectCancellation()
    {
        // Arrange
        var cts = new CancellationTokenSource();
        cts.Cancel();

        string fileContent = "This will not be read";
        await File.WriteAllTextAsync(DummyTestFilePath, fileContent);

        var wordReader = new WordReader();

        // Act & Assert
        await Assert.ThrowsAsync<TaskCanceledException>(() => wordReader.ReadAsync(DummyTestFilePath, cts.Token));
    }

    [Fact]
    public async Task ReadAsync_ShouldThrowIfFileDoesNotExist()
    {
        // Arrange
        const string nonExistentFilePath = "nonexistent.txt";
        var wordReader = new WordReader();

        // Act & Assert
        await Assert.ThrowsAsync<FileNotFoundException>(() => wordReader.ReadAsync(nonExistentFilePath, CancellationToken.None));
    }
}
