using GameElements;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class CharacterCardController : MonoBehaviour, 
        IGameInitElement
    {
        [SerializeField]
        private Card card;

        private ICharactersManager charactersManager;

        private ICharacter targetCharacter;
        
        public void Show(UIArguments args)
        {
            var characterId = args.Get<int>(UIArgumentName.CHARACTER_ID);
            this.targetCharacter = this.charactersManager.GetCharacter(characterId);
            this.targetCharacter.OnDamageChanged += this.OnDamageChanged;
            this.targetCharacter.OnHitPointsChanged += this.OnHitPointsChanged;

            this.card.SetTitle(this.targetCharacter.Name);
            this.card.SetIcon(this.targetCharacter.Icon);
            this.UpdateHitPoints(this.targetCharacter.HitPoints);
            this.UpdateDamage(this.targetCharacter.Damage);
        }

        public void Hide()
        {
            this.targetCharacter.OnDamageChanged -= this.OnDamageChanged;
            this.targetCharacter.OnHitPointsChanged -= this.OnHitPointsChanged;
            this.targetCharacter = null;
        }

        #region Callbacks

        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.charactersManager = system.GetService<ICharactersManager>();
        }

        private void OnHitPointsChanged(int hitPoints)
        {
            this.UpdateHitPoints(hitPoints);
        }

        private void OnDamageChanged(int damage)
        {
            this.UpdateDamage(damage);
        }

        #endregion

        private void UpdateHitPoints(int hitPoints)
        {
            this.card.SetProperty(0, $"Hit Points: {hitPoints}");
        }

        private void UpdateDamage(int damage)
        {
            this.card.SetProperty(1, $"Damage: {damage}");
        }
    }
}