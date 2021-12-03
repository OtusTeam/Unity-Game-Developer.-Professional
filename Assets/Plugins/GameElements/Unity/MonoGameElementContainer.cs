using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementContainer : MonoBehaviour, IGameElement
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private GameElementContainer container;

        public bool AddElement(object element)
        {
            return this.container.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.container.RemoveElement(element);
        }

        public bool ContainsElement(object element)
        {
            return this.container.ContainsElement(element);
        }

        #region Lifecycle

        private void Awake()
        {
            this.container = new GameElementContainer();
            this.IniitializeElementSet();
        }

        private void IniitializeElementSet()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var gameElement = this.gameElements[i];
                if (gameElement != null)
                {
                    this.AddElement(gameElement);
                }
            }
        }

        void IGameElement.BindGame(IGameSystem system)
        {
            this.container.BindGame(system);
        }

        void IGameElement.UnbindGame()
        {
            this.container.UnbindGame();
        }

        #endregion
    }
}