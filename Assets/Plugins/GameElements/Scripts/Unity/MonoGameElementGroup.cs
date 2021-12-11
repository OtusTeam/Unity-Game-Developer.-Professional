using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementGroup : MonoBehaviour, IGameElementGroup
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

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
                var monoElement = this.gameElements[i];
                this.elements.Add((IGameElement) monoElement);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            this.gameElements = GameElementsEditor.ValidateGameElements(this.gameElements);
        }
#endif
    }
}