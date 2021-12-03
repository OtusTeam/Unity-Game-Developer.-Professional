using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementSet : MonoBehaviour, IGameElement
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private GameElementSet set;

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
            this.set = new GameElementSet();
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
            this.set.BindGame(system);
        }

        void IGameElement.UnbindGame()
        {
            this.set.UnbindGame();
        }

        #endregion
    }
}