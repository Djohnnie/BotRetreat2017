using System;
using System.Collections.Generic;
using BotRetreat2017.Utilities;
using BotRetreat2017.Wpf.Framework.Services.Interfaces;

namespace BotRetreat2017.Wpf.Framework.Services
{
    public class CacheService : ICacheService
    {
        private readonly Dictionary<String, String> _objectCache = new Dictionary<String, String>();

        public void Store<T>(String key, T value)
        {
            if (!_objectCache.ContainsKey(key))
            {
                _objectCache.Add(key, string.Empty);
            }
            _objectCache[key] = value.Serialize();
        }

        public T Load<T>(String key)
        {
            if (_objectCache.ContainsKey(key))
            {
                return _objectCache[key].Deserialize<T>();
            }
            return default(T);
        }
    }
}