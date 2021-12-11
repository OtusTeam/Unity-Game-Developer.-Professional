using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class PlayerCharacterService : MonoBehaviour, IPlayerCharacterService,
        IGameInitElement
    {
        private PlayerEntityService entityService;

        private ICharactersManager charactersManager;
        
        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.entityService = system.GetService<PlayerEntityService>();
            this.charactersManager = system.GetService<ICharactersManager>();
        }

        public ICharacter GetPlayerCharacter()
        {
            var playerEntity = this.entityService.GetPlayerEntity();
            return this.charactersManager.GetCharacter(playerEntity.Id);
        }
    }
}