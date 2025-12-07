using System.Globalization;

namespace Pingie.Maui.Utils.Converters;

public class SelectedBorderColorConverter : IValueConverter
{
    public Color SelectedBorderColor {get; set;}
    public Color UnselectedBorderColor {get; set;}
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool isSelected = (bool)value;
        return isSelected ? SelectedBorderColor : UnselectedBorderColor;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}