using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static V GetOrFallback<K, V>(this IDictionary<K, V> dictionary, K key, V fallbackValue)
    {
        V value;
        if (!dictionary.TryGetValue(key, out value))
        {
            value = fallbackValue;
        }
        return value;
    }

    public static V GetOrAddNew<K, V>(this IDictionary<K, V> dictionary, K key) where V : new()
    {
        V value;
        if (!dictionary.TryGetValue(key, out value))
        {
            value = new V();
            dictionary.Add(key, value);
        }
        return value;
    }
}
