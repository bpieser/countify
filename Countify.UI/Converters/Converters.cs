using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

namespace Countify.UI.Converters;

public static class Converters
{
    public static IValueConverter BooleanToVisibleConverter { get; } =
        LambdaConverter.Create((object? value) => value is true ? Visibility.Visible : Visibility.Hidden);

    public static IValueConverter ULongToStringNumberConverter { get; } =
        LambdaConverter.Create((object? value) =>
            value is ulong number ? number.ToString("N0", new CultureInfo("de-DE")) : "invalid number");
}