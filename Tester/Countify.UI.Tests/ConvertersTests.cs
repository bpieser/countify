using System.Windows;
using Countify.UI.Converters;

namespace Tester.Countify.UI.Tests;

public class ConvertersTests
{
    [Theory]
    [InlineData(1234UL, "1.234")]
    [InlineData(1234567UL, "1.234.567")]
    [InlineData(32UL, "32")]
    [InlineData(null, "invalid number")]
    public void ULongToStringNumberConverter_ShouldWorkCorrectly(ulong? input, string expected)
    {
        // Act
        object? value = Converters.ULongToStringNumberConverter.Convert(input, null, null, null);

        // Assert
        Assert.Equal(expected, ((string)value)!);
    }

    [Theory]
    [InlineData(false, Visibility.Hidden)]
    [InlineData(true, Visibility.Visible)]
    public void BooleanToVisibleConverter_ShouldWorkCorrectly(bool input, Visibility expected)
    {
        // Act
        object? value = Converters.BooleanToVisibleConverter.Convert(input, null, null, null);

        // Assert
        Assert.Equal(expected, ((Visibility)value)!);
    }
}

