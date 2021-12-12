using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameEngineAdapter
{
    public sealed class CharactersManager : MonoBehaviour, ICharactersManager, 
        IGameInitElement
    {
        private IEntitiesManager entitiesManager;

        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.entitiesManager = system.GetService<IEntitiesManager>();
        }

        public ICharacter GetCharacter(int characterId)
        {
            var entity = this.entitiesManager.GetEntity(characterId);
            return new Character(entity);
        }

        public ICharacterUpgrade GetHitPointsUpgrade(int characterId)
        {
            var entity = this.entitiesManager.GetEntity(characterId);
            return new CharacterHitPointsUpgrade(entity);
        }

        public ICharacterUpgrade GetDamageUpgrade(int characterId)
        {
            var entity = this.entitiesManager.GetEntity(characterId);
            return new CharacterDamageUpgrade(entity);
        }
    }
}