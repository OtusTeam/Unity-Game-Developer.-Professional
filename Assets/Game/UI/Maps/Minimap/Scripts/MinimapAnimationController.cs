using GameElements;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MinimapAnimationController : MonoBehaviour, 
        IGameReadyElement,
        IGameStartElement,
        IGameFinishElement
    {
        private const string HIDDEN_ANIMATION_NAME = "Hidden";

        private const string SHOW_ANIMATION_NAME = "Show";

        private const string HIDE_ANIMATION_NAME = "Hide";

        [SerializeField]
        private Animator animator;

        void IGameReadyElement.ReadyGame(IGameSystem system)
        {
            this.animator.Play(HIDDEN_ANIMATION_NAME);
        }

        void IGameStartElement.StartGame(IGameSystem system)
        {
            this.animator.Play(SHOW_ANIMATION_NAME);
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.animator.Play(HIDE_ANIMATION_NAME);
        }
    }
}