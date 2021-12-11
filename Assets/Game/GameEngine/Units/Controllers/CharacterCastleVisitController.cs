using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class CharacterCastleVisitController : MonoBehaviour,
        IGameStartElement,
        IGameFinishElement
    {
        [SerializeField]
        private Entity character;

        private TriggerComponent triggerComponent;

        private IPopupManager popupManager;

        void IGameStartElement.StartGame(IGameSystem system)
        {
            this.triggerComponent = this.character.GetEntityComponent<TriggerComponent>();
            this.triggerComponent.OnTriggerEntered += this.OnCharacterEntered;

            this.popupManager = system.GetService<IPopupManager>();
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.triggerComponent.OnTriggerEntered -= this.OnCharacterEntered;
        }

        private void OnCharacterEntered(IEntity entity)
        {
            if (entity.TryGetEntityComponent(out UnitInfoComponent component) &&
                component.Type == UnitType.CASTLE)
            {
                this.ShowHomePopup(entity);
            }
        }

        private void ShowHomePopup(IEntity castle)
        {
            var popupArgs = new UIArguments(
                new UIArgument(UIArgumentName.CHARACTER_ID, this.character.Id),
                new UIArgument(UIArgumentName.CASTLE_ID, castle.Id)
            );
            this.popupManager.ShowPopup(PopupName.POPUP_HOME, popupArgs);
        }
    }
}