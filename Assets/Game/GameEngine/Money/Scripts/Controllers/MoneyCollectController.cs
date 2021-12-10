using System;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyCollectController : MonoBehaviour,
        IGameStartElement,
        IGameFinishElement
    {
        [SerializeField]
        private Parameters parameters;

        private MoneyStorageComponent storageComponent;

        private TriggerComponent triggerComponent;

        private IPopupManager popupManager;

        void IGameStartElement.StartGame(IGameSystem system)
        {
            var entity = this.parameters.collector;
            this.storageComponent = entity.GetEntityComponent<MoneyStorageComponent>();

            this.triggerComponent = entity.GetEntityComponent<TriggerComponent>();
            this.triggerComponent.OnTriggerEntered += this.OnTriggerEntered;

            this.popupManager = system.GetService<IPopupManager>();
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.triggerComponent.OnTriggerEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(IEntity otherEntity)
        {
            if (otherEntity.TryGetEntityComponent(out MoneyResourceComponent resourceComponent))
            {
                resourceComponent.Collect(out var money);
                this.storageComponent.AddMoney(money);
                this.ShowPopup(money);
            }
        }

        private void ShowPopup(int money)
        {
            var resourceInfo = this.parameters.info;
            var rewardText = string.Format(resourceInfo.rewardFormat, money);
            var reward = new BaseReward(resourceInfo.icon, rewardText);
            
            var popupArgs = new UIArguments(
                new UIArgument(UIArgumentName.REWARD, reward)
            );
            this.popupManager.ShowPopup(PopupName.POPUP_REWARD, popupArgs);
        }

        [Serializable]
        public struct Parameters
        {
            [SerializeField]
            public Entity collector;

            [SerializeField]
            public MoneyResourceInfo info;
        }
    }
}