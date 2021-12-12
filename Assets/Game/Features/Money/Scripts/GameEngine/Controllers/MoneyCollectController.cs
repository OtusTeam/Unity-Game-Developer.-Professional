using System;
using GameElements;
using UnityEngine;

namespace Prototype.GameEngine
{
    public sealed class MoneyCollectController : MonoBehaviour,
        IGameReferenceElement,
        IGameStartElement,
        IGameFinishElement
    {
        public IGameSystem GameSystem { private get; set; }

        [SerializeField]
        private Parameters parameters;

        private MoneyStorageComponent storageComponent;

        private TriggerComponent triggerComponent;

        void IGameStartElement.StartGame(IGameSystem system)
        {
            var collectorEntity = this.parameters.collector;
            this.storageComponent = collectorEntity.GetEntityComponent<MoneyStorageComponent>();

            this.triggerComponent = collectorEntity.GetEntityComponent<TriggerComponent>();
            this.triggerComponent.OnTriggerEntered += this.OnTriggerEntered;
        }

        void IGameFinishElement.FinishGame(IGameSystem system)
        {
            this.triggerComponent.OnTriggerEntered -= this.OnTriggerEntered;
        }

        private void OnTriggerEntered(IEntity otherEntity)
        {
            if (!otherEntity.TryGetEntityComponent(out MoneyResourceComponent moneyComponent))
            {
                return;
            }

            var moneyReward = moneyComponent.Money;
            this.storageComponent.AddMoney(moneyReward);
            this.RemoveMoneyEntity(otherEntity);
            this.ShowRewardPopup(moneyReward);
        }

        private void RemoveMoneyEntity(IEntity otherEntity)
        {
            var entitiesManager = this.GameSystem.GetService<IEntitiesManager>();
            entitiesManager.RemoveEntity(otherEntity);
        }
        
        //Показ попапа награды!!!
        private void ShowRewardPopup(int money)
        {
            if (!this.GameSystem.TryGetService(out IPopupManager popupManager))
            {
                return;
            }
            
            var reward = new Reward(this.parameters.info, money);
            var popupArgs = new UIArguments(
                new UIArgument(UIArgumentName.REWARD, reward)
            );

            popupManager.ShowPopup(PopupName.POPUP_REWARD, popupArgs);
        }

        [Serializable]
        public struct Parameters
        {
            [SerializeField]
            public Entity collector;

            [SerializeField]
            public MoneyResourceInfo info;
        }
        
        private sealed class Reward : IReward
        {
            public Sprite Icon { get; }
       
            public string Text { get; }

            public Reward(MoneyResourceInfo moneyInfo, int money)
            {
                this.Icon = moneyInfo.portraitIcon;
                this.Text = string.Format(moneyInfo.rewardFormat, money);
            }
        }
    }
}