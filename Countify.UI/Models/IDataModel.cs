namespace Countify.UI.Models;

public interface IDataModel
{
    IEnumerable<WordInfo> WordInfos { get; }
    Task ReadAsync(string filePath, CancellationToken cancellationToken);
    Task SortByCountAsync(bool isAscending);
}
