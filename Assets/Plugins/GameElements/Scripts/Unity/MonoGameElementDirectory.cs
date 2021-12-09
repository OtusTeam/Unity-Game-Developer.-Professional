using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementDirectory : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private Transform[] containers;

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
            for (int i = 0, count = this.containers.Length; i < count; i++)
            {
                var container = this.containers[i];
                foreach (Transform child in container)
                {
                    var gameElements = child.GetComponents<IGameElement>();
                    this.elements.UnionWith(gameElements);
                }
            }
        }
    }
}