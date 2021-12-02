using System;
using UnityEngine;

namespace GameElements.Unity
{
    public abstract class MonoGameElement : MonoBehaviour, IGameElement
    {
        protected IGameSystem GameSystem { get; private set; }

        void IGameElement.Setup(IGameSystem system)
        {
            if (this.GameSystem != null)
            {
                throw new Exception("Game system is already setuped!");
            }

            this.GameSystem = system;
            this.OnSetup();
        }

        protected virtual void OnSetup()
        {
        }

        void IGameElement.Dispose()
        {
            if (this.GameSystem == null)
            {
                return;
            }

            this.OnDispose();
            this.GameSystem = null;
        }

        protected virtual void OnDispose()
        {
        }
    }
}