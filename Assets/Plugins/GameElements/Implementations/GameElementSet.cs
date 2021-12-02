using System.Collections.Generic;
using JetBrains.Annotations;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementSet"/>
    public sealed class GameElementSet : GameElement, IGameElementSet
    {
        [CanBeNull]
        private IGameSystem gameSystem;

        private readonly HashSet<object> elements;

        public GameElementSet()
        {
            this.elements = new HashSet<object>();
        }

        public GameElementSet(IEnumerable<object> elements)
        {
            this.elements = new HashSet<object>(elements);
        }

        public bool AddElement(object element)
        {
            if (!this.elements.Add(element))
            {
                return false;
            }

            if (this.gameSystem != null)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(this.gameSystem);
                }    
            }

            return true;
        }

        public bool RemoveElement(object element)
        {
            if (this.elements.Remove(element))
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Dispose();
                }

                return true;
            }

            return false;
        }

        public bool ContainsElement(object element)
        {
            return this.elements.Contains(element);
        }

        protected override void OnSetup(IGameSystem system)
        {
            this.gameSystem = system;
            foreach (var element in this.elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(system);
                }
            }
        }

        protected override void OnDispose()
        {
            foreach (var element in this.elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Dispose();
                }
            }
        }
    }
}