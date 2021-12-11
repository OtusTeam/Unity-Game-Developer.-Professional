#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace GameElements
{
    internal static class MonoValidator
    {
        internal static MonoBehaviour[] ValidateServices(MonoBehaviour[] array)
        {
            var result = new List<MonoBehaviour>();
            
            for (int i = 0, count = array.Length; i < count; i++)
            {
                var monoBehaviour = array[i];
                if (monoBehaviour != null)
                {
                    result.Add(monoBehaviour);
                }
            }

            return result.ToArray();
        }

        internal static MonoBehaviour[] ValidateGameElements(MonoBehaviour[] array)
        {
            var result = new List<MonoBehaviour>();
            
            for (int i = 0, count = array.Length; i < count; i++)
            {
                var monoBehaviour = array[i];
                if (monoBehaviour == null)
                {
                    continue;
                }

                var gameElements = monoBehaviour.GetComponents<IGameElement>();
                foreach (var element in gameElements)
                {
                    result.Add((MonoBehaviour) element);
                }
            }

            return result.ToArray();
        }
    }
}
#endif