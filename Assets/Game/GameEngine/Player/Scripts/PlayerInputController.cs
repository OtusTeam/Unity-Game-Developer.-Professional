using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PlayerInputController : MonoBehaviour,
        IGameStartElement,
        IGameFinishElement
    {
        private MoveComponent moveComponent;

        private bool moveRequired;

        private WorldVector moveDirection;

        private float fixedDeltaTime;

        #region Lifecycle

        private void Awake()
        {
            this.enabled = false;
            this.fixedDeltaTime = Time.fixedDeltaTime;
        }

        void IGameStartElement.StartGame(IGameSystem system)
        {
            var player = system.GetService<PlayerService>();
            this.moveComponent = player.Character.GetEntityComponent<MoveComponent>();
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
                this.moveDirection = new WorldVector(-1, 0);
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                this.moveDirection = new WorldVector(1, 0);
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                this.moveDirection = new WorldVector(0, 1);
                this.moveRequired = true;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.moveDirection = new WorldVector(0, -1);
                this.moveRequired = true;
            }
        }
        
        private void ProcessMove()
        {
            if (this.moveRequired)
            {
                this.moveComponent.Move(this.moveDirection, this.fixedDeltaTime);
                this.moveRequired = false;
            }
        }
    }
}