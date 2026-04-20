
using System.Globalization;

namespace ProyectoFinal_BibliotecaPersonal.Converters
{
    public class BoolToReadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return boolValue ? "Leído" : "No leído";
            }
            return "No leído";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return stringValue == "Leído";
            }
            return false;
        }
    }
}