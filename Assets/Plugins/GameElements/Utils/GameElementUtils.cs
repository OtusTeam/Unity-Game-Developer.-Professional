using System.Collections.Generic;

namespace GameElements
{
    public static class GameElementUtils
    {
        public static IEnumerable<IGameElement> CollectElements(IGameElement element)
        {
            var elementSet = new HashSet<IGameElement>();
            CollectElements(element, ref elementSet);
            return elementSet;
        }

        private static void CollectElements(IGameElement element, ref HashSet<IGameElement> elementSet)
        {
            elementSet.Add(element);
            
            if (element is IGameElementGroup group)
            {
                foreach (var child in group)
                {
                    CollectElements(child, ref elementSet);
                }
            }
        }
    }
}