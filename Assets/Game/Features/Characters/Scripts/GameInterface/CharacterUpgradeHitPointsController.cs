using GameElements;
using Prototype.GameEngine;
using UnityEngine;

namespace Prototype.GameInterface
{
    public sealed class CharacterUpgradeHitPointsController : MonoBehaviour,
        IGameInitElement
    {
        [SerializeField]
        private ButtonPrice button;

        private ICharactersManager charactersManager;

        private ICharacter targetCharacter;

        private ICharacterUpgrade targetUpgrade;

        void IGameInitElement.InitGame(IGameSystem system)
        {
            this.charactersManager = system.GetService<ICharactersManager>();
        }

        public void Show(UIArguments args)
        {
            var characterId = args.Get<int>(UIArgumentName.CHARACTER_ID);
            this.targetCharacter = this.charactersManager.GetCharacter(characterId);
            this.targetCharacter.OnMoneyChanged += this.OnMoneyChanged;

            this.targetUpgrade = this.charactersManager.GetHitPointsUpgrade(characterId);

            this.button.SetPrice(this.targetUpgrade.Price.ToString());
            this.button.OnClicked += this.OnButtonClicked;

            this.UpdateButtonState();
        }

        public void Hide()
        {
            this.targetUpgrade = null;

            this.targetCharacter.OnMoneyChanged -= this.OnMoneyChanged;
            this.targetCharacter = null;

            this.button.OnClicked -= this.OnButtonClicked;
        }

        private void OnMoneyChanged(int money)
        {
            this.UpdateButtonState();
        }

        private void OnButtonClicked()
        {
            this.targetUpgrade.Upgrade();
        }

        private void UpdateButtonState()
        {
            var canUpgrade = this.targetUpgrade.CanUpgrade();
            this.button.SetEnable(canUpgrade);
        }
    }
}