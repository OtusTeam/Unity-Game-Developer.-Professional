using System.Collections;
using GameElements.Unity;
using UnityEngine;

namespace GameEngine
{
    public sealed class GameSystem : UnityGameSystem
    {
        [SerializeField]
        private bool autoRun;

        [SerializeField]
        private MonoBehaviour[] subsystems;
        
        public void InitializeGame()
        {
            for (int i = 0, count = this.subsystems.Length; i < count; i++)
            {
                var subsystem = this.subsystems[i];
                this.AddElement(subsystem);
            }
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
            this.PrepareGame(this);
            yield return new WaitForEndOfFrame();
            this.ReadyGame(this);
            yield return new WaitForEndOfFrame();
            this.StartGame(this);
        }
    }
}