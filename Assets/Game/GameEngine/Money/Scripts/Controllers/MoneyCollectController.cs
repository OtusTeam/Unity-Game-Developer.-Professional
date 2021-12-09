using System;
using GameElements;
using Popups;
using Prototype.UI;
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
            var moneyResource = new MoneyResource(this.parameters.info, money);
            var popupArgs = new GameResourcePopupArgs(moneyResource);
            this.popupManager.ShowPopup(PopupName.POPUP_RESOURCE, popupArgs);
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