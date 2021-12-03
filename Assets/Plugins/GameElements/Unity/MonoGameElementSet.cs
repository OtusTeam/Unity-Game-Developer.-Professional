using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementSet : MonoGameElement
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private readonly GameElementSet set;

        public MonoGameElementSet()
        {
            this.set = new GameElementSet();
        }

        public bool AddElement(object element)
        {
            return this.set.AddElement(element);
        }

        public bool RemoveElement(object element)
        {
            return this.set.RemoveElement(element);
        }

        public bool ContainsElement(object element)
        {
            return this.set.ContainsElement(element);
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
            IGameElement gameElement = this.set;
            gameElement.BindGame(system);
        }

        protected override void UnbindGame()
        {
            IGameElement gameElement = this.set;
            gameElement.UnbindGame();
        }

        #endregion
    }
}