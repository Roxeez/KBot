using System.Collections;
using System.Collections.Generic;

namespace KBot.Extension
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.TryGetValue(key, out TValue value) ? value : default;
        }
    }
}