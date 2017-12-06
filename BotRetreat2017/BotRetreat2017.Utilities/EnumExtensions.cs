using System;

namespace BotRetreat2017.Utilities
{
    public static class EnumExtensions
    {
        public static String GetName<T>(this T value)
        {
            return value.ToString();
        }

        public static String GetDescription<T>(this T value)
        {
            return String.Empty;
        }
    }
}