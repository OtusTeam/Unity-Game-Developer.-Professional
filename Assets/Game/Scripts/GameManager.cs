using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class GameManager : MonoBehaviour, IGameManager
    {
        public event Action OnInitializeGame;
        
        public event Action OnStartGame;

        public event Action OnFinishGame;

        [SerializeField]
        private bool autoRun;

        private IEnumerator Start()
        {
            if (!this.autoRun)
            {
                yield break;
            }
            
            yield return new WaitForEndOfFrame();
            this.InitializeGame();
            yield return new WaitForEndOfFrame();
            this.StartGame();
        }

        [HideIf("autoRun")]
        [Button("Initialize Game")]
        public void InitializeGame()
        {
            this.OnInitializeGame?.Invoke();
        }

        [HideIf("autoRun")]
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