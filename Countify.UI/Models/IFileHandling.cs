namespace Countify.UI.Models;

public interface IFileHandling
{
    (bool isFileSelected, string filePath) OpenFileDialog();
}

