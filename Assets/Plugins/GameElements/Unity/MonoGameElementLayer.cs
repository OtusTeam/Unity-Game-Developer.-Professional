using System.Collections.Generic;

namespace GameElements.Unity
{
    public class MonoGameElementLayer : MonoGameElementGroup
    {
        private GenericDictionary elementMap;

        public T GetElement<T>()
        {
            return this.elementMap.Get<T>();
        }

        public IEnumerable<T> GetElements<T>()
        {
            return this.elementMap.All<T>();
        }

        public bool TryGetElement<T>(out T element)
        {
            return this.elementMap.TryGet(out element);
        }
        
        private void Awake()
        {
            this.elementMap = new GenericDictionary();
            this.InitializeElements();
        }

        private void InitializeElements()
        {
            var count = this.gameElements.Length;
            for (var i = 0; i < count; i++)
            {
                var element = this.gameElements[i];
                if (element != null)
                {
                    this.elementMap.Add(element);
                }
            }
        }
    }
}