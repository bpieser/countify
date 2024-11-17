using System.IO;

namespace Countify.UI.Models;

public sealed class FileHandling : IFileHandling
{
    public (bool isFileSelected, string filePath) OpenFileDialog()
    {
        Microsoft.Win32.OpenFileDialog fileDialog = new()
        {
            CheckFileExists = true,
            Filter = "Text file|*.txt",
            Multiselect = false
        };
        if (fileDialog.ShowDialog() == true && File.Exists(fileDialog.FileName))
        {
            Context.Logger.LogInformation("Selected file: {fileName}", fileDialog.FileName);
            return (true, fileDialog.FileName);
        }

        Context.Logger.LogInformation("No file selected.");

        return (false, "");
    }
}
