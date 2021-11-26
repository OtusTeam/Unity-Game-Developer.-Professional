using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class GameManager : MonoBehaviour, IGameManager
    {
        public event Action OnInitializeGame;
        
        public event Action OnStartGame;

        public event Action OnFinishGame;

        [Button("Initialize Game")]
        public void InitializeGame()
        {
            this.OnInitializeGame?.Invoke();
        }

        [Button("Start Game")]
        public void StartGame()
        {
            this.OnStartGame?.Invoke();
        }

        [Button("Finish Game")]
        public void FinishGame()
        {
            this.OnFinishGame?.Invoke();
        }
    }
}