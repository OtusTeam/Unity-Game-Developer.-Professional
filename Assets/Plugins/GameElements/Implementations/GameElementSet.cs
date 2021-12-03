using System.Collections.Generic;

namespace GameElements
{
    public sealed class GameElementSet : GameElement
    {
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
                    gameElement.BindGame(this.gameSystem);
                }    
            }

            return true;
        }

        public bool RemoveElement(object element)
        {
            if (!this.elements.Remove(element))
            {
                return false;
            }

            if (this.gameSystem != null)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }    
            }
            
            return true;
        }

        public bool ContainsElement(object element)
        {
            return this.elements.Contains(element);
        }

        protected override void BindGame(IGameSystem system)
        {
            this.gameSystem = system;
            foreach (var element in this.elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.BindGame(system);
                }
            }
        }

        protected override void UnbindGame()
        {
            foreach (var element in this.elements)
            {
                if (element is IGameElement gameElement)
                {
                    gameElement.UnbindGame();
                }
            }
        }
    }
}