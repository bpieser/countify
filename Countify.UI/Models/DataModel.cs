namespace Countify.UI.Models;

internal class DataModel(IWordReader wordReader) : IDataModel
{
    public IEnumerable<WordInfo> WordInfos { get; private set; } = [];


    public async Task ReadAsync(string filePath, CancellationToken cancellationToken)
    {
        Context.Logger.LogInformation("Daten werden gelesen.");
        try
        {
            WordInfos = await wordReader.ReadAsync(filePath, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            Context.Logger.LogInformation("Einlesen der Daten wurde abgebrochen.");
            WordInfos = [];
        }
        catch (Exception ex)
        {
            Context.Logger.LogCritical("Ein Fehler ist aufgetreten. {ErrorMessage}", ex.Message);
            WordInfos = [];
        }
    }

    public async Task SortByCountAsync(bool isAscending)
    {
        await Task.Run(() =>
        {
            WordInfos = isAscending
                ? WordInfos.OrderBy(x => x.Count)
                : WordInfos.OrderByDescending(x => x.Count);
        });
    }
}