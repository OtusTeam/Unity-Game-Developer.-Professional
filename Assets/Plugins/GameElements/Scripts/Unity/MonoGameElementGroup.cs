using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameElements.Unity
{
    public sealed class MonoGameElementGroup : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private Object[] gameElements;

        private HashSet<IGameElement> elements;

        public IEnumerable<IGameElement> GetElements()
        {
            return this.elements;
        }

        private void Awake()
        {
            this.elements = new HashSet<IGameElement>();
            this.LoadElements();
        }

        private void LoadElements()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var element = this.gameElements[i];
                if (element is IGameElement gameElement)
                {
                    this.elements.Add(gameElement);
                }
                else if (element is GameObject gameObject)
                {
                    var elements = gameObject.GetComponents<IGameElement>();
                    this.elements.UnionWith(elements);
                }
            }
        }
    }
}