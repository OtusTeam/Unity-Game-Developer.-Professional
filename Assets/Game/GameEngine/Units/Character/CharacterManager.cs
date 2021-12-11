using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class CharacterManager : MonoBehaviour, ICharacterManager, IGameInitElement
    {
        private IEntityManager entityManager;
        
        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.entityManager = system.GetService<IEntityManager>();
        }

        public ICharacter GetCharacter(int characterId)
        {
            var entity = this.entityManager.GetEntity(characterId);
            return new Character(entity);
        }

        public ICharacterUpgrade GetHitPointsUpgrade(int characterId)
        {
            var entity = this.entityManager.GetEntity(characterId);
            return new CharacterHitPointsUpgrade(entity);
        }

        public ICharacterUpgrade GetDamageUpgrade(int characterId)
        {
            var entity = this.entityManager.GetEntity(characterId);
            return new CharacterDamageUpgrade(entity);
        }
    }
}