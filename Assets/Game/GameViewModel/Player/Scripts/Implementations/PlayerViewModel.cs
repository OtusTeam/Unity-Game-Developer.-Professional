using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.ViewModel
{
    public sealed class PlayerViewModel : MonoBehaviour, IPlayerManager,
        IGameInitElement
    {
        private PlayerEntityService entityService;

        private IEntity player;

        private MoveComponent moveComponent;

        public ICharacter GetCharacter()
        {
            return new Character(this.player);
        }

        public void Move(Vector3 moveVector)
        {
            var vector = new WorldVector(moveVector.x, moveVector.z);
            this.moveComponent.Move(vector);
        }

        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.entityService = system.GetService<PlayerEntityService>();
            this.player = this.entityService.GetPlayerEntity();
            this.moveComponent = this.player.GetEntityComponent<MoveComponent>();
        }
    }
}