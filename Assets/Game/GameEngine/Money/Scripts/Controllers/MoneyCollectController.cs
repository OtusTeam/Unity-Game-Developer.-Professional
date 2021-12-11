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

        private IEntitiesManager entitiesManager;

        void IGameStartElement.StartGame(IGameSystem system)
        {
            var collectorEntity = this.parameters.collector;
            this.storageComponent = collectorEntity.GetEntityComponent<MoneyStorageComponent>();

            this.triggerComponent = collectorEntity.GetEntityComponent<TriggerComponent>();
            this.triggerComponent.OnTriggerEntered += this.OnTriggerEntered;

            this.entitiesManager = system.GetService<IEntitiesManager>();
            this.popupManager = system.GetService<IPopupManager>();
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.triggerComponent.OnTriggerEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(IEntity otherEntity)
        {
            if (!otherEntity.TryGetEntityComponent(out MoneyResourceComponent resourceComponent))
            {
                return;
            }

            var moneyReward = resourceComponent.Money;
            this.storageComponent.AddMoney(moneyReward);
            this.entitiesManager.RemoveEntity(otherEntity);
            this.ShowRewardPopup(moneyReward);
        }

        private void ShowRewardPopup(int money)
        {
            var resourceInfo = this.parameters.info;
            var rewardText = string.Format(resourceInfo.rewardFormat, money);
            var reward = new BaseReward(resourceInfo.portraitIcon, rewardText);
            
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