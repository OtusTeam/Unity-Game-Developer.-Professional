using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class CharactersManager : MonoBehaviour, ICharactersManager
    {
        [SerializeField]
        private EntitiesManager entitiesManager;

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