using System;
using System.Collections.Generic;
using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace GameEngine
{
    public sealed class MonoGameModule : MonoGameElement
    {
        [SerializeField]
        private Element[] gameElements;

        private readonly GameElementSet container;
        
        private readonly Dictionary<GameElementId, object> publicMap;

        public MonoGameModule()
        {
            this.container = new GameElementSet();
            this.publicMap = new Dictionary<GameElementId, object>();
        }
        
        public T GetElement<T>(GameElementId id)
        {
            return (T) this.publicMap[id];
        }
        
        public bool TryGetElement<T>(GameElementId id, out T element)
        {
            if (this.publicMap.TryGetValue(id, out var value))
            {
                element = (T) value;
                return true;
            }

            element = default;
            return false;
        }

        public void AddElement(GameElementId elementId, object element)
        {
            if (!this.container.AddElement(element))
            {
                return;
            }
            
            if (elementId != GameElementId.NONE)
            {
                this.publicMap.Add(elementId, element);
            }
        }

        public void RemoveElement(GameElementId id)
        {
            if (!this.publicMap.TryGetValue(id, out var element))
            {
                return;
            }

            this.publicMap.Remove(id);
            this.container.RemoveElement(element);
        }

        #region Lifecycle

        private void Awake()
        {
            for (int i = 0, count = this.gameElements.Length; i < count; i++)
            {
                var element = this.gameElements[i];
                this.AddElement(element.id, element.value);
            }
        }

        protected override void OnSetup()
        {
            base.OnSetup();
            IGameElement container = this.container;
            container.Setup(this.GameSystem);
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            IGameElement container = this.container;
            container.Dispose();
        }

        #endregion

        [Serializable]
        private struct Element
        {
            [SerializeField]
            public GameElementId id;
            
            [SerializeField]
            public MonoBehaviour value;
        }
    }
}