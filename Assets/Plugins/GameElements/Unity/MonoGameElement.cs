using System;
using UnityEngine;

namespace GameElements.Unity
{
    public abstract class MonoGameElement : MonoBehaviour, IGameElement
    {
        void IGameElement.BindGame(IGameSystem system)
        {
            if (system == null)
            {
                throw new Exception("The system is null!");
            }
            
            this.BindGame(system);
        }

        protected virtual void BindGame(IGameSystem system)
        {
        }

        void IGameElement.UnbindGame()
        {
            this.UnbindGame();
        }

        protected virtual void UnbindGame()
        {
        }
    }
}