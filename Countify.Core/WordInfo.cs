using System.Diagnostics;

namespace Countify.Core;

/// <summary>The information about the word.</summary>
/// <param name="word">The word</param>
/// <param name="count">The word count.</param>
[DebuggerDisplay("({Word} | {Count})")]
public class WordInfo(string word, ulong count)
{
    public string Word { get; } = word;
    public ulong Count { get; set; } = count;
}