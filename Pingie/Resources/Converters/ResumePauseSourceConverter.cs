using System.Globalization;

namespace Pingie.Maui.Utils.Converters;

public class ResumePauseSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool resume)
        {
            return resume ? "resume.svg" : "pause.svg";
        }
        return "resume.svg";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}