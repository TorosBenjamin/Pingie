using System.Globalization;

namespace Pingie.Maui.Utils.Converters;

public class ResumePauseTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool resume)
        {
            return resume ? "Resume" : "Pause";
        }
        return "Resume";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}