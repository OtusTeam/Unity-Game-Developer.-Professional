using System.Collections;
using GameElements.Unity;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MonoGameContext : MonoGameSystem
    {
        [ReadOnly]
        [SerializeField]
        private bool gameLaunched;
        
        [SerializeField]
        private bool autoRun;

        [SerializeField]
        private MonoBehaviour[] subsystems;

        private void Awake()
        {
            this.LoadSubsystems();
        }
        
        private void LoadSubsystems()
        {
            for (int i = 0, count = this.subsystems.Length; i < count; i++)
            {
                var subsystem = this.subsystems[i];
                this.RegisterService(subsystem);
            }
        }

        private IEnumerator Start()
        {
            if (this.autoRun)
            {
                yield return new WaitForEndOfFrame();
                this.LaunchGame();
            }
        }

        private void LaunchGame()
        {
            this.InitGame();
            this.ReadyGame();
            this.StartGame();
            this.gameLaunched = true;
        }

#if UNITY_EDITOR

        [HideIf("autoRun")]
        [Button("Launch Game")]
        private void Editor_LaunchGame()
        {
            this.LaunchGame();
        }

        [ShowIf("gameLaunched")]
        [Button("End Game")]
        private void Editor_FinishGame()
        {
            this.FinishGame();
            this.DisposeGame();
            this.gameLaunched = false;
        }
#endif
    }
}