using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementLayer : MonoGameElement
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private readonly GameElementLayer layer;

        public MonoGameElementLayer()
        {
            this.layer = new GameElementLayer();
        }

        public bool AddElement(object element)
        {
            return this.layer.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.layer.RemoveElement(element);
        }

        public T GetElement<T>()
        {
            return this.layer.GetElement<T>();
        }

        public bool TryGetElement<T>(out T element)
        {
            return this.layer.TryGetElement(out element);
        }

        #region Lifecycle

        private void Awake()
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

        protected override void BindGame(IGameSystem system)
        {
            IGameElement gameElement = this.layer;
            gameElement.BindGame(system);
        }

        protected override void UnbindGame()
        {
            IGameElement gameElement = this.layer;
            gameElement.UnbindGame();
        }
        
        #endregion
    }
}