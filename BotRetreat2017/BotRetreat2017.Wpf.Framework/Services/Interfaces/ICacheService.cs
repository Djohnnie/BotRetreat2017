using System;

namespace BotRetreat2017.Wpf.Framework.Services.Interfaces
{
    public interface ICacheService
    {
        void Store<T>(String key, T value);

        T Load<T>(String key);
    }
}