using System;
using System.Globalization;
using System.Windows.Data;
using BotRetreat2017.Contracts;

namespace BotRetreat2017.Wpf.Framework.Converters
{
    public class HealthToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var health = value as HealthDto;
            return health == null ? String.Empty : $"{health.Current} / {health.Maximum} (-{health.Drain})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Converting from String to Health is not supported!");
        }
    }
}