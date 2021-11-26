using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerMoveController : MonoBehaviour
    {
        [Inject]
        private IGameManager gameManager;

        [Inject]
        private IMoveController moveController;
        
        private bool isEnable;

        private Vector3 moveDirection;
        
        #region Lifecycle

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
        }

        private void OnStartGame()
        {
            this.isEnable = true;
        }

        private void Update()
        {
            if (this.isEnable)
            {
                this.ProcessPlayerInput();
            }
        }

        private void FixedUpdate()
        {
            if (this.isEnable)
            {
                this.moveController.Move(this.moveDirection);
            }
        }

        private void OnFinishGame()
        {
            this.isEnable = false;
        }

        private void OnDisable()
        {
            this.gameManager.OnFinishGame += this.OnFinishGame;
        }

        #endregion

        private void ProcessPlayerInput()
        {
            this.moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                this.moveDirection = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.moveDirection = Vector3.back;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                this.moveDirection = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.moveDirection = Vector3.right;
            }
        }
    }
}