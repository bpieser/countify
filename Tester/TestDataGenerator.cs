using System.Text;

namespace Tester;

public class TestDataGenerator
{
    private bool _wasHere;

    public void Run()
    {
        if (_wasHere)
            return;

        WriteToFile("Generated_HugeDataFile_TenWords.txt", GenerateFileWithTenWords(1_220_000));
        WriteToFile("Generated_SmallDataFile_TenWords.txt", GenerateFileWithTenWords(50_000));

        WriteToFile("Generated_HugeDataFile_RandomWords.txt", GenerateRandomWords(2_500_000));
        WriteToFile("Generated_SmallDataFile_RandomWords.txt", GenerateRandomWords(50_000));

        _wasHere = true;
    }

    private static IEnumerable<string> GenerateFileWithTenWords(int totalLines)
    {
        string tenCities = "Berlin Leipzig Bremen Stralsund    Berlin Florenz Sevilla         Bordeaux Leipzig Bozen   Verona Bordeaux Nizza Berlin Bozen";
        return Enumerable.Repeat(tenCities, totalLines);
    }

    private static IEnumerable<string> GenerateRandomWords(int totalLines)
    {
        const string chars = "AntonPaarProveTec";

        Random random = new();

        for (int i = 0; i < totalLines; i++)
        {
            int numberWords = random.Next(3, 9);
            StringBuilder builder = new();
            for (int j = 0; j < numberWords; j++)
            {
                int length = random.Next(3, 9);
                var word = new StringBuilder(length);
                for (int k = 0; k < length; k++)
                {
                    word.Append(chars[random.Next(chars.Length)]);
                }

                if (j > 0)
                    builder.Append(' ');
                builder.Append(word);
            }

            yield return builder.ToString();
        }
    }

    private static void WriteToFile(string fileName, IEnumerable<string> lines)
    {
        string filePath = Path.Combine(@"..\..\..\..\Tester\TestData\", fileName);
        Console.WriteLine($"Write file {filePath}");
        using StreamWriter writer = new(filePath);
        foreach (string line in lines)
        {
            writer.WriteLine(line);
        }
    }
}
