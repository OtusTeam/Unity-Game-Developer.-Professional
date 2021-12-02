using System.Collections.Generic;

namespace GameElements
{
    /// <inheritdoc cref="IGameElementSet"/>
    public sealed class GameElementSet : GameElement, IGameElementSet
    {
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

            if (element is IGameElement gameElement)
            {
                gameElement.Setup(this.GameSystem);
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

        protected override void OnSetup()
        {
            base.OnSetup();
            foreach (var element in this.elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.Setup(this.GameSystem);
                }
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
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