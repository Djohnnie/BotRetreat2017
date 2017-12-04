using System;
using System.Globalization;
using System.Windows.Data;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Wpf.Framework.Converters
{
    public class PositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = value as PositionDto;
            return position == null ? String.Empty : $"{position.X} x {position.Y}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from String to Position is not supported!");
        }
    }
}