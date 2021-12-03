using System;
using GameElements;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class MinimapAnimationController : MonoGameController
    {
        private const string HIDDEN_ANIMATION_NAME = "Hidden";

        private const string SHOW_ANIMATION_NAME = "Show";

        private const string HIDE_ANIMATION_NAME = "Hide";

        [SerializeField]
        private Animator animator;
        
        protected override void OnReadyGame()
        {
            base.OnReadyGame();
            this.animator.Play(HIDDEN_ANIMATION_NAME);
        }

        protected override void OnStartGame()
        {
            base.OnStartGame();
            this.animator.Play(SHOW_ANIMATION_NAME);
        }

        protected override void OnFinishGame()
        {
            base.OnFinishGame();
            this.animator.Play(HIDE_ANIMATION_NAME);
        }
    }
}