using DynamicObjects;
using UnityEngine;
using Zenject;

namespace Otus
{
    public sealed class PlayerMoveController : MonoBehaviour
    {
        [Inject]
        private IGameManager gameManager;

        [Inject]
        private IDynamicObject player;

        private bool isEnable;

        private MoveTransformData moveData;

        #region Lifecycle

        private void OnEnable()
        {
            this.gameManager.OnStartGame += this.OnStartGame;
        }

        private void OnStartGame()
        {
            this.moveData = new MoveTransformData();
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
                this.player.InvokeMethod(ActionKey.MOVE, this.moveData);
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
            this.moveData.direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                this.moveData.direction = Vector3.forward;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                this.moveData.direction = Vector3.back;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                this.moveData.direction = Vector3.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                this.moveData.direction = Vector3.right;
            }
        }
    }
}