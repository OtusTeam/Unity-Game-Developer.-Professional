using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PlayerManager : MonoBehaviour, IPlayerManager
    {
        [SerializeField]
        private PlayerEntityService entityService;

        public ICharacter GetCharacter()
        {
            var player = this.entityService.GetPlayerEntity();
            return new Character(player);
        }
    }
}