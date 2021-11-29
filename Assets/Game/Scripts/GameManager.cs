using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Otus
{
    public sealed class GameManager : MonoBehaviour, IGameManager
    {
        public event Action OnInitializeGame;

        public event Action OnStartGame;

        public event Action OnFinishGame;

        private bool isGameStarted;

        private readonly List<IUpdateListener> updateListeners;

        private readonly List<IUpdateListener> fixedUpdateListeners;

        private readonly List<IUpdateListener> processingListeners;
        
        [SerializeField]
        private bool autoRun;

        public GameManager()
        {
            this.updateListeners = new List<IUpdateListener>();
            this.fixedUpdateListeners = new List<IUpdateListener>();
            this.processingListeners = new List<IUpdateListener>();
        }

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

        private void Update()
        {
            if (!this.isGameStarted)
            {
                return;
            }

            this.processingListeners.Clear();
            this.processingListeners.AddRange(this.updateListeners);

            for (int i = 0, count = this.processingListeners.Count; i < count; i++)
            {
                var listener = this.processingListeners[i];
                listener.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (!this.isGameStarted)
            {
                return;
            }
            
            this.processingListeners.Clear();
            this.processingListeners.AddRange(this.fixedUpdateListeners);
            
            for (int i = 0, count = this.processingListeners.Count; i < count; i++)
            {
                var listener = this.processingListeners[i];
                listener.OnUpdate(Time.fixedDeltaTime);
            }
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
            this.isGameStarted = true;
            this.OnStartGame?.Invoke();
        }

        [Button("Finish Game")]
        public void FinishGame()
        {
            this.OnFinishGame?.Invoke();
        }

        public void AddUpdateListener(IUpdateListener listener)
        {
            this.updateListeners.Add(listener);
        }

        public void RemoveUpdateListener(IUpdateListener listener)
        {
            this.updateListeners.Remove(listener);
        }

        public void AddFixedUpdateListener(IUpdateListener listener)
        {
            this.fixedUpdateListeners.Add(listener);
        }

        public void RemoveFixedUpdateListener(IUpdateListener listener)
        {
            this.fixedUpdateListeners.Remove(listener);
        }
    }
}