using GameElements;
using Prototype.GameEngineAdapter;
using UnityEngine;

namespace Prototype.GameInput
{
    public sealed class PlayerMoveInputController : MonoBehaviour,
        IGameStartElement,
        IGameFinishElement
    {
        private IPlayerManager playerManager;

        private Vector3 moveVector;
        
        private bool moveRequired;

        private float fixedDeltaTime;

        #region Lifecycle

        private void Awake()
        {
            this.enabled = false;
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }

        void IGameStartElement.StartGame(IGameSystem system)
        {
            this.playerManager = system.GetService<IPlayerManager>();
            this.enabled = true;
        }

        private void Update()
        {
            this.ProcessInput();
        }

        private void FixedUpdate()
        {
            this.ProcessMove();
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.enabled = false;
        }

        #endregion

        private void ProcessInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.moveVector = Vector3.left;
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.moveVector = Vector3.right;
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.moveVector = Vector3.forward;
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.moveVector = Vector3.back;
                this.moveRequired = true;
            }
        }
        
        private void ProcessMove()
        {
            if (this.moveRequired)
            {
                this.playerManager.Move(this.moveVector * this.fixedDeltaTime);
                this.moveRequired = false;
            }
        }
    }
}