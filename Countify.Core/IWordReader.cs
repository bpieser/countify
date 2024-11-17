namespace Countify.Core;

public interface IWordReader
{
    Task<IEnumerable<WordInfo>> ReadAsync(string filePath, CancellationToken cancellationToken);
}

