using System;
using System.Collections.Generic;

namespace GameElements
{
    public static class GameElementUtils
    {
        internal static T Find<T>(Dictionary<Type, object> map)
        {
            return (T) Find(map, typeof(T));
        }

        internal static object Find(Dictionary<Type, object> map, Type requiredType)
        {
            if (map.ContainsKey(requiredType))
            {
                return map[requiredType];
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    return map[key];
                }
            }

            throw new Exception("Value is not found!");
        }

        internal static bool TryFind<T>(Dictionary<Type, object> map, out T item)
        {
            var requiredType = typeof(T);
            if (map.ContainsKey(requiredType))
            {
                item = (T) map[requiredType];
                return true;
            }

            var keys = map.Keys;
            foreach (var key in keys)
            {
                if (requiredType.IsAssignableFrom(key))
                {
                    item = (T) map[key];
                    return true;
                }
            }

            item = default(T);
            return false;
        }
    }
}