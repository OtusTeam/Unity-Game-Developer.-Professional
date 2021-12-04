using System.Collections.Generic;
using UnityEngine;

namespace GameElements.Unity
{
    public sealed class MonoGameElementLayer : MonoBehaviour, IGameElement
    {
        [SerializeField]
        private MonoBehaviour[] gameElements;

        private GameElementLayer layer;

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

        public IEnumerable<T> GetElements<T>()
        {
            return this.layer.GetElements<T>();
        }

        public bool TryGetElement<T>(out T element)
        {
            return this.layer.TryGetElement(out element);
        }

        #region Lifecycle

        private void Awake()
        {
            this.layer = new GameElementLayer();
            this.InitializeElementLayer();
        }

        private void InitializeElementLayer()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var gameElement = this.gameElements[i];
                if (gameElement != null)
                {
                    this.layer.AddElement(gameElement);
                }
            }
        }

        void IGameElement.BindGame(IGameSystem system)
        {
            this.layer.BindGame(system);
        }

        void IGameElement.Dispose()
        {
            this.layer.Dispose();
        }
        
        #endregion
    }
}