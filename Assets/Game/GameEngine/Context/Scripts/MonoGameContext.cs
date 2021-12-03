using System.Collections;
using GameElements.Unity;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MonoGameContext : MonoGameSystem
    {
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
                this.AddService(subsystem);
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
            this.ReadyGame();
            yield return new WaitForEndOfFrame();
            this.StartGame();
        }
    }
}