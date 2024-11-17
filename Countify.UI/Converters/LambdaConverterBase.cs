using System.Globalization;
using System.Windows.Data;

namespace Countify.UI.Converters;

public class LambdaConverter<TInput, TOutput>(Func<TInput?, TOutput?> convert) : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            null => convert(default),
            TInput input => convert(input),
            _ => throw new ArgumentException($"Expected {typeof(TInput)} but got {value.GetType()}")
        };
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
}

public static class LambdaConverter
{
    public static LambdaConverter<TInput, TOutput> Create<TInput, TOutput>(Func<TInput?, TOutput?> convert) => new(convert);
}
