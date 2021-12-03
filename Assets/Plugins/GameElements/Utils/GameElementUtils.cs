using System;
using System.Collections.Generic;

namespace GameElements
{
    internal static class GameElementUtils
    {
        internal static T FindValue<T>(Dictionary<Type, object> map)
        {
            return (T) FindValue(map, typeof(T));
        }

        internal static object FindValue(Dictionary<Type, object> map, Type requiredType)
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

            throw new Exception($"Value of type {requiredType.Name} is not found!");
        }

        internal static bool TryFindValue<T>(Dictionary<Type, object> map, out T item)
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

            item = default;
            return false;
        }
    }
}