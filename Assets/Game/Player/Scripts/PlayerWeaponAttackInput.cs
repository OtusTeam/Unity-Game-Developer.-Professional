using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerWeaponAttackInput : MonoBehaviour
    {
        [Inject]
        private IGameManager gameManager;
        
        [Inject]
        private IWeaponAttackComponent attackComponent;
        
        private bool isEnabled;
        
        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
            this.gameManager.OnFinishGame += this.OnFinishGame;
        }

        private void OnStartGame()
        {
            this.isEnabled = true;
        }

        private void Update()
        {
            if (this.isEnabled)
            {
                this.ProcessAttackInput();
            }
        }

        private void OnFinishGame()
        {
            this.isEnabled = false;
        }

        private void OnDisable()
        {
            this.gameManager.OnStartGame -= this.OnStartGame;
            this.gameManager.OnFinishGame -= this.OnFinishGame;
        }

        private void ProcessAttackInput()
        {
            if (Input.GetMouseButton(0))
            {
                this.attackComponent.Attack();
            }
        }
    }
}