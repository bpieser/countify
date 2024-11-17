namespace Countify.Core;

public class WordReader : IWordReader
{
    public async Task<IEnumerable<WordInfo>> ReadAsync(string filePath, CancellationToken cancellationToken)
    {
        Dictionary<string, ulong> wordCounter = new(StringComparer.Ordinal);

        using (var reader = new StreamReader(filePath))
        {
            while (await reader.ReadLineAsync(cancellationToken) is { } line)
            {
                var words = ProcessLine(line);
                foreach (var word in words)
                {
                    if (!wordCounter.TryAdd(word, 1))
                        wordCounter[word]++;
                }
            }
        }

        return wordCounter.Select(x => new WordInfo(x.Key, x.Value));
    }

    private static IEnumerable<string> ProcessLine(string line)
    {
        var split = line.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
        foreach(var word in split)
            yield return word;
    }

}
